using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnapurnaEnterprises.Api.Models;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Required, MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonIgnore]
    public Product? Product { get; set; }
}