using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.DTOs;

public class ReviewCreateDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;

    [Range(0, 5)]
    public decimal Rating { get; set; }
}