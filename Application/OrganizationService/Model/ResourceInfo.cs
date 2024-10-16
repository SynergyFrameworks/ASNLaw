using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class ResourceInfo
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string ResourceType { get; set; }

        public string JsonValues { get; set; }

        public Guid? ModuleId { get; set; }

        public ModuleInfo Module { get; set; }



    }
}
