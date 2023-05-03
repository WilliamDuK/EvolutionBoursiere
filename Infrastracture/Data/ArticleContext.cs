using EvolutionBoursiere.Core.Entities.Articles.Models;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBoursiere.Infrastructure.Data;

public class ArticleContext : DbContext
{
    public ArticleContext(DbContextOptions<ArticleContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } = null!;
}