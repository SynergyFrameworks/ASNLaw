using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Domain.Mail
{
    public class WorkflowInstanceSaveResult: IMailParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string SaveOutcome { get; set; }
        public string WorkflowName { get; set; }
        public string WorkflowType { get; set; }
    }
}
