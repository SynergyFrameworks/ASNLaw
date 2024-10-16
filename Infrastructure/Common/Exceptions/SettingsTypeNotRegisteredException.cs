namespace Infrastructure.Exceptions
{
    public class SettingsTypeNotRegisteredException : PlatformException
    {
        public SettingsTypeNotRegisteredException(string settingsType)
            : base($"Settings for type: {settingsType} not registered, please register it first")
        {
        }
    }
}
