using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.DTOs;

public class ProductCreateUpdateDto
{
    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 999999999)]
    public decimal PricePerSqFt { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    // up to 5
    public List<string> ImageUrls { get; set; } = new();
}