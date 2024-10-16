namespace LawUI.Exceptions
{
    public class AzureConfigurationException : Exception
    {
        public AzureConfigurationException() : base("Azure Cognitive Services are not properly configured. Please check your configuration settings.")
        {
        }

        public AzureConfigurationException(string message) : base(message)
        {
        }

        public AzureConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
