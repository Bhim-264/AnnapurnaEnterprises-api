using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.Models;

public class Review
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;

    [Range(0, 5)]
    public decimal Rating { get; set; }  // decimal allowed like 4.5

    public bool IsApproved { get; set; } = true; // you can later make admin approve

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}