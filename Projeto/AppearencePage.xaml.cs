namespace Projeto;

public partial class AppearencePage : ContentPage
{
	private bool isLoading = false; 

	public AppearencePage()
	{
		InitializeComponent();
		RestoreTheme(); 
    }

	private void RestoreTheme()
	{
		isLoading = true; 

		switch (Preferences.Get("AppTheme", nameof(AppTheme.Unspecified)))
		{
			case nameof(AppTheme.Light): RbLight.IsChecked = true; break;
			case nameof(AppTheme.Dark): RbDark.IsChecked = true; break;
			default: RbSystem.IsChecked = true; break; 
		}

		isLoading = false; 
	}

	private void OnThemeChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value || isLoading) return; 

		AppTheme theme = AppTheme.Unspecified;

		if (sender == RbDark)
			theme = AppTheme.Dark;
		else if (sender == RbLight)
			theme = AppTheme.Light;

		Application.Current!.UserAppTheme = theme;
		Preferences.Set("AppTheme", theme.ToString());       
	}
}