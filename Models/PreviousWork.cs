using System.ComponentModel.DataAnnotations;

namespace AnnapurnaEnterprises.Api.Models;

public class PreviousWork
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<PreviousWorkImage> Images { get; set; } = new();
}