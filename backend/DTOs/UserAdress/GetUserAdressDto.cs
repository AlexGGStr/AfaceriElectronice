namespace backend.DTOs.UserAdress;

public class GetUserAdressDto
{
    public int Id { get; set; }
    
    public string AdressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string Telephone { get; set; } = null!;
}