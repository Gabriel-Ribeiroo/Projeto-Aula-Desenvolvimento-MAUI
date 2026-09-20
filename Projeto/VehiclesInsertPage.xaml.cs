//using Data.Entities;
//using Data.Repositories;
//using System.Runtime.CompilerServices;

//namespace Projeto;

//[QueryProperty(nameof(VehicleId), "id")]
//public partial class VehiclesInsertPage : ContentPage
//{

//    private readonly VehiclesRepository vehiclesRepository;
//    private readonly ModelsRepository modelsRepository;
//    private readonly ManufacturersRepository manufacturersRepository; 

//    private Vehicles vehicle = new(); 

//    public VehiclesInsertPage(VehiclesRepository vr, ModelsRepository mlr, ManufacturersRepository mr)
//    {
//        InitializeComponent();

//        vehiclesRepository = vr;
//        modelsRepository = mlr;
//        manufacturersRepository = mr; 
//    }

//    public int VehicleId { get; set; }

//    protected override async void OnAppearing()
//    {
//        base.OnAppearing();
//        LoadYears(); 

//        try
//        {
//            await LoadAsync();
//        }
//        catch (Exception ex)
//        {
//            await DisplayAlertAsync("Erro", ex.Message, "OK");
//        }
//    }

//    private async Task LoadAsync()
//    {
//        try
//        {
//            List<Manufacturers> manufacturers = await manufacturersRepository.GetAllAsync();
//            List<Models> models = await modelsRepository.GetAllAsync();

//            PckManufacturer.ItemsSource = manufacturers;
//            PckModel.ItemsSource = models;  

//            if (VehicleId == 0)
//                return;

//            Vehicles? found = await vehiclesRepository.GetByIdAsync(VehicleId);
//            if (found == null) return;

//            vehicle = found; 

//            EntName.Text = vehicle.Name;
//            PckManufacturerYear.SelectedItem = vehicle.ManufectureYear;
//            PckModelYear.SelectedIndex = vehicle.ModelYear;

//            PckManufacturer.SelectedItem = manufacturers.FirstOrDefault(m => m.Id == vehicle.ManufecturerId);
//            PckModel.SelectedItem = models.FirstOrDefault(m => m.Id == vehicle.ModelId);
//        } catch(Exception)
//        {
//            await DisplayAlertAsync("Error", "Cheque as informações e tente novamente.", "Ok");
//        }

//    }

//    private void LoadYears()
//    {
//        var currentYear = DateTime.Now.Year;

//        var years = Enumerable.Range(1900, currentYear - 1898)
//            .Reverse()
//            .ToList();

//        PckManufacturerYear.ItemsSource = years;
//        PckModelYear.ItemsSource = new List<int>(years);
//    }

//    private void OnManufactureYearChanged(object sender, EventArgs e)
//    {
//        if (PckModelYear.SelectedIndex >= 0) return;
//        if (PckManufacturerYear.SelectedItem is not int year) return;

//        var modelYears = (List<int>)PckModelYear.ItemsSource;
//        var index = modelYears.IndexOf(year);
//        if (index >= 0)
//            PckModelYear.SelectedIndex = index;
//    }

//    private async void OnSaveButtonClicked(object sender, EventArgs args)
//    {
//        string name = EntName.Text?.Trim() ?? string.Empty;

//        if (string.IsNullOrEmpty(name))
//        {
//            await DisplayAlertAsync("Tente novamente", "Fabricante não encontrada", "Ok");
//            EntName.Focus();

//            return;
//        }

//        if (PckManufacturer.SelectedItem is not Manufacturers manufacturer || PckModel.SelectedItem is not Models model)
//        {
//            await DisplayAlertAsync("Erro", "Preencha modelo e fabricante", "Ok");
//            return; 
//        }

//        if(PckManufacturerYear.SelectedItem is not int manufacturerYear || PckModelYear.SelectedItem is not int modelYear)
//        {
//            await DisplayAlertAsync("Erro", "Preencha ano do modelo e fabricação", "Ok");
//            return; 
//        }

//        vehicle.Name = name;
//        vehicle.Description = EdtDescription.Text?.Trim();
//        vehicle.ManufecturerId = manufacturer.Id;
//        vehicle.ModelId = model.Id; 
//        vehicle.ManufectureYear = manufacturerYear;
//        vehicle.ModelYear = modelYear; 

