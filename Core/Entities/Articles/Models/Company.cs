namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Company
{
	public string id { get; set; }
	public string name { get; set; }
	public List<string> domains { get; set; }
	public List<string> symbols { get; set; }

	public Company(string i, string value, List<string>? dom = null, List<string>? sym = null)
	{
		id = i;
		name = value;
		domains = dom == null ? new List<string>() : dom;
		symbols = sym == null ? new List<string>() : sym;
	}
}