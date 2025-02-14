using System;

namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Main platform exception type
    /// </summary>
    public class PlatformException : Exception
    {
        public PlatformException(string message)
            : base(message)
        {
        }

        public PlatformException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
