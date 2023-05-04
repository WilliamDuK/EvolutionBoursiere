using System.Text;
using System.Text.Json;
using EvolutionBoursiere.Core.Entities.Articles.Api;
using EvolutionBoursiere.Core.Interfaces;
using System.Globalization;

namespace EvolutionBoursiere.Infrastructure.Services;

public class ArticlesApiService : IArticlesApiService
{
    private static readonly HttpClient _client;

    static ArticlesApiService()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.goperigon.com/v1/all")
        };
    }

    public async Task<ArticlesApiResponse> GetArticles(ArticlesApiConfiguration config)
    {
        var url = string.Format(GetUri(config));
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ArticlesApiResponse>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) ?? new ArticlesApiResponse();
        }
        else
        {
            throw new HttpRequestException(response.ReasonPhrase);
        }
    }

    private string GetUri(ArticlesApiConfiguration config)
    {
        string uri = "";
        bool isFirst = true;

        foreach (var property in config.GetType().GetProperties())
        {
            var value = property.GetValue(config, null);
            if (value != null)
            {
                if (isFirst)
                {
                    uri += $"apiKey=[KEY]"; // TODO: Implémenter la clé API
                    isFirst = false;
                }

                if (property.PropertyType == typeof(Single?))
                {
                    float floatValue = (float)value;
                    NumberFormatInfo nfi = new NumberFormatInfo();
                    nfi.NumberDecimalSeparator = ".";
                    uri += $"&{property.Name}={floatValue.ToString(nfi)}";
                }
                else if (property.PropertyType != typeof(List<string>))
                {
                    uri += $"&{property.Name}={value}";
                }
                else
                {
                    List<string> strList = (List<string>)value;
                    foreach (var str in strList)
                    {
                        uri += $"&{property.Name}={str}";
                    }
                }
            }
        }

        return $"?{encodeUri(uri)}";
    }

    private string encodeUri(string uri)
    {
        StringBuilder sb = new StringBuilder(uri);

        sb.Replace("[", "%5B");
        sb.Replace("]", "%5D");
        sb.Replace(" ", "%20");
        sb.Replace("\"", "%22");
        sb.Replace("(", "%28");
        sb.Replace(")", "%29");
        sb.Replace("*", "%2A");
        sb.Replace("?", "%3F");
        sb.Replace("True", "true");
        sb.Replace("False", "false");

        return sb.ToString();
    }
}