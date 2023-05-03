namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Source
{
    public string domain { get; set; }
    public bool paywall { get; set; }

    public Source(string dom, bool pay)
    {
        domain = dom;
        paywall = pay;
    }
}