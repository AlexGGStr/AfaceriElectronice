namespace backend.DTOs.UserPayment;

public class GetUserPaymentDto
{
    public int Id { get; set; }
    
    public string? PaymentType { get; set; }

    public string? AccountNo { get; set; }
}