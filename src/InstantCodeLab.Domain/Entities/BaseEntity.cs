using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace InstantCodeLab.Domain.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
