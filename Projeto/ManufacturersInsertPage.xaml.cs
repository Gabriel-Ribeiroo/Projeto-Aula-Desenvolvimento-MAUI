namespace Projeto;

using Data.Entities;
using Data.Repositories;

[QueryProperty(nameof(ManufacturerId), "id")]
public partial class ManufacturersInsertPage : ContentPage
{

	private readonly ManufacturersRepository manufacturersRepository;
	private Manufacturers manufacturer = new(); 

	public int ManufacturerId { get; set; }

	public ManufacturersInsertPage(ManufacturersRepository mr)
	{
		InitializeComponent();
		manufacturersRepository = mr;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing(); 

		if (ManufacturerId == 0)
			return;

		Manufacturers? found = await manufacturersRepository.GetByIdAsync(ManufacturerId);

		if (found == null)
		{
			await DisplayAlertAsync("Erro", "Fabricante não encontrada", "Ok");
			await Shell.Current.GoToAsync("..");

			return;
		}

		manufacturer = found;

		EntName.Text = manufacturer.Name;
		EdtDescription.Text = manufacturer.Description; 
	}
	
	private async void OnSaveButtonClicked(object sender, EventArgs args)
	{
		string name = EntName.Text?.Trim() ?? string.Empty; 

		if (string.IsNullOrEmpty(name))
		{
			await DisplayAlertAsync("Tente novamnte", "Fabricante não encontrada", "Ok");
			EntName.Focus();

			return; 
		}

		manufacturer.Name = name;
		manufacturer.Description = EdtDescription.Text?.Trim();

		BtnSave.IsEnabled = false; 

		try
		{
			await manufacturersRepository.SaveAsync(manufacturer);
			await Navigation.PopAsync(animated: true);

        } catch(Exception)
		{
			await DisplayAlertAsync("Erro", "Não foi possível salvar, cheque as informações e tente novamente", "Ok");

		} finally
		{
			BtnSave.IsEnabled = true;
		}
	}
}