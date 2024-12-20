namespace backend.DTOs.UserAdress;

public class PostUserAdressDto
{
    
    public string AdressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string Telephone { get; set; } = null!;
}