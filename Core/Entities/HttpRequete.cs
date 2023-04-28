using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EvolutionBoursiere.Core.Entities;

public class HttpRequete
{
    public HttpRequete(string id, string method, string path, string address, string host,
        string userArgent, string body, DateTime createdAt)
    {
        Id = id;
        Method = method;
        Path = path;
        Address = address;
        Host = host;
        UserAgent = UserAgent;
        Body = body;
        CreatedAt = createdAt;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("method")]
    public string? Method { get; set; }

    [BsonElement("path")]
    public string? Path { get; set; }

    [BsonElement("address")]
    public string? Address { get; set; }

    [BsonElement("host")]
    public string? Host { get; set; }

    [BsonElement("userAgent")]
    public string? UserAgent { get; set; }

    [BsonElement("body")]
    public string? Body { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
