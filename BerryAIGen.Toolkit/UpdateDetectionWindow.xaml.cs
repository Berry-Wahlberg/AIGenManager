using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BerryAIGen.Common;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.Localization;
using BerryAIGen.Toolkit.Models;
using BerryAIGen.Toolkit.Services;

namespace BerryAIGen.Toolkit
{
    /// <summary>
    /// Interaction logic for UpdateDetectionWindow.xaml
    /// </summary>
    public partial class UpdateDetectionWindow : Window
    {
        private UpdateChecker _updateChecker;
        private CancellationTokenSource _cts;
        private bool _isChecking;
        private MessageService _messageService; // Fixed: Changed from non-existent IMessagePopupManager to MessageService

        public UpdateDetectionWindow()
        {
            InitializeComponent();
            Loaded += UpdateDetectionWindow_Loaded;
        }

        private async void UpdateDetectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the MessageService when the window is loaded, not in the constructor
            _messageService = ServiceLocator.MessageService;
            await CheckForUpdatesAsync();
        }

        private async Task CheckForUpdatesAsync()
        {
            _isChecking = true;
            ProgressText.Text = GetLocalizedText("Update.CheckingForUpdates");
            TimeoutPanel.Visibility = Visibility.Collapsed;
            SkipButton.Visibility = Visibility.Visible;
            RetryButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;

            try
            {
                _cts = new CancellationTokenSource();
                _updateChecker = new UpdateChecker();

                // Create a timeout cancellation token source
                using var timeoutCts = new CancellationTokenSource(3000);
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, timeoutCts.Token);

                // Start update check with timeout
                var hasUpdate = await _updateChecker.CheckForUpdate(timeout: 3000, cancellationToken: linkedCts.Token);

                if (_cts.IsCancellationRequested)
                {
                    return; // Skip was pressed
                }

                if (hasUpdate)
                {
                    await HandleUpdateAvailableAsync();
                }
                else
                {
                    NavigateToMainInterface();
                }
            }
            catch (OperationCanceledException)
            {
                // Check if it was a timeout or user cancellation
                if (!_cts.IsCancellationRequested)
                {
                    // It was a timeout
                    ShowTimeoutMessage();
                }
            }
            catch (Exception ex)
            {
                await HandleUpdateErrorAsync(ex);
            }
            finally
            {
                _isChecking = false;
            }
        }

        private async Task HandleUpdateAvailableAsync()
        {
            var result = await _messageService.Show(
                GetLocalizedText("Main.Update.UpdateAvailable"),
                "AIGenManager",
                PopupButtons.YesNo);

            if (result == PopupResult.Yes)
            {
                CallUpdater();
            }
            
            NavigateToMainInterface();
        }

        private async Task HandleUpdateErrorAsync(Exception ex)
        {
            // Log the error
            Logger.Log($"Update detection failed: {ex.Message}");
            Logger.Log(ex);
            
            // Don't show error message to user unless it's critical
            // Instead, silently continue to main interface
            NavigateToMainInterface();
        }

        private void ShowTimeoutMessage()
        {
            ProgressText.Text = GetLocalizedText("Update.CheckTimeout");
            TimeoutPanel.Visibility = Visibility.Visible;
            SkipButton.Visibility = Visibility.Collapsed;
            RetryButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            // Log user action
            Logger.Log("User skipped update detection");
            
            // Cancel ongoing operation
            _cts?.Cancel();
            
            // Navigate to main interface
            NavigateToMainInterface();
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            _ = CheckForUpdatesAsync();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("User cancelled update retry");
            NavigateToMainInterface();
        }

        private void CallUpdater()
        {
            try
            {
                System.Diagnostics.Process.Start("explorer", "https://github.com/Berry-Wahlberg/AIGenManager/releases/latest");
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to open updater: {ex.Message}");
                Logger.Log(ex);
            }
        }

        private void NavigateToMainInterface()
        {
            // Close this window and show main application
            DialogResult = true;
            Close();
        }

        private string GetLocalizedText(string key)
        {
            try
            {
                return (string)JsonLocalizationProvider.Instance.GetLocalizedObject(key, null, CultureInfo.CurrentCulture);
            }
            catch
            {
                // Fallback to English if localization fails
                return key switch
                {
                    "Update.CheckingForUpdates" => "Checking for updates...",
                    "Update.CheckTimeout" => "Update check timed out",
                    "Update.SkipDetection" => "Skip Detection",
                    "Main.Update.UpdateAvailable" => "An update is available. Would you like to download it?",
                    _ => key
                };
            }
        }
    }
}
