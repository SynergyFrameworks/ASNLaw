using Infrastructure.Common.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;

namespace Domain.Parse.Model
{
    [BsonCollection("Excel Results")]
    public class ExcelResult : IDocument
    {
        public ExcelResult() { }
    
        public Guid Id { get; set; }
  
        public int Version { get; set; }

        // Your custom properties.
        public ObjectId ParseId { get; set; }
        public DateTime DateTimeUTC { get; set; }
        public ExcelFileResult ExcelFileResult { get; set; }
    }

    public class ExcelFileResult
    {
        public ExcelFileResult() { }

        public string FileName { get; set; }
        public List<ExcelWorkSheet> WorkSheets { get; set; }
    }

    public class ExcelWorkSheet
    {
        public ExcelWorkSheet() { }

        public string SheetName { get; set; }
        public List<string> Headers { get; set; }
        public List<ExcelContent> Contents { get; set; }
    }

    public class ExcelContent
    {
        public ExcelContent() { }

        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string Value { get; set; }
        public string TypeOfValue { get; set; }
        public string Label { get; set; }
    }
}
