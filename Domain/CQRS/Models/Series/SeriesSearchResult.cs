﻿using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;

namespace Infrastructure.CQRS.Models
{
    public class SeriesSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;
    }
}
