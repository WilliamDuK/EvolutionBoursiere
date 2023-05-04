namespace EvolutionBoursiere.Core.Entities.Articles.Api;

public class ArticlesApiConfiguration
{
    public string? q { get; set; } = null;

    public string? title { get; set; } = null;

    public string? desc { get; set; } = null;

    public string? content { get; set; } = null;

    public string? url { get; set; } = null;

    public string? from { get; set; } = null;

    public string? to { get; set; } = null;

    public string? addDateFrom { get; set; } = null;

    public string? addDateTo { get; set; } = null;

    public string? refreshDateFrom { get; set; } = null;

    public string? refreshDateTo { get; set; } = null;

    public List<string>? articleId { get; set; } = null; // TODO: Vérifier validité de la liste.

    public string? clusterId { get; set; } = null;

    public List<string>? medium { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? source { get; set; } = null; // TODO: Vérifier validité de la liste.

    public string? sourceGroup { get; set; } = null;

    public List<string>? excludeSource { get; set; } = null; // TODO: Vérifier validité de la liste.

    public bool? paywall { get; set; } = null;

    public List<string>? country { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? language { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? label { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? excludeLabel { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? byline { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? topic { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? category { get; set; } = null; // TODO: Vérifier validité de la liste.

    public string? journalistId { get; set; } = null;

    public List<string>? state { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? city { get; set; } = null; // TODO: Vérifier validité de la liste.

    public List<string>? area { get; set; } = null; // TODO: Vérifier validité de la liste.

    public string? location { get; set; } = null;

    public string? sortBy { get; set; } = null;

    public int? page { get; set; } = null;

    public int? size { get; set; } = null;

    public bool? showReprints { get; set; } = null;

    public bool? showNumResults { get; set; } = null;

    public string? type { get; set; } = null;

    public string? linkTo { get; set; } = null;

    public string? reprintGroupId { get; set; } = null;

    public List<string>? personWikidataId { get; set; } = null;

    public List<string>? personName { get; set; } = null;

    public List<string>? companyId { get; set; } = null;

    public string? companyName { get; set; } = null;

    public List<string>? companyDomain { get; set; } = null;

    public List<string>? companySymbol { get; set; } = null;

    public int? maxDistance { get; set; } = null;

    public float? lat { get; set; } = null;

    public float? lon { get; set; } = null;
}