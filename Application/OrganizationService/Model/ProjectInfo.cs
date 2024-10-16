using System;
using System.ComponentModel.DataAnnotations;
using Datalayer.Contracts;

namespace OrganizationService.Model
{
    public class ProjectInfo
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid GroupId { get; set; }

        public string ImageUrl { get; set; }

        public GroupInfo Group { get; set; }
    }
}
