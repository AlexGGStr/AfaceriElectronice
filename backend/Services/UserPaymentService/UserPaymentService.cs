using AutoMapper;
using backend.DTOs.UserPayment;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserPaymentService;

public class UserPaymentService : IUserPaymentService
{
    private readonly AlexContext _context;
    private readonly IMapper _mapper;

    public UserPaymentService(AlexContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetUserPaymentDto>>> GetAllUserPayments(int userId)
    {
        var serviceResponse = new ServiceResponse<List<GetUserPaymentDto>>();
        var payments = await _context.UserPayments.Where(c => c.UserId == userId).ToListAsync();
        serviceResponse.Data = _mapper.Map<List<GetUserPaymentDto>>(payments);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserPaymentDto>> GetUserPaymentById(int userId, int id)
    {
        var serviceResponse = new ServiceResponse<GetUserPaymentDto>();
        var payment = await _context.UserPayments.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        serviceResponse.Data = _mapper.Map<GetUserPaymentDto>(payment);
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> UpdateUserPayment(int userId, int id, PostPutUserPaymentDto updatedUserPayment)
    {
        var serviceResponse = new ServiceResponse<int>();
        var payment = await _context.UserPayments.
            FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        if (payment != null)
        {
            payment.PaymentType = updatedUserPayment.PaymentType;
            payment.AccountNo = updatedUserPayment.AccountNo;
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Adress not found";
        }
        await _context.SaveChangesAsync();
        serviceResponse.Data = payment.Id;
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> DeleteUserPayment(int userId, int id)
    {
        var serviceResponse = new ServiceResponse<int>();
        var payment = await _context.UserPayments.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        if (payment != null)
        {
            _context.UserPayments.Remove(payment);
            await _context.SaveChangesAsync();
            serviceResponse.Data = payment.Id;
        }
        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Adress not found";
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> AddUserPayment(int userId, PostPutUserPaymentDto newUserPayment)
    {
        var serviceResponse = new ServiceResponse<int>();
        var payment = _mapper.Map<UserPayment>(newUserPayment);
        payment.UserId = userId;
        await _context.UserPayments.AddAsync(payment);
        await _context.SaveChangesAsync();
        serviceResponse.Data = payment.Id;
        return serviceResponse;
    }
}