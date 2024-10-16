using Infrastructure.Common;

namespace Infrastructure.Jobs
{
    public class Job : Entity
    {
        public string State { get; set; }
        public bool Completed { get; set; }
    }
}
