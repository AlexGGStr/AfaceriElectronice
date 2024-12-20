using backend.Models;
using backend.Services.AuthService;
using backend.Services.CartService;
using backend.Services.CategoryService;
using backend.Services.EmailService;
using backend.Services.ImageService;
using backend.Services.PlaceOrder;
using backend.Services.ProductService;
using backend.Services.UserAdressService;
using backend.Services.UserPaymentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header with Bearer, e.g -bearer + token",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    config.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<AlexContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserAdressService, UserAdressService>();
builder.Services.AddScoped<IUserPaymentService, UserPaymentService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPlaceOrderService, PlaceOrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var value = builder.Configuration.GetSection("AppSettings:Token").Value;
        if (value != null)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
    }).AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleAuthSettings:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleAuthSettings:ClientSecret").Value;
    });

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

app.Run();

