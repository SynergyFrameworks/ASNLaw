using System;

namespace Infrastructure.Query
{
    public class OperationResult
    {
        public string ResultMessage { get; set; }
        public bool CompletedSuccessfully { get; set; }
        public Guid IdResult { get; set; }
    }
}
