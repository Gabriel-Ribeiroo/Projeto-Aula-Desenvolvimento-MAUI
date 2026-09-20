using Data.Entities;
using Data.Repositories;

namespace Projeto;

[QueryProperty(nameof(ModelId), "id")]
public partial class ModelsInsertPage : ContentPage
{

	private readonly ModelsRepository modelsRepository;
	private Models model = new(); 

	public int ModelId { get; set; }

	public ModelsInsertPage(ModelsRepository mr)
	{
		InitializeComponent();
		modelsRepository = mr;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (ModelId == 0)
            return;

        Models? found = await modelsRepository.GetByIdAsync(ModelId);

        if (found == null)
        {
            await DisplayAlertAsync("Erro", "Fabricante não encontrada", "Ok");
            await Shell.Current.GoToAsync("..");

            return;
        }

        model = found;

        EntName.Text = model.Name;
        EdtDescription.Text = model.Description;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs args)
    {
        string name = EntName.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlertAsync("Tente novamente", "Campo nome é obrigatório", "Ok");
            EntName.Focus();

            return;
        }

        model.Name = name;
        model.Description = EdtDescription.Text?.Trim();

        BtnSave.IsEnabled = false;

        try
        {
            await modelsRepository.SaveAsync(model);
            await Navigation.PopAsync(animated: true);

        }
        catch (Exception)
        {
            await DisplayAlertAsync("Erro", "Não foi possível salvar, cheque as informações e tente novamente", "Ok");

        }
        finally
        {
            BtnSave.IsEnabled = true;
        }
    }
}