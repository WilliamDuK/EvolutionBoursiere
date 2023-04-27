using EvolutionBoursiere.Models;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBoursiere.Infrastructure.Data;

public class CoteContext : DbContext
{
    public CoteContext(DbContextOptions<CoteContext> options)
        : base(options)
    {
    }

    public DbSet<CoteBoursiere> CotesBoursieres { get; set; } = null!;
}