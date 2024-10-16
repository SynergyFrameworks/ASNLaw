using OrganizationService.Contracts;
using Infrastructure.CQRS.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrganizationService.Model
{
    public class DefaultSearch<T> : IDefaultSearch<T> where T : class
    {
        [Required]
        public int CurrentPage { get; set; }

        [Required]
        public int PageCount { get; set; } = 10;

        public string NameSearch { get; set; }

        public string DescriptionSearch { get; set; }

        public SearchRange CreatedDateSearchRange { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotalOfRecords { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<T> Results { get; set; }
        
        public IList<SortOption> SortOptions { get; set; }
    }
}
