using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EvolutionBoursiere.Core.Entities;

// TODO: Implémenter HttpRequete à partir d'une extension de HttpRequest.
public class HttpRequete
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("method")]
    public string? Method { get; set; }

    [BsonElement("path")]
    public string? Path { get; set; }

    [BsonElement("host")]
    public string? Host { get; set; }

    [BsonElement("body")]
    public string? Body { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
