using Microsoft.EntityFrameworkCore;
using AiTaakplanner.Models;

namespace AiTaakplanner.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<TaskItem> Tasks { get; set; }
}
