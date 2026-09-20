namespace Projeto;

[QueryProperty(nameof(VehicleId), "id")]
public partial class VehiclesInsertPage : ContentPage
{
    private bool loaded;

    public VehiclesInsertPage()
    {
        InitializeComponent();
    }

    public string VehicleId { get; set; } = string.Empty;

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (loaded) return;
        loaded = true;

        LoadYears();
    }

    private void LoadYears()
    {
        var currentYear = DateTime.Now.Year;

        var years = Enumerable.Range(1900, currentYear - 1898)
            .Reverse()
            .ToList();

        ManufactureYearPicker.ItemsSource = years;
        ModelYearPicker.ItemsSource = new List<int>(years);
    }

    private void OnManufactureYearChanged(object sender, EventArgs e)
    {
        if (ModelYearPicker.SelectedIndex >= 0) return;
        if (ManufactureYearPicker.SelectedItem is not int year) return;

        var modelYears = (List<int>)ModelYearPicker.ItemsSource;
        var index = modelYears.IndexOf(year);
        if (index >= 0)
            ModelYearPicker.SelectedIndex = index;
    }

}