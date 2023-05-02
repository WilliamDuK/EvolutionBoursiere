using System.Text.Json;
using EvolutionBoursiere.Core.Entities;
using EvolutionBoursiere.Core.Interfaces;

namespace EvolutionBoursiere.Infrastructure.Services;

public class ArticlesApiService: IArticlesApiService
{
    private static readonly HttpClient _client;

    static ArticlesApiService()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.goperigon.com/v1/all")
        };
    }

    public async Task<List<Stock>> GetArticles(ArticlesApiConfiguration config)
    {
        var url = string.Format(GetUri(config));
        var result = new List<Stock>();
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
    
            result = JsonSerializer.Deserialize<List<Stock>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        else
        {
            throw new HttpRequestException(response.ReasonPhrase);
        }
    
        return result ?? new List<Stock>();
    }

    private string GetUri(ArticlesApiConfiguration config)
    {
        string uri = "";
        bool isFirst = true;

        foreach(var property in config.GetType().GetProperties())
        {
            if (property != null)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    uri += "&";
                }
                
                if (property.PropertyType != typeof(List<string>))
                {
                    uri += $"{property.Name}={property.GetValue(config, null)}";
                }
                else
                {
                    List<string> strList = (List<string>?)property.GetValue(config, null) ?? new List<string>();
                    foreach (var str in strList)
                    {
                        if (str != strList.First())
                        {
                            uri += "&";
                        }
                        uri += $"{property.Name}={str}";
                    }
                }
            }
        }
        
        return System.Web.HttpUtility.UrlEncode(uri);
    }
}