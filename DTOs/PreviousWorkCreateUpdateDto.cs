using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.DTOs;

public class PreviousWorkCreateUpdateDto
{
    [Required, MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public List<string> ImageUrls { get; set; } = new();
}