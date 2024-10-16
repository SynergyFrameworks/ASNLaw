using Domain.Parse.Model;
using System;
using System.Collections.Generic;

namespace ParseService.Dtos
{
    public class ParseDto
    {

        /// <summary>
        /// Used as reference
        /// </summary>
        public Guid TransactionID { get; init; }
        public Guid TaskID { get; init; }
        public Guid ProjectID { get; init; }
        public Guid UserId { get; init; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<ParseParameter> Parameters { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<Concept> Concepts { get; set; }
        public string strContent { get; set; }

    }
}
