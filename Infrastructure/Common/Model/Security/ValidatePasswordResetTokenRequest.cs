using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model.Security
{
    public class ValidatePasswordResetTokenRequest
    {
        public string Token { get; set; }
    }
}
