using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBoursiere.Core.Entities.Articles.Models;

public class Article
{
	public string url { get; set; } = "";
	public string authorsByline { get; set; } = "";
	public string articleId { get; set; } = "";
	public string clusterId { get; set; } = "";
	public Source source { get; set; } = new Source("", false);
	public string imageUrl { get; set; } = "";
	public string country { get; set; } = "";
	public string language { get; set; } = "";
	public DateTime pubDate { get; set; }
	public DateTime addDate { get; set; }
	public DateTime refreshDate { get; set; }
	public int score { get; set; }
	public string title { get; set; } = "";
	public string description { get; set; } = "";
	public string content { get; set; } = "";
	public string medium { get; set; } = "";
	[NotMapped]
	public List<string> links { get; set; } = new List<string>();
	public List<Label> labels { get; set; } = new List<Label>();
	public List<MatchedAuthor> matchedAuthors { get; set; } = new List<MatchedAuthor>();
	public string claim { get; set; } = "";
	public string verdict { get; set; } = "";
	public List<Keyword> keywords { get; set; } = new List<Keyword>();
	public List<Topic> topics { get; set; } = new List<Topic>();
	public List<Category> categories { get; set; } = new List<Category>();
	public List<Entity> entities { get; set; } = new List<Entity>();
	public List<Company> companies { get; set; } = new List<Company>();
	public Sentiment sentiment { get; set; } = new Sentiment(0, 0, 1);
	public string summary { get; set; } = "";
	public string translation { get; set; } = "";
	public List<Location> locations { get; set; } = new List<Location>();
	public bool reprint { get; set; }
	public string reprintGroupId { get; set; } = "";
	public List<Place> places { get; set; } = new List<Place>();
	public List<Person> people { get; set; } = new List<Person>();

	public class Category
	{
		public Category()
		{
		}

		public Category(string value)
		{         
			name = value;
		}

		[Key]
		public string? name { get; set; }
	}

	public class Company
	{
		public Company()
		{
		}

		public Company(string i, string value, List<string>? dom = null, List<string>? sym = null)
		{
			id = i;
			name = value;
			domains = dom == null ? new List<string>() : dom;
			symbols = sym == null ? new List<string>() : sym;
		}

		public string? id { get; set; }
		public string? name { get; set; }
		[NotMapped]
		public List<string>? domains { get; set; }
		[NotMapped]
		public List<string>? symbols { get; set; }
	}

	[PrimaryKey(nameof(data), nameof(type))]
	public class Entity
	{
		public Entity()
		{
		}

		public Entity(string d, string t, int m)
		{
			data = d;
			type = t;
			mentions = m;
		}

		public string? data { get; set; }
		public string? type { get; set; }
		public int mentions { get; set; }
	}

	public class Keyword
	{
		public Keyword()
		{
		}

		public Keyword(string value, double wg)
		{
			name = value;
			weight = wg;
		}

		[Key]
		public string? name { get; set; }
		public double weight { get; set; }
	}

	public class Label
	{
		public Label()
		{
		}

		public Label(string value)
		{
			name = value;
		}

		[Key]
		public string? name { get; set; }
	}

	[PrimaryKey(nameof(country), nameof(city), nameof(state), nameof(county), nameof(area))]
	public class Location
	{
		public Location()
		{
		}

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

		public string? country { get; set; }
		public string? city { get; set; }
		public string? state { get; set; }
		public string? county { get; set; }
		public string? area { get; set; }
	}

	public class MatchedAuthor
	{
		public MatchedAuthor()
		{
		}

		public MatchedAuthor(string value)
		{
			name = value;
		}

		public MatchedAuthor(string value, string? i = null)
		{
			name = value;
			id = i;
		}

		public string? id { get; set; }
		public string? name { get; set; }
	}

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

		[Key]
		public string? wikidataId { get; set; }
		public string? name { get; set; }
	}

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

		[Key]
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

		[PrimaryKey(nameof(lat), nameof(lon))]
		public class Coordinates
		{
			public Coordinates()
			{
			}

			public Coordinates(double latitude, double longitude)
			{
				lat = latitude;
				lon = longitude;
			}

			public double lat { get; set; }
			public double lon { get; set; }
		}
	}

	[PrimaryKey(nameof(positive), nameof(negative), nameof(neutral))]
	public class Sentiment
	{
		public Sentiment()
		{
		}

		public Sentiment(double pos, double neg, double neu)
		{
			// TODO: Les trois paramètres doivent égaler 1.00.
			positive = pos;
			negative = neg;
			neutral = neu;
		}

		public double positive { get; set; }
		public double negative { get; set; }
		public double neutral { get; set; }
	}

	public class Source
	{
		public Source()
		{
		}

		public Source(string dom, bool pay)
		{
			domain = dom;
			paywall = pay;
		}

		[Key]
		public string? domain { get; set; }
		public bool paywall { get; set; }
	}

	public class Topic
	{
		public Topic()
		{
		}

		public Topic(string value)
		{
			name = value;
		}

		[Key]
		public string? name { get; set; }
	}
}