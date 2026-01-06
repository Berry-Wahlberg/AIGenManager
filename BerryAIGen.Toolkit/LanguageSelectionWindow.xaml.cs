using System;
using BerryAIGen.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.Configuration;
using BerryAIGen.Toolkit.Models;
using WPFLocalizeExtension.Engine;

namespace BerryAIGen.Toolkit
{
    public class LanguageSelectionModel : BaseNotify
    {
        private string _selectedLanguage;

        public LanguageSelectionModel()
        {
            // Set default language based on system language
            var systemLanguage = CultureInfo.CurrentUICulture.Name;
            if (systemLanguage.StartsWith("zh-CN"))
            {
                SelectedLanguage = "zh-CN";
            }
            else if (systemLanguage.StartsWith("zh-TW"))
            {
                SelectedLanguage = "zh-TW";
            }
            else
            {
                SelectedLanguage = "en-US";
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetField(ref _selectedLanguage, value);
        }

        public List<string> SupportedLanguages
        {
            get => new List<string> { "en-US", "zh-CN", "zh-TW", "de-DE", "fr-FR", "es-ES", "ja-JP" };
        }
    }

    public partial class LanguageSelectionWindow : BorderlessWindow
    {
        private readonly LanguageSelectionModel _model = new LanguageSelectionModel();
        private readonly Settings _settings;

        public LanguageSelectionWindow(Settings settings)
        {
            _settings = settings;
            InitializeComponent();
            DataContext = _model;
            
            // Load appropriate theme resources
            LoadThemeResources();
        }
        
        private void LoadThemeResources()
        {
            // Get the current theme from settings or use system theme
            string themeName = _settings.Theme ?? "System";
            
            // Determine the actual theme to use
            if (string.IsNullOrEmpty(themeName) || themeName == "System")
            {
                // Use Windows theme preference
                using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                var registryValueObject = key?.GetValue("AppsUseLightTheme");
                if (registryValueObject == null)
                {
                    themeName = "Light";
                }
                else
                {
                    var registryValue = (int)registryValueObject;
                    themeName = registryValue > 0 ? "Light" : "Dark";
                }
            }
            
            // Clear any existing resources
            Resources.MergedDictionaries.Clear();
            
            // Load theme resources
            LoadResourceDictionary($"Themes/{themeName}.xaml");
            LoadResourceDictionary("Themes/Common.xaml");
            LoadResourceDictionary("Themes/Menu.xaml");
            LoadResourceDictionary("Themes/SWStyles.xaml");
            LoadResourceDictionary("Themes/Scrollbars.xaml");
            LoadResourceDictionary("Themes/Window.xaml");
            
            
        }
        
        private void LoadResourceDictionary(string url)
        {
            try
            {
                var resource = (ResourceDictionary)Application.LoadComponent(new System.Uri(url, System.UriKind.Relative));
                Resources.MergedDictionaries.Add(resource);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading resource dictionary {url}: {ex.Message}");
            }
        }

        public string SelectedLanguage => _model.SelectedLanguage;

        private void Continue_OnClick(object sender, RoutedEventArgs e)
        {
            // Apply the selected language
            if (!string.IsNullOrEmpty(_model.SelectedLanguage))
            {
                LocalizeDictionary.Instance.Culture = new CultureInfo(_model.SelectedLanguage);
                _settings.Culture = _model.SelectedLanguage;
            }

            // Close the language selection window
            this.DialogResult = true;
            Close();
        }
    }
}






