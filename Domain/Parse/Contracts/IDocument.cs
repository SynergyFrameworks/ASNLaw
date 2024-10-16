using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Parse.Contracts
{

    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId ParseId { get; set; }

        DateTime DateTimeUTC { get; }
    }

}
