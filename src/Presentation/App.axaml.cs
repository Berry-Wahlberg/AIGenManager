using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using AIGenManager.Presentation.ViewModels;
using AIGenManager.Presentation.Views;
using AIGenManager.Core.Application.Ports;
using AIGenManager.Infrastructure.Repositories;
using AIGenManager.Infrastructure.Data;
using AIGenManager.Application.UseCases.Images;
using AIGenManager.Application.UseCases.Folders;

namespace AIGenManager.Presentation;

public partial class App : Avalonia.Application
{
    public IServiceProvider? ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Configure dependency injection
        ConfigureServices();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider!.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        // Database context
        var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AIGenManager");
        var dbPath = Path.Combine(appDataPath, "aigenmanager.db");
        services.AddSingleton(new DatabaseContext(dbPath));
        
        // SQLite connection
        services.AddSingleton(sp =>
        {
            var dbContext = sp.GetRequiredService<DatabaseContext>();
            return dbContext.GetConnection();
        });

        // Repositories
        services.AddSingleton<IImageRepository, SQLiteImageRepository>();
        services.AddSingleton<IFolderRepository, SQLiteFolderRepository>();

        // Use cases
        services.AddTransient<GetAllImagesUseCase>();
        services.AddTransient<GetImagesByFolderIdUseCase>();
        services.AddTransient<GetAllFoldersUseCase>();
        services.AddTransient<GetRootFoldersUseCase>();

        // ViewModels
        services.AddTransient<MainWindowViewModel>();

        ServiceProvider = services.BuildServiceProvider();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}