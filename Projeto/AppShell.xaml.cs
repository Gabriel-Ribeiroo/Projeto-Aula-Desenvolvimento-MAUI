namespace Projeto
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AppearencePage), typeof(AppearencePage)); 
        }

        private async void OnAppearencePageClicked(object sender, EventArgs args)
        {
            FlyoutIsPresented = false;
            await GoToAsync(nameof(AppearencePage)); 
        }
    }
}
