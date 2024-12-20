using AutoMapper;
using backend.DTOs.Email;
using backend.DTOs.Image;
using backend.DTOs.Order;
using backend.DTOs.Product;
using backend.DTOs.UserAdress;
using backend.DTOs.UserPayment;
using backend.Models;
using backend.Services.EmailService;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PlaceOrder;

public class PlaceOrderService : IPlaceOrderService
{
    private readonly AlexContext _context;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

     public PlaceOrderService(AlexContext context, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
     {
         _context = context;
         _mapper = mapper;
         _emailService = emailService;
         _httpContextAccessor = httpContextAccessor;
     }
    // public PlaceOrderService(LicentaAlexContext context, IMapper mapper, IEmailService emailService)
    // {
    //     _context = context;
    //     _mapper = mapper;
    //     _emailService = emailService;
    // }
    
    public async Task<ServiceResponse<int>> PlaceOrder(int UserId, PlaceOrderDto orderDto)
    {
        var response = new ServiceResponse<int>();
        var r = _httpContextAccessor.HttpContext.Request;
        try
        {
            var cartItems = await _context.CartItems.Include(p => p.Product).Include(p => p.Product.Images) .Where(c => c.UserId == UserId).ToListAsync();
            var orderItems = cartItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
            });

            var order = new OrderDetail
            {
                UserId = UserId,
                OrderItems = orderItems.ToList(),
                Total = orderDto.total,
                Adress = _mapper.Map<OrderAdress>(orderDto.adress),
                Payment = _mapper.Map<OrderPayment>(orderDto.payment)
            };
            
            


            var user = await _context.Users.Where(u => u.Id == UserId).FirstOrDefaultAsync();
            String orderedItems = "";
            foreach (var item in cartItems)
            {
                orderedItems += $" <img src=\"{r.Scheme}://{r.Host}{r.PathBase}/Images/{item.Product.Images.ElementAt(0).ImageName}\" />  <p>{item.Product.Name} x {item.Quantity}</p>";
            }
            String emailtext =
                $"Hello {user.Username}, <br /> Your order has been placed successfully." +
                $"The items that you ordered:" +
                $"{orderedItems}" +
                $" <br /> Thank you for choosing us! <br /> <br /> Best regards, <br /> Licenta Alex Team";
            _emailService.SendEmail(new Email
            {
                To = user.Email,
                Subject = "Order placed",
                Body = emailtext
            });
            
            await _context.OrderDetails.AddAsync(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetOrdersDto>>> GetOrdersByUser(int UserId)
    {
        var response = new ServiceResponse<List<GetOrdersDto>>();
        var data = await _context.OrderDetails.Include(o => o.Adress)
            .Include(o => o.Payment)
            .Include(o => o.OrderItems).ThenInclude(a => a.Product)
            .Where(o => o.UserId == UserId).ToListAsync();

        var data2 = data
            .Select(o => new GetOrdersDto
            {
                Adress = _mapper.Map<GetUserAdressDto>(o.Adress),
                Payment = _mapper.Map<GetUserPaymentDto>(o.Payment),
                OrderItems = o.OrderItems.Select(i => new OrderItemDto
                {
                    Product = new GetProductDto
                    {
                        CategoryId = i.Product.CategoryId,
                        Description = i.Product.Description,
                        Id = i.Product.Id,
                        Images = i.Product.Images.Select(img => new GetImageDto
                        {
                            ProductId = img.ProductId,
                            ImageName = img.ImageName
                        }).ToList(),
                        Name = i.Product.Name,
                        Price = i.Product.Price,
                        Quantity = i.Product.Quantity
                    },
                    Quantity = i.Quantity
                }).ToList(),
                Total = o.Total ?? 0
            })
            .ToList();

        response.Data = data2;

        
        return response;
    }
}