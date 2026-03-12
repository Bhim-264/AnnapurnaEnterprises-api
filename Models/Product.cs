using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 999999999)]
    public decimal PricePerSqFt { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<ProductImage> Images { get; set; } = new();
}