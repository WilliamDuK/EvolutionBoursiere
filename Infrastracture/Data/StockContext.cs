using EvolutionBoursiere.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBoursiere.Infrastructure.Data;

public class StockContext : DbContext
{
    public StockContext(DbContextOptions<StockContext> options)
        : base(options)
    {
    }

    public DbSet<Stock> Stocks { get; set; } = null!;
}