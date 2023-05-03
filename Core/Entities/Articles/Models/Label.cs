namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Label
{
	public string name { get; set; }

	public Label(string value)
	{
		name = value;
	}
}