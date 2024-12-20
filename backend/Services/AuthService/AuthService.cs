using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using backend.DTOs.Email;
using backend.DTOs.User;
using backend.Models;
using backend.Services.EmailService;
using Google.Apis.Auth;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;

namespace backend.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly AlexContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(AlexContext context, IConfiguration configuration, IEmailService emailService)
    {
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<ServiceResponse<string>> AuthenticateWithGoogle(string googleToken)
    {
        var response = new ServiceResponse<string>();
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken);
            var email = payload.Email;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                user = new User
                {
                    Username = payload.Name,
                    Email = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    VerifiedAt = DateTime.Now
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            response.Data = CreateWebToken(user);
        }
        catch (InvalidJwtException ex)
        {
            response.Success = false;
            response.Message = "Invalid Google token.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "An error occurred.";
        }
        return response;
    }

    public async Task<ServiceResponse<int>> VerifyUser(string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        var response = new ServiceResponse<int>();
        if (user == null)
        {
            response.Success = false;
            response.Message = "Invalid Token";
            return response;
        }
        
        user.VerifiedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        response.Data = user.Id;
        response.Message = "User Verified";
        return response;
    }

    public async Task<ServiceResponse<int>> Register(UserRegisterDto userDto)
    {
        ServiceResponse<int> response = new ServiceResponse<int>();
        if (await UserExists(userDto.Username))
        {
            response.Success = false;
            response.Message = "Username Already Exists";
            return response;
        }
        if (await UserExists(userDto.Email))
        {
            
            response.Success = false;
            response.Message = "Email Already Exists";
            return response;
        }
        CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User
        {
            Username = userDto.Username,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            VerificationToken = CreateVerificationToken(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        _emailService.SendEmail(new Email
        {
            To = user.Email,
            Subject = "Verify your account",
            Body = $"Hello {user.Username}, <br /> Please verify your account by clicking the link below: <br/> <a href=\"http://localhost:3000/verifyaccount?token={user.VerificationToken}\">http://localhost:3000/verifyaccount?token={user.VerificationToken}</a>",
        });
        
        
        response.Data = user.Id;
        return response;
    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Email.ToLower().Equals(email.ToLower()));

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not Found";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong Password";
        }
        else if (user.VerifiedAt == null)
        {
            response.Success = false;
            response.Message = "User not verified";
        }
        else
        {
            response.Data = CreateWebToken(user);
        }
        return response;
    }

    public async Task<bool> UserExists(string username)
    {
        if (await _context.Users.AnyAsync(user => user.Username.ToLower() == username.ToLower() || user.Email.ToLower() == username.ToLower()))
        {
            return true;
        }
        return false;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateWebToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role ?? "-")
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token); //Token
    }

    private string CreateVerificationToken()
    {
        return Convert.ToHexString((RandomNumberGenerator.GetBytes(64)));
    }

    

}