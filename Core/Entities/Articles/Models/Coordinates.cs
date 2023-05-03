namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Coordinates
{
	public double lat { get; set; }
	public double lon { get; set; }

	public Coordinates(double latitude, double longitude)
	{
		lat = latitude;
		lon = longitude;
	}
}