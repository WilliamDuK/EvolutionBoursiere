using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using EvolutionBoursiere.Core.Entities;
using EvolutionBoursiere.Core.Settings;

namespace EvolutionBoursiere.Infrastructure.Services;

public class MongoDBService {

    private readonly IMongoCollection<HttpRequete> _httpRequetesCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _httpRequetesCollection = database.GetCollection<HttpRequete>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<HttpRequete>> GetAsync()
    {
        return await _httpRequetesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateAsync(HttpRequete httpRequete)
    {
        await _httpRequetesCollection.InsertOneAsync(httpRequete);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<HttpRequete> filter = Builders<HttpRequete>.Filter.Eq("Id", id);
        await _httpRequetesCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task ClearAsync()
    {
        FilterDefinition<HttpRequete> filter = Builders<HttpRequete>.Filter.Empty;
        await _httpRequetesCollection.DeleteManyAsync(filter);
        return;
    }
}