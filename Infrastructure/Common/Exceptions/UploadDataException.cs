using System;

namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Use this exception type to signal to the UploadController that you have already saved a failed upload entry.
    /// </summary>
    public class UploadDataException : Exception
    {
        public UploadDataException() : base() { }
        public UploadDataException(string message) : base(message) { }
        public UploadDataException(string message, Exception innerException) : base(message, innerException) { }
        public UploadDataException(Exception innerException) : base("Error uploading data.", innerException) { }
    }
}
