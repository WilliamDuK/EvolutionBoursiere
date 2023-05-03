namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Topic
{
    public Topic()
    {
    }

    public Topic(string value)
    {
        name = value;
    }

    public string? name { get; set; }
}