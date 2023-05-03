namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Person
{
	public Person()
	{
	}

	public Person(string wiki, string value)
	{
		wikidataId = wiki;
		name = value;
	}

	public string? wikidataId { get; set; }
	public string? name { get; set; }
}