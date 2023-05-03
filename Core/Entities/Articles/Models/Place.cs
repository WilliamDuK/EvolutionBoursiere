namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Place
{
	public string osmId { get; set; }
	public string road { get; set; }
	public string suburb { get; set; }
	public string city { get; set; }
	public string county { get; set; }
	public string state { get; set; }
	public string postcode { get; set; }
	public string country { get; set; }
	public string countryCode { get; set; }
	public string neighbourhood { get; set; }
	public Coordinates coordinates { get; set; }
	public string quarter { get; set; }
}