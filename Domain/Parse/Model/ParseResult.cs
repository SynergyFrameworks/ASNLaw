using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Infrastructure.Common.Attributes;
using MongoDbGenericRepository.Models;

namespace Domain.Parse.Model
{
    [BsonCollection("ParseResults")]
    public class ParseResult : IDocument
    {

        public ObjectId ParseId { get; set; }      
        public Guid TransactionId { get; init; }
        public Guid TaskId { get; init; }
        public Guid ProjectId { get; init; }
        public Guid UserId { get; init; }
        public IDictionary<String, Library> Libraries { get; init; }
        public ICollection<ParseSegmentResult> ParseSegmentResults { get; set; }
        public DateTime DateTimeUTC { get; set; }
        public Guid Id { get ; set; }
        public int Version { get ; set; }

        public static implicit operator Task<object>(ParseResult v)
        {
            throw new NotImplementedException();
        }



    }
}

