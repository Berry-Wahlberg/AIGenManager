using ElectronNET.API;
using ElectronNET.API.Entities;
using BerryAIGen.Common;
using BerryAIGen.Database;
using BerryAIGen.IO;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Electron.NET
builder.WebHost.UseElectron(args);

var app = builder.Build();

// Initialize core services
InitializeCoreServices();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Configure Electron window with required dimensions and error handling
if (HybridSupport.IsElectronActive)
{
    Task.Run(async () =>
    {
        try
        {
            Logger.Log("Initializing Electron window...");
            
            var browserWindowOptions = new BrowserWindowOptions
            {
                Width = 1280,
                Height = 800,
                Show = false,
                Resizable = true,
                AutoHideMenuBar = true,
                Center = true,
                Title = "BerryAIGen Toolkit",
                BackgroundColor = "#ffffff",
                // Add icon if available
            };
            
            var mainWindow = await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
            
            // Clear cache for better performance
            await mainWindow.WebContents.Session.ClearCacheAsync();
            
            // Show window when ready
            mainWindow.OnReadyToShow += () => 
            {
                Logger.Log("Electron window ready, showing to user...");
                mainWindow.Show();
            };
            
            // Quit app when main window is closed
            mainWindow.OnClosed += () => 
            {
                Logger.Log("Main window closed, quitting application...");
                Electron.App.Quit();
            };
            
            Logger.Log("Electron window initialization completed successfully");
        }
        catch (Exception ex)
        {
            Logger.Log($"Error initializing Electron window: {ex.Message}");
            Logger.Log($"Stack trace: {ex.StackTrace}");
            
            // Show error dialog to user if possible
            try
            {
                Electron.Dialog.ShowErrorBox("Application Error", 
                    $"Failed to initialize Electron window: {ex.Message}\n\n" +
                    "Please contact support or restart the application.");
                Electron.App.Quit();
            }
            catch (Exception dialogEx)
            {
                Logger.Log($"Error showing error dialog: {dialogEx.Message}");
                // Fallback to console if dialog fails
                Console.WriteLine($"Fatal error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    });
}

// Run the application
app.Run();

void InitializeCoreServices()
{
    try
    {
        // Initialize logger
        Logger.Log("Initializing core services for Electron.NET application...");
        
        // Initialize data store
        var dataStore = new DataStore(AppInfo.DatabasePath);
        dataStore.Create(null, null).Wait();
        Logger.Log("Data store initialized successfully");
        
        // Initialize metadata scanner
        var metadataScanner = new MetadataScanner();
        Logger.Log("Metadata scanner initialized successfully");
        
        Logger.Log("Core services initialized successfully");
    }
    catch (Exception ex)
    {
        Logger.Log($"Error initializing core services: {ex.Message}");
        Logger.Log($"Stack trace: {ex.StackTrace}");
        
        // Show error dialog to user if possible
        if (HybridSupport.IsElectronActive)
        {
            Task.Run(() =>
            {
                Electron.Dialog.ShowErrorBox("Application Error", 
                    $"Failed to initialize core services: {ex.Message}\n\n" +
                    "Please contact support or restart the application.");
                Electron.App.Quit();
            });
        }
        else
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
