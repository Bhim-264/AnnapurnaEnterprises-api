using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.DTOs;

public class InquiryCreateDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, RegularExpression(@"^\d{10}$")]
    public string Mobile { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Message { get; set; }
}