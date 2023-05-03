namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Source
{
    public Source()
    {
    }

    public Source(string dom, bool pay)
    {
        domain = dom;
        paywall = pay;
    }

    public string? domain { get; set; }
    public bool paywall { get; set; }
}