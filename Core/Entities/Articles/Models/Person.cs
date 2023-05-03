namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Person
{
	public string wikidataId { get; set; }
	public string name { get; set; }

	public Person(string wiki, string value)
	{
		wikidataId = wiki;
		name = value;
	}
}