namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Category
{
    public Category()
    {
    }

    public Category(string value)
    {         
        name = value;
    }

    public string? name { get; set; }
}