namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Article
{
	public string url { get; set; }
	public string authorsByline { get; set; }
	public string articleId { get; set; }
	public string clusterId { get; set; }
	public Source source { get; set; }
	public string imageUrl { get; set; } = "";
	public string country { get; set; }
	public string language { get; set; }
	public DateTime pubDate { get; set; }
	public DateTime addDate { get; set; }
	public DateTime refreshDate { get; set; }
	public int score { get; set; }
	public string title { get; set; }
	public string description { get; set; }
	public string content { get; set; }
	public string medium { get; set; }
	public List<string> links { get; set; }
	public List<Label> labels { get; set; } = new List<Label>();
	public List<MatchedAuthor> matchedAuthors { get; set; } = new List<MatchedAuthor>();
	public string claim { get; set; } = "";
	public string verdict { get; set; } = "";
	public List<Keyword> keywords { get; set; }
	public List<Topic> topics { get; set; } = new List<Topic>();
	public List<Category> categories { get; set; } = new List<Category>();
	public List<Entity> entities { get; set; }
	public List<Company> companies { get; set; } = new List<Company>();
	public Sentiment sentiment { get; set; }
	public string summary { get; set; }
	public string translation { get; set; } = "";
	public List<Location> locations { get; set; } = new List<Location>();
	public bool reprint { get; set; }
	public string reprintGroupId { get; set; }
	public List<Place> places { get; set; } = new List<Place>();
	public List<Person> people { get; set; } = new List<Person>();
}