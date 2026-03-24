using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/movies
    // GET /api/movies?genre=Action
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieResponseDto>>> GetAll([FromQuery] string? genre)
    {
        var query = _context.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(m => m.Genre.ToLower() == genre.ToLower());

        var movies = await query
            .Select(m => ToDto(m))
            .ToListAsync();

        return Ok(movies);
    }

    // GET /api/movies/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieResponseDto>> GetById(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();
        return Ok(ToDto(movie));
    }

    // POST /api/movies
    [HttpPost]
    public async Task<ActionResult<MovieResponseDto>> Create(MovieCreateDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            Year = dto.Year,
            Director = dto.Director,
            Rating = dto.Rating
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, ToDto(movie));
    }

    // PUT /api/movies/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MovieCreateDto dto)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        movie.Title = dto.Title;
        movie.Genre = dto.Genre;
        movie.Year = dto.Year;
        movie.Director = dto.Director;
        movie.Rating = dto.Rating;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/movies/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static MovieResponseDto ToDto(Movie m) => new()
    {
        Id = m.Id,
        Title = m.Title,
        Genre = m.Genre,
        Year = m.Year,
        Director = m.Director,
        Rating = m.Rating
    };
}
