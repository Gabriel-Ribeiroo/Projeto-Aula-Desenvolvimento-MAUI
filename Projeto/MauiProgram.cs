namespace Projeto;

using Data;
using Data.Repositories; 
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "vehicles.db3");
        builder.Services.AddSingleton(_ => new AppDatabase(dbPath));

        builder.Services.AddSingleton<ManufacturersRepository>();
        builder.Services.AddSingleton<ModelsRepository>();
        builder.Services.AddSingleton<VehiclesPage>();

        builder.Services.AddTransient<VehiclesInsertPage>();
        builder.Services.AddTransient<ModelsPage>(); 
        builder.Services.AddTransient<ModelsInsertPage>();
        builder.Services.AddTransient<ManufacturersPage>(); 
        builder.Services.AddTransient<ManufacturersInsertPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
