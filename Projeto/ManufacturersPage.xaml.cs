namespace Projeto;

using Data.Entities;
using Data.Repositories;
using Projeto.Components;

public partial class ManufacturersPage : ContentPage
{

	private readonly ManufacturersRepository manufacturersRepository; 

	public ManufacturersPage(ManufacturersRepository mr)
	{
		InitializeComponent();
		manufacturersRepository = mr; 
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await LoadAsync();
    }

	private async Task LoadAsync()
	{
		List<Manufacturers> items = await manufacturersRepository.GetAllAsync();

		ManufacturersList.ItemsSource = items;
		LblCount.Text = items.Count == 1
			? "1 cadastrada"
			: $"{items.Count} cadastradas"; 
	}

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is not Card card || card.BindingContext is not Manufacturers item)
            return;

        await Shell.Current.GoToAsync($"{nameof(ManufacturersInsertPage)}?id={item.Id}");
    }

    private async void OnDeleteButtonClicked(object? sender, EventArgs args)
	{
		if (sender is not Card card || card.BindingContext is not Manufacturers item)
			return;

		bool confirm = await DisplayAlertAsync(
			"Excluir",
			$"Deseja excluir: {item.Name}",
			"Excluir",
			"Cancelar"
		);

		if (!confirm) return;

		await manufacturersRepository.DeleteAsync(item.Id);
		await LoadAsync();
	}

	private async void OnAddManufacturerClicked(object sender, EventArgs args)
	{
        await Shell.Current.GoToAsync($"{nameof(ManufacturersInsertPage)}?id=0");
    }
}