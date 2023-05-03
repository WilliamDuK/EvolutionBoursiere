using EvolutionBoursiere.Core.Entities.Articles.Models;

namespace EvolutionBoursiere.Core.Entities.Articles.Api;

public class ArticlesApiResponse
{
    public int status { get; set; } = 200;
    public int numResults { get; set; } = 0;
    public List<Article> articles { get; set; } = new List<Article>();
}