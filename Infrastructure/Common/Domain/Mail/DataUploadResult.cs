using Infrastructure.Common.Domain.Performance;

namespace Infrastructure.Common.Domain.Mail
{
    public class DataUploadResult : DataUpload, IMailParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
