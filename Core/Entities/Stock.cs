using System.ComponentModel.DataAnnotations;

namespace EvolutionBoursiere.Core.Entities;

public class Stock
{
    [Key]
    public long id { get; set; }

    public string? Titre { get; set; }
    
    public string? Description { get; set; }
}
