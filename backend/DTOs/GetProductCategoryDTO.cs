namespace backend.DTOs;

public class GetProductCategoryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Descriprion { get; set; }
}