namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Place
{
	public Place()
	{
	}

	public Place(string id, string st, string post, string c, string cCode, Coordinates coord)
	{
		osmId = id;
		state = st;
		postcode = post;
		country = c;
		countryCode = cCode;
		coordinates = coord;
	}

	public Place(string id, string s, string post, string c, string cCode, Coordinates coord,
		string? r = null, string? su = null, string? ci = null, string? co = null, string? n = null,
		string? a = null, string? q = null)
	{
		osmId = id;
		state = s;
		postcode = post;
		country = c;
		countryCode = cCode;
		coordinates = coord;
		road = r;
		suburb = su;
		city = ci;
		county = co;
		neighbourhood = n;
		amenity = a;
		quarter = q;
	}

	public string? osmId { get; set; }
	public string? road { get; set; }
	public string? suburb { get; set; }
	public string? city { get; set; }
	public string? county { get; set; }
	public string? state { get; set; }
	public string? postcode { get; set; }
	public string? country { get; set; }
	public string? countryCode { get; set; }
	public string? neighbourhood { get; set; }
	public string? amenity { get; set; }
	public Coordinates? coordinates { get; set; }
	public string? quarter { get; set; }
}