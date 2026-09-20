using Data.Entities;
using Data.Repositories;
using Projeto.Components;

namespace Projeto;

public partial class VehiclesPage : ContentPage
{

	private readonly VehiclesRepository vehiclesRepository; 

	public VehiclesPage(VehiclesRepository vr)
	{
		InitializeComponent();
		vehiclesRepository = vr; 
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await LoadAsync(); 
    }

	private async Task LoadAsync()
	{
		List<Vehicles> items = await vehiclesRepository.GetAllAsync();

		VehiclesList.ItemsSource = items;
		LblCount.Text = items.Count == 1
			? "1 cadasctrado"
			: $"{items.Count} cadastrados"; 
    }

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is not Card card || card.BindingContext is not Vehicles item)
            return;

        await Shell.Current.GoToAsync($"{nameof(VehiclesInsertPage)}?id={item.Id}");
    }

    private async void OnDeleteButtonClicked(object? sender, EventArgs args)
    {
        if (sender is not Card card || card.BindingContext is not Vehicles item)
            return;

        bool confirm = await DisplayAlertAsync(
            "Excluir",
            $"Deseja excluir: {item.Name}",
            "Excluir",
            "Cancelar"
        );

        if (!confirm) return;

        await vehiclesRepository.DeleteAsync(item.Id);
        await LoadAsync();
    }

    private async void OnAddVehicleButtonClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync($"{nameof(VehiclesInsertPage)}?id=0");
	}
}