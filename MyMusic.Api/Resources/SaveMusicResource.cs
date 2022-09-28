using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyMusic.Api.Resources;

public class SaveMusicResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [Range(1, Int32.MaxValue)]
    public int ArtistId { get; set; }
}