//        BtnSave.IsEnabled = false;

//        try
//        {
//            await vehiclesRepository.SaveAsync(vehicle);
//            await Navigation.PopAsync(animated: true);

//        }
//        catch (Exception)
//        {
//            await DisplayAlertAsync("Erro", "Não foi possível salvar, cheque as informações e tente novamente", "Ok");

//        }
//        finally
//        {
//            BtnSave.IsEnabled = true;
//        }
//    }

//}

using Data.Entities;
using Data.Repositories;

namespace Projeto;

[QueryProperty(nameof(VehicleId), "id")]
public partial class VehiclesInsertPage : ContentPage
{

    private readonly VehiclesRepository vehiclesRepository;
    private readonly ModelsRepository modelsRepository;
    private readonly ManufacturersRepository manufacturersRepository;

    private Vehicles vehicle = new();

    private List<int> manufactureYears = [];
    private List<int> modelYears = [];

    private bool loading;

    public VehiclesInsertPage(VehiclesRepository vr, ModelsRepository mlr, ManufacturersRepository mr)
    {
        InitializeComponent();

        vehiclesRepository = vr;
        modelsRepository = mlr;
        manufacturersRepository = mr;
    }

    public int VehicleId { get; set; }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadYears();

        try
        {
            await LoadAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private async Task LoadAsync()
    {
        loading = true;

        try
        {
            List<Manufacturers> manufacturers = await manufacturersRepository.GetAllAsync();
            List<Models> models = await modelsRepository.GetAllAsync();

            PckManufacturer.ItemsSource = manufacturers;
            PckModel.ItemsSource = models;

            if (VehicleId == 0)
                return;

            Vehicles? found = await vehiclesRepository.GetByIdAsync(VehicleId);
            if (found == null) return;

            vehicle = found;

            EntName.Text = vehicle.Name;
            EdtDescription.Text = vehicle.Description;

            PckManufacturerYear.SelectedIndex = manufactureYears.IndexOf(vehicle.ManufectureYear);
            PckModelYear.SelectedIndex = modelYears.IndexOf(vehicle.ModelYear);

            PckManufacturer.SelectedItem = manufacturers.FirstOrDefault(m => m.Id == vehicle.ManufecturerId);
            PckModel.SelectedItem = models.FirstOrDefault(m => m.Id == vehicle.ModelId);
        }
        finally
        {
            loading = false;
        }
    }

    private void LoadYears()
    {
        var currentYear = DateTime.Now.Year;

        manufactureYears = Enumerable.Range(1900, currentYear - 1900 + 1)
            .Reverse()
            .ToList();

        modelYears = Enumerable.Range(1900, currentYear - 1900 + 2)
            .Reverse()
            .ToList();

        PckManufacturerYear.ItemsSource = manufactureYears;
        PckModelYear.ItemsSource = modelYears;
    }

    private void OnManufactureYearChanged(object sender, EventArgs e)
    {
        if (loading) return;
        if (PckModelYear.SelectedIndex >= 0) return;
        if (PckManufacturerYear.SelectedItem is not int year) return;

        var index = modelYears.IndexOf(year);
        if (index >= 0)
            PckModelYear.SelectedIndex = index;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs args)
    {
        string name = EntName.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlertAsync("Tente novamente", "Informe o nome do veículo", "Ok");
            EntName.Focus();

            return;
        }

        if (PckManufacturer.SelectedItem is not Manufacturers manufacturer || PckModel.SelectedItem is not Models model)
        {
            await DisplayAlertAsync("Erro", "Preencha modelo e fabricante", "Ok");
            return;
        }

        if (PckManufacturerYear.SelectedItem is not int manufacturerYear || PckModelYear.SelectedItem is not int modelYear)
        {
            await DisplayAlertAsync("Erro", "Preencha ano do modelo e fabricação", "Ok");
            return;
        }

        vehicle.Name = name;
        vehicle.Description = EdtDescription.Text?.Trim();
        vehicle.ManufecturerId = manufacturer.Id;
        vehicle.ModelId = model.Id;
        vehicle.ManufectureYear = manufacturerYear;
        vehicle.ModelYear = modelYear;

        BtnSave.IsEnabled = false;

        try
        {
            await vehiclesRepository.SaveAsync(vehicle);
            await Shell.Current.GoToAsync("..");
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