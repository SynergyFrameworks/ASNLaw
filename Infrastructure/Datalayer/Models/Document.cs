using Datalayer.Contracts;
using MongoDB.Bson;
using System;

namespace Datalayer.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId ParseId { get; set; }

        public DateTime DateTimeUTC => ParseId.CreationTime;
    }
}
