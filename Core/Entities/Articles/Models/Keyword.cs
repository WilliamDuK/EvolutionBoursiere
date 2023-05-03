namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Keyword
{
    public Keyword()
    {
    }

    public Keyword(string value, double wg)
    {
        name = value;
        weight = wg;
    }

    public string? name { get; set; }
    public double weight { get; set; }
}