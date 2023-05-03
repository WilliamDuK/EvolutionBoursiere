namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Entity
{
    public string data { get; set; }
    public string type { get; set; }
    public int mentions { get; set; }

    public Entity(string d, string t, int m)
    {
        data = d;
        type = t;
        mentions = m;
    }
}