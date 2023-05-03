using EvolutionBoursiere.Core.Entities.Articles.Models;

namespace EvolutionBoursiere.Core.Entities.Articles.Api;

public class ArticlesApiResponse
{
    public int status { get; set; }
    public int numResults { get; set; }
    public List<Article> articles { get; set; }
}