using Microsoft.EntityFrameworkCore;
using StageTracker.Models;
namespace StageTracker.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<StageDay> StageDays { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Competence> Competences { get; set; }
    public DbSet<DayCompetence> DayCompetences { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DayCompetence>().HasKey(dc => new { dc.StageDayId, dc.CompetenceId });
        modelBuilder.Entity<Competence>().HasData(
            new Competence { Id = 1, Name = "Analyseren", Description = "Problemen analyseren en opdelen" },
            new Competence { Id = 2, Name = "Programmeren", Description = "Code schrijven en testen" },
            new Competence { Id = 3, Name = "Communiceren", Description = "Communiceren met collega's en klanten" },
            new Competence { Id = 4, Name = "Leren", Description = "Nieuwe technologieën leren" },
            new Competence { Id = 5, Name = "Samenwerken", Description = "Teamwork en samenwerken" }
        );
    }
}
