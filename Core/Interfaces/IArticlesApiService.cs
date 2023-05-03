using EvolutionBoursiere.Core.Entities.Articles.Api;

namespace EvolutionBoursiere.Core.Interfaces;

public interface IArticlesApiService
{
    Task<ArticlesApiResponse> GetArticles(ArticlesApiConfiguration config);
}