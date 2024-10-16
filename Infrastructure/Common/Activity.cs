using System;
using Infrastructure.Common.Domain.Users;
using Infrastructure;

namespace Infrastructure
{
    public class Activity : BaseObject
    {
        public string ActivityText { get; set; }
        public Guid ObjectId { get; set; }
        public User User { get; set; }
        public object ChangeReportJson { get; internal set; }
    }
}