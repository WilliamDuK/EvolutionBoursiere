using Microsoft.EntityFrameworkCore;

namespace EvolutionBoursiere.Models;

public class CoteContext : DbContext
{
    public CoteContext(DbContextOptions<CoteContext> options)
        : base(options)
    {
    }

    public DbSet<CoteBoursiere> CotesBoursieres { get; set; } = null!;
}