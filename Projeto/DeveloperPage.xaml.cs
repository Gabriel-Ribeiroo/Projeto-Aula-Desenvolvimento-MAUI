namespace Projeto;

public partial class DeveloperPage : ContentPage
{
	public DeveloperPage()
	{
		InitializeComponent();
	}

	private async void OnHomePageButtonClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync("MainPage");
	}
}