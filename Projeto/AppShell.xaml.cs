namespace Projeto
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AppearencePage), typeof(AppearencePage));
            Routing.RegisterRoute(nameof(VehiclesInsertPage), typeof(VehiclesInsertPage));
            Routing.RegisterRoute(nameof(ModelsInsertPage), typeof(ModelsInsertPage));
            Routing.RegisterRoute(nameof(ManufacturersInsertPage), typeof(ManufacturersInsertPage));
        }

        private async void OnAppearencePageClicked(object sender, EventArgs args)
        {
            FlyoutIsPresented = false;
            await GoToAsync(nameof(AppearencePage)); 
        }
    }
}
