using ITExperts.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITExperts.DAL.Context;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FilmCategory> FilmCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}