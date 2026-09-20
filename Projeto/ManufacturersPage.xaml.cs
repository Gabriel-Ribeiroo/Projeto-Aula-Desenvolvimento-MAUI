namespace Projeto;

public partial class ManufacturersPage : ContentPage
{
	public ManufacturersPage()
	{
		InitializeComponent();
	}

	private async void OnAddManufacturerClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync(nameof(ManufacturersInsertPage));
	}
}