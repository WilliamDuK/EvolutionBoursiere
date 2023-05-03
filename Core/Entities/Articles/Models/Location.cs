namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Location
{
	public string country { get; set; }
	public string city { get; set; }
	public string? state { get; set; }
	public string? county { get; set; }
	public string? area { get; set; }

	public Location(string co, string ci)
	{
		country = co;
		city = ci;
	}

	public Location(string co, string ci, string? s = null)
	{
		country = co;
		city = ci;
		state = s;
	}

	public Location(string co, string ci, string? s = null, string? cu = null)
	{
		country = co;
		city = ci;
		state = s;
		county = cu;
	}

	public Location(string co, string ci, string? s = null, string? cu = null, string? a = null)
	{
		country = co;
		city = ci;
		state = s;
		county = cu;
		area = a;
	}
}