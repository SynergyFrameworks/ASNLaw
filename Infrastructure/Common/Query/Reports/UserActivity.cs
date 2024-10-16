using System;

namespace Infrastructure.Query.Performance.Reporting
{
    public class UserActivity
    {
        public string Username { get; set; }
        public string ActivityText { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
