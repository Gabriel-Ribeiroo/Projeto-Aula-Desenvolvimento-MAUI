using Microsoft.Extensions.DependencyInjection;

namespace Projeto;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        DefineTheme(); 
    }

    private static void DefineTheme()
    {
        string savedTheme = Preferences.Get("AppTheme", nameof(AppTheme.Unspecified));
        Application.Current!.UserAppTheme = Enum.TryParse<AppTheme>(savedTheme, out var theme)
            ? theme
            : AppTheme.Unspecified;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}