

namespace Infrastructure.Common.Domain.Mail
{
    public class ISLSynchronizationSaveResult : IMailParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string SaveOutcome { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }

        public string WorkflowType { get; set; } = "ISL Synchronization";
    }
}
