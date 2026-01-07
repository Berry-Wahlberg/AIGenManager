using System;
using BerryAIGen.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.Configuration;
using BerryAIGen.Toolkit.Models;
using BerryAIGen.Toolkit.Services;
using Microsoft.WindowsAPICodePack.Dialogs;
using SQLite;
using WPFLocalizeExtension.Engine;

namespace BerryAIGen.Toolkit
{
    public class WelcomeModel : BaseNotify
    {
        private List<FolderChange> _folderChanges = new List<FolderChange>();
        
        // Backing fields for all properties
        private int _step;
        private int _selectedIndex;
        private ObservableCollection<string> _imagePaths;
        private bool _storeWorkflow;
        private bool _storeMetadata;
        private bool _scanForNewImagesOnStartup;
        private string _databasePath;

        public WelcomeModel()
        {
            Step = 1;
            StoreWorkflow = true;
            StoreMetadata = true;
            // Set default database path to user's documents folder
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DatabasePath = Path.Combine(documentsPath, "BerryAIGen.Toolkit", "BerryAIGen-toolkit.db");
            
            // Initialize Escape command
            Escape = new RelayCommand<object>(_ => { });
        }

        public ICommand Escape
        {
            get;
            set => SetField(ref field, value);
        }

        public int Step
        {
            get => _step;
            set
            {
                SetField(ref _step, value);
                OnPropertyChanged("NotStart");
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetField(ref _selectedIndex, value);
        }

        public ObservableCollection<string> ImagePaths
        {
            get => _imagePaths;
            set => SetField(ref _imagePaths, value);
        }

        public bool StoreWorkflow
        {
            get => _storeWorkflow;
            set => SetField(ref _storeWorkflow, value);
        }

        public bool StoreMetadata
        {
            get => _storeMetadata;
            set => SetField(ref _storeMetadata, value);
        }

        public bool NotStart
        {
            get
            {
                switch (Step)
                {
                    case 1:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public bool ScanForNewImagesOnStartup
        {
            get => _scanForNewImagesOnStartup;
            set => SetField(ref _scanForNewImagesOnStartup, value);
        }

        public string DatabasePath
        {
            get => _databasePath;
            set => SetField(ref _databasePath, value);
        }
    }



    /// <summary>
    /// Interaction logic for Tips.xaml
    /// </summary>
    public partial class WelcomeWindow : BorderlessWindow
    {
        private readonly Settings _settings;
        private readonly WelcomeModel _model = new WelcomeModel();

        public IReadOnlyList<string> SelectedPaths { get; private set; }


        private class MinimalFolder
        {
            public string Path { get; set; }
        }


        List<MinimalFolder> FindRootFolders(SQLiteConnection db)
        {
            var allFolders = db.Query<MinimalFolder>("SELECT Path FROM Folder").ToList();

            var rootFolders = allFolders.Where(d => !allFolders.Any(p => d.Path.StartsWith(p.Path) && p.Path != d.Path)).ToList();

            return rootFolders;
        }

        public WelcomeWindow(Settings settings)
        {
            _settings = settings;

            InitializeComponent();

            // Validate localization keys
            ValidateLocalizationKeys();

            _model.ImagePaths = new ObservableCollection<string>();

            try
            {
                // For 1.9+ users starting with no config, but with an existing database
                // Try to load the current root folders
                if (ServiceLocator.DataStore != null)
                {
                    var db = ServiceLocator.DataStore.OpenConnection();

                    List<MinimalFolder> folders = new List<MinimalFolder>();


                    if (db.TableExist("Folder"))
                    {
                        try
                        {
                            var hasIsRoot = db.HasColumn("Folder", "IsRoot");
                            if (hasIsRoot)
                            {
                                folders = db.Query<MinimalFolder>("SELECT Path FROM Folder WHERE IsRoot = 1").ToList();
                            }
                            else
                            {
                                folders = FindRootFolders(db);
                            }
                        }
                        catch (Exception e)
                        {
                            folders = FindRootFolders(db);
                        }
                    }

                    if (folders.Any())
                    {
                        _model.ImagePaths = new ObservableCollection<string>(folders.Select(d => d.Path));
                    }
                }
            }
            catch (Exception e)
            {
                // Swallow exceptions in case the schema hasn't been updated yet
            }

            Closing += (sender, args) =>
            {
                settings.SetPristine();
                SelectedPaths = _model.ImagePaths.ToList();
                settings.StoreWorkflow = _model.StoreWorkflow;
                settings.StoreMetadata = _model.StoreMetadata;
                settings.ScanForNewImagesOnStartup = _model.ScanForNewImagesOnStartup;
                
                // Save user-selected database path
                AppInfo.InitializePaths(_model.DatabasePath);
            };

            DataContext = _model;
        }

        //private void HyperLink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
        //    {
        //        UseShellExecute = true,
        //    });
        //    e.Handled = true;
        //}


        private void AddFolder_OnClick(object sender, RoutedEventArgs e)
        {
            using var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                if (_model.ImagePaths.Any(d => dialog.FileName.StartsWith(d + "\\")))
                {
                    MessageBox.Show(this,
                        "The selected folder is already on the path of one of the included folders",
                        "Add folder", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                else if (_model.ImagePaths.Any(d => d.StartsWith(dialog.FileName)))
                {
                    MessageBox.Show(this,
                        "One of the included folders is on the path of the selected folder! It is recommended that you remove it.",
                        "Add folder", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                _model.ImagePaths.Add(dialog.FileName);
            }

        }

        private void RemoveFolder_OnClick(object sender, RoutedEventArgs e)
        {
            _model.ImagePaths.RemoveAt(_model.SelectedIndex);
        }

        private void SelectDatabasePath_OnClick(object sender, RoutedEventArgs e)
        {
            using var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select database location";
            
            // Show the dialog
            if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                // Set the database path
                _model.DatabasePath = Path.Combine(dialog.FileName, "diffusion-toolkit.db");
            }
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            if (_model.Step > 1)
            {
                _model.Step -= 1;
            }
        }

        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            if (_model.Step < 5)
            {
                _model.Step += 1;
            }
            else
            {
                Close();
            }
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }



        private void ValidateLocalizationKeys()
        {
            // Skip validation if LocalizeDictionary.Instance is not initialized
            if (LocalizeDictionary.Instance == null)
            {
                Console.WriteLine("[Localization Warning] LocalizeDictionary.Instance is not initialized, skipping validation.");
                return;
            }

            var requiredKeys = new[]
            {
                "Welcome.Title",
                "Welcome.Subtitle",
                "Welcome.Database.Title",
                "Welcome.Database.Subtitle",
                "Welcome.Database.InterfaceLanguage",
                "Common.Buttons.Browse",
                "Welcome.Folders.Title",
                "Welcome.Folders.Subtitle",
                "Welcome.Folders.AddFolder",
                "Welcome.Folders.RemoveFolder",
                "Settings.Images.ScanForNewImagesOnStartup",
                "Welcome.Metadata.Title",
                "Welcome.Metadata.Subtitle",
                "Welcome.Metadata.ComfyUI",
                "Settings.Metadata.StoreWorkflow",
                "Welcome.Metadata.RawMetadata",
                "Settings.Metadata.StoreMetadata",
                "Settings.Metadata.Warning",
                "Welcome.Complete.Title",
                "Welcome.Complete.Subtitle",
                "Common.Buttons.Back",
                "Common.Buttons.Next",
                "Common.Buttons.Finish",
                "Welcome.Common.ChangeSettingsLater"
            };

            var missingKeys = new List<string>();
            foreach (var key in requiredKeys)
            {
                try
                {
                    var value = LocalizeDictionary.Instance.GetLocalizedObject(key, null, LocalizeDictionary.Instance.Culture);
                    if (value == null || string.IsNullOrEmpty(value.ToString()) || value.ToString()!.StartsWith("[[") || value.ToString()!.StartsWith("Missing"))
                    {
                        missingKeys.Add(key);
                    }
                }
                catch
                {
                    missingKeys.Add(key);
                }
            }

            if (missingKeys.Any())
            {
                Console.WriteLine("[Localization Warning] Missing localization keys:");
                foreach (var key in missingKeys)
                {
                    Console.WriteLine($"  - {key}");
                }
            }
            else
            {
                Console.WriteLine("[Localization] All required localization keys are present.");
            }
        }

    }
}







