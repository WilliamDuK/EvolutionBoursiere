namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Coordinates
{
	public Coordinates()
	{
	}

	public Coordinates(double latitude, double longitude)
	{
		lat = latitude;
		lon = longitude;
	}

	public double lat { get; set; }
	public double lon { get; set; }
}