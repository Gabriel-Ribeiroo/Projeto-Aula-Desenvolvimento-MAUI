namespace Projeto;

public partial class ModelsPage : ContentPage
{
	public ModelsPage()
	{
		InitializeComponent();
	}

	private async void OnAddModelClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync(nameof(ModelsInsertPage));
	}
}