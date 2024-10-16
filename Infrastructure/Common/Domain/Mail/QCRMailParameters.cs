namespace Infrastructure.Common.Domain.Mail
{
    public class QCRMailParameters : BaseMailParameters
    {
        public string Link { get; set; }
        public string ErrorMessage { get; set; }
    }
}
