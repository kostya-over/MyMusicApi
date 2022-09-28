using System.ComponentModel.DataAnnotations;

namespace MyMusic.Api.Resources;

public class SaveArtistResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}