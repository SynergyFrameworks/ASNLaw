using MudBlazor;

public interface IThemeService
{
    bool IsDarkMode { get; }
    event EventHandler<bool> ThemeChanged;
    Task SetDarkMode(bool isDarkMode);
    MudTheme GetCurrentTheme();
}

public class ThemeService : IThemeService
{
    private readonly MudTheme _theme;

    public bool IsDarkMode { get; private set; }

    public event EventHandler<bool> ThemeChanged;

    public ThemeService()
    {
        _theme = new MudTheme();
    }

    public Task SetDarkMode(bool isDarkMode)
    {
        if (IsDarkMode != isDarkMode)
        {
            IsDarkMode = isDarkMode;
            ThemeChanged?.Invoke(this, isDarkMode);
        }
        return Task.CompletedTask;
    }

    public MudTheme GetCurrentTheme() => _theme;
}