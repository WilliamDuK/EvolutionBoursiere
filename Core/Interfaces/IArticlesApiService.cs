using EvolutionBoursiere.Core.Entities;

namespace EvolutionBoursiere.Core.Interfaces;

public interface IArticlesApiService
{
    Task<List<Stock>> GetArticles(ArticlesApiConfiguration config);
}