using Data.Entities;
using Data.Repositories;
using Projeto.Components;

namespace Projeto;

public partial class ModelsPage : ContentPage
{

	private readonly ModelsRepository modelsRepository; 

	public ModelsPage(ModelsRepository mr)
	{
		InitializeComponent();
        modelsRepository = mr; 
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync(); 
    }

    private async Task LoadAsync()
    {
        List<Models> items = await modelsRepository.GetAllAsync();

        ModelsList.ItemsSource = items;
        LblCount.Text = items.Count == 1
            ? "1 cadastrado"
            : $"{items.Count} cadastrados";
    }

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is not Card card || card.BindingContext is not Models item)
            return;

        await Shell.Current.GoToAsync($"{nameof(ModelsInsertPage)}?id={item.Id}");
    }

    private async void OnDeleteButtonClicked(object? sender, EventArgs args)
    {
        if (sender is not Card card || card.BindingContext is not Models item)
            return;

        bool confirm = await DisplayAlertAsync(
            "Excluir",
            $"Deseja excluir: {item.Name}",
            "Excluir",
            "Cancelar"
        );

        if (!confirm) return;

        await modelsRepository.DeleteAsync(item.Id);
        await LoadAsync();
    }

    private async void OnAddModelClicked(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync($"{nameof(ModelsInsertPage)}?id=0");
	}
}