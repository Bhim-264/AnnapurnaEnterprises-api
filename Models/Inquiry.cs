using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.Models;

public class Inquiry
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, RegularExpression(@"^\d{10}$")]
    public string Mobile { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}