namespace EvolutionBoursiere.Core.Entities;

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

    public string? articleId { get; set; } = null;

    public string? clusterId { get; set; } = null;

    public string? medium { get; set; } = null;

    public string? source { get; set; } = null;

    public string? sourceGroup { get; set; } = null;

    public string? excludeSource { get; set; } = null;

    public bool? paywall { get; set; } = null;

    public string? country { get; set; } = null;

    public string? language { get; set; } = null;

    public string? label { get; set; } = null;

    public string? excludeLabel { get; set; } = null;

    public string? byline { get; set; } = null;

    public string? topic { get; set; } = null;

    public string? category { get; set; } = null;

    public string? journalistId { get; set; } = null;

    public string? state { get; set; } = null;

    public string? city { get; set; } = null;

    public string? area { get; set; } = null;

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