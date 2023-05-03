namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Company
{
	public string id { get; set; }
	public string name { get; set; }
	public List<string> domains { get; set; }
	public List<string> symbols { get; set; } = new List<string>();
}