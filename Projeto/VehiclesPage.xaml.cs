namespace Projeto;

public partial class VehiclesPage : ContentPage
{
	public VehiclesPage()
	{
		InitializeComponent();
	}

	private async void OnAddVehicleButtonClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync(nameof(VehiclesInsertPage));
	}
}