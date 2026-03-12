using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnapurnaEnterprises.Api.Models;

public class PreviousWorkImage
{
    public int Id { get; set; }

    public int PreviousWorkId { get; set; }

    [Required, MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonIgnore]
    public PreviousWork? PreviousWork { get; set; }
}