using System;

namespace Infrastructure.Common.Domain.Contracts
{
    public interface IAuditable
    {
       
            DateTimeOffset CreatedDate { get; set; }
            string CreatedBy { get; set; }
            DateTimeOffset? ModifiedDate { get; set; }
            string ModifiedBy { get; set; }


    }  


}
