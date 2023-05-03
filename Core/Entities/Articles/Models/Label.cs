namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Label
{
	public Label()
	{
	}

	public Label(string value)
	{
		name = value;
	}

	public string? name { get; set; }
}