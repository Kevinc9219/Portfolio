using Microsoft.EntityFrameworkCore;
using CvAnalyzer.Models;

namespace CvAnalyzer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CvAnalysis> CvAnalyses { get; set; }
}
