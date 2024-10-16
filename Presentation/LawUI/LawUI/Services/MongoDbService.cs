using LawUI.Domain.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawUI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _documents;
        private readonly IMongoCollection<SpeechToTextEntry> _speechToTextEntries;

        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
            _documents = database.GetCollection<BsonDocument>("Documents");
            _speechToTextEntries = database.GetCollection<SpeechToTextEntry>("SpeechToTextEntries");
        }

        public async Task<string> InsertDocumentAsync(string collectionName, BsonDocument document)
        {
            await _documents.InsertOneAsync(document);
            return document["_id"].ToString();
        }

        public async Task<DocumentEntry> GetDocumentById(string documentId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(documentId));
            var document = await _documents.Find(filter).FirstOrDefaultAsync();
            if (document != null)
            {
                return new DocumentEntry
                {
                    Id = document["_id"].ToString(),
                    Content = document["Content"].AsString,
                    Placeholders = document["Placeholders"].AsBsonArray.Select(p => new DocumentField
                    {
                        Key = p["Key"].AsString,
                        Value = p["Value"].AsString,
                        FieldType = p["FieldType"].AsString,
                        IsRequired = p["IsRequired"].AsBoolean
                    }).ToList()
                };
            }
            return null;
        }

        public async Task<string> InsertSpeechToTextEntryAsync(SpeechToTextEntry entry)
        {
            await _speechToTextEntries.InsertOneAsync(entry);
            return entry.Id.ToString();
        }

        public async Task<List<SpeechToTextEntry>> GetSpeechToTextEntriesByUserIdAsync(string userId)
        {
            var filter = Builders<SpeechToTextEntry>.Filter.Eq(x => x.UserId, userId);
            return await _speechToTextEntries.Find(filter).ToListAsync();
        }
    }

    public class DocumentEntry
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public List<DocumentField> Placeholders { get; set; }
    }

    public class SpeechToTextEntry
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string Transcription { get; set; }
        public DateTime Timestamp { get; set; }
    }
}