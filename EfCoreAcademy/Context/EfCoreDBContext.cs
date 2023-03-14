using EfCoreAcademy.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreAcademy.Context;

public class EfCoreDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Class> Classes { get; set; }

    public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=EfCoreAcademy.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().HasKey(e => e.Id);
        modelBuilder.Entity<Student>().HasKey(e => e.Id);
        modelBuilder.Entity<Professor>().HasKey(e => e.Id);
        modelBuilder.Entity<Class>().HasKey(e => e.Id);

        modelBuilder.Entity<Student>().HasOne(e => e.Address);
        modelBuilder.Entity<Professor>().HasOne(e => e.Address);

        modelBuilder.Entity<Class>().HasMany(e => e.Students).WithMany(e => e.Classes);
        modelBuilder.Entity<Class>().HasOne(e => e.Professor).WithMany(e => e.Classes);

        base.OnModelCreating(modelBuilder);
    }
}