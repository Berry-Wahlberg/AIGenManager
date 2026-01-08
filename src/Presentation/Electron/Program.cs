using ElectronNET.API;
using ElectronNET.API.Entities;
using BerryAIGen.Common;
using BerryAIGen.Database;
using BerryAIGen.IO;

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

// Configure Electron window with required dimensions
if (HybridSupport.IsElectronActive)
{
    Task.Run(async () =>
    {
        var browserWindowOptions = new BrowserWindowOptions
        {
            Width = 1280,
            Height = 800,
            Show = false,
            Resizable = true,
            AutoHideMenuBar = true
        };
        
        var mainWindow = await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
        await mainWindow.WebContents.Session.ClearCacheAsync();
        mainWindow.OnReadyToShow += () => mainWindow.Show();
        mainWindow.OnClosed += () => Electron.App.Quit();
    });
}

app.Run();

void InitializeCoreServices()
{
    // Initialize logger
    Logger.Log("Initializing core services for Electron.NET application");
    
    // Initialize data store
    var dataStore = new DataStore(AppInfo.DatabasePath);
    dataStore.Create(null, null).Wait();
    
    // Initialize metadata scanner
    var metadataScanner = new MetadataScanner();
    
    Logger.Log("Core services initialized successfully");
}
