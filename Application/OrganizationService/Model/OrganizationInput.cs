using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrganizationService.Model
{
    public class OrganizationInput: IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string WebUrl { get; set; }

        [MaxLength(256)]
        public string ImageUrl { get; set; }

        public Guid? SuperUserId { get; set; }





    }
}