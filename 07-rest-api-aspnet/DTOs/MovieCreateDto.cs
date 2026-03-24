using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs;

public class MovieCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int Year { get; set; }

    [Required]
    [MaxLength(200)]
    public string Director { get; set; } = string.Empty;

    [Range(0.0, 10.0)]
    public double Rating { get; set; }
}
