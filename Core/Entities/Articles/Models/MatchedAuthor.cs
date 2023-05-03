namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class MatchedAuthor
{
    public string? id { get; set; }
    public string name { get; set; }

    public MatchedAuthor(string value)
    {
        name = value;
    }

    public MatchedAuthor(string value, string? i = null)
    {
        name = value;
        id = i;
    }
}