namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Sentiment
{
    public Sentiment()
    {
    }

    public Sentiment(double pos, double neg, double neu)
    {
        // TODO: Les trois paramètres doivent égaler 1.00.
        positive = pos;
        negative = neg;
        neutral = neu;
    }

    public double positive { get; set; }
    public double negative { get; set; }
    public double neutral { get; set; }
}