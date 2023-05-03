namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Topic
{
    public string name { get; set; }

    public Topic(string value)
    {
        name = value;
    }
}