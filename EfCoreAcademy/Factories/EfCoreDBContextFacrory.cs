using EfCoreAcademy.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfCoreAcademy.Factories;

public class EfCoreDbContextFactory : IDesignTimeDbContextFactory<EfCoreDbContext>
{
    public EfCoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>();
        optionsBuilder.UseSqlite("Filename=EfCoreAcademy.db");
        return new EfCoreDbContext(optionsBuilder.Options);
    }
}