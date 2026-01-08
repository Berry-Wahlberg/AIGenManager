using System;
using System.IO;

namespace BerryAIGen.Common;

public static class AppInfo
{
    private const string AppName = "AIGenManager";
    public static string AppDir { get; }
    public static SemanticVersion Version => SemanticVersionHelper.GetLocalVersion();
    public static string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AIGenManager");

    public static string DatabasePath { get; set; }

    public static string SettingsPath { get; set; }

    public static bool IsPortable { get; set; }

    static AppInfo()
    {
        AppDir = AppDomain.CurrentDomain.BaseDirectory;

        if (AppDir.EndsWith("\\"))
        {
            AppDir = AppDir.Substring(0, AppDir.Length - 1);
        }

        // Configuration file is saved in the application root directory by default
        SettingsPath = Path.Combine(AppInfo.AppDir, "config.json");
        IsPortable = true;
        
        // Default database path (users will be prompted to select during first run)
        DatabasePath = Path.Combine(AppInfo.AppDir, "aigen-manager.db");

    }
    
    // Initialize configuration and database paths
    public static void InitializePaths(string customDatabasePath = null)
    {
        // Use the custom database path if specified by the user
        if (!string.IsNullOrEmpty(customDatabasePath))
        {
            DatabasePath = customDatabasePath;
        }
    }


}

