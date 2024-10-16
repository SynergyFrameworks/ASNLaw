namespace Infrastructure.Common.Domain.Mail
{
    public class BaseMailParameters : IMailParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
