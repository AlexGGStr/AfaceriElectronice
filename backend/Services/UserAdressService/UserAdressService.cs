using AutoMapper;
using Azure;
using backend.DTOs.UserAdress;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserAdressService;

public class UserAdressService : IUserAdressService
{
    private readonly AlexContext _context;
    private readonly IMapper _mapper;

    public UserAdressService(AlexContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<ServiceResponse<List<GetUserAdressDto>>> GetAllUserAdresses(int userId)
    {
        var serviceResponse = new ServiceResponse<List<GetUserAdressDto>>();
        var adresses = await _context.UserAdresses.Where(c => c.UserId == userId).ToListAsync();
        serviceResponse.Data = _mapper.Map<List<GetUserAdressDto>>(adresses);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserAdressDto>> GetUserAdressById(int userId, int id)
    {
        var serviceResponse = new ServiceResponse<GetUserAdressDto>();
        var adress = await _context.UserAdresses.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        serviceResponse.Data = _mapper.Map<GetUserAdressDto>(adress);
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> UpdateUserAdress(int userId, int id, PutUserAdressDto updatedUserAdress)
    {
        var serviceResponse = new ServiceResponse<int>();
        var adress = await _context.UserAdresses.
            FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        if (adress != null)
        {
            adress.AdressLine = updatedUserAdress.AdressLine;
            adress.City = updatedUserAdress.City;
            adress.Telephone = updatedUserAdress.Telephone;
            adress.PostalCode = updatedUserAdress.PostalCode;
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Adress not found";
        }
        await _context.SaveChangesAsync();
        serviceResponse.Data = adress.Id;
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> DeleteUserAdress(int userId, int id)
    {
        var serviceResponse = new ServiceResponse<int>();
        var adress = await _context.UserAdresses.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        if (adress != null)
        {
            _context.UserAdresses.Remove(adress);
            await _context.SaveChangesAsync();
            serviceResponse.Data = adress.Id;
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Adress not found";
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> AddUserAdress(int userId, PostUserAdressDto newUserAdress)
    {
        var serviceResponse = new ServiceResponse<int>();
        var adress = _mapper.Map<UserAdress>(newUserAdress);
        adress.UserId = userId;
        await _context.UserAdresses.AddAsync(adress);
        await _context.SaveChangesAsync();
        serviceResponse.Data = adress.Id;
        return serviceResponse;
    }
}