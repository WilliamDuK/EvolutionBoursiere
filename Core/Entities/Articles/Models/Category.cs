namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Category
{
    public string name { get; set; }

    public Category(string value)
    {         
        name = value;
    }
}