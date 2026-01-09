using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AIGenManager.Application.UseCases.Folders;
using AIGenManager.Application.UseCases.Images;
using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Presentation.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly GetRootFoldersUseCase _getRootFoldersUseCase;
    private readonly GetAllImagesUseCase _getAllImagesUseCase;
    private readonly GetImagesByFolderIdUseCase _getImagesByFolderIdUseCase;

    [ObservableProperty]
    private ObservableCollection<Folder> _folders = [];

    [ObservableProperty]
    private ObservableCollection<Image> _images = [];

    [ObservableProperty]
    private Folder? _selectedFolder;

    public MainWindowViewModel(
        GetRootFoldersUseCase getRootFoldersUseCase,
        GetAllImagesUseCase getAllImagesUseCase,
        GetImagesByFolderIdUseCase getImagesByFolderIdUseCase)
    {
        _getRootFoldersUseCase = getRootFoldersUseCase;
        _getAllImagesUseCase = getAllImagesUseCase;
        _getImagesByFolderIdUseCase = getImagesByFolderIdUseCase;
        
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            await LoadFolders();
            await LoadImages();
        }
        catch (Exception ex)
        {
            // Log the error (in a real app, you'd use a logging framework)
            Debug.WriteLine($"Error loading data: {ex.Message}");
        }
    }

    private async Task LoadFolders()
    {
        try
        {
            var folders = await _getRootFoldersUseCase.ExecuteAsync();
            Folders = new ObservableCollection<Folder>(folders);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading folders: {ex.Message}");
        }
    }

    private async Task LoadImages()
    {
        try
        {
            var images = await _getAllImagesUseCase.ExecuteAsync();
            Images = new ObservableCollection<Image>(images);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading images: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task LoadImagesForSelectedFolder()
    {
        try
        {
            if (SelectedFolder != null)
            {
                var images = await _getImagesByFolderIdUseCase.ExecuteAsync(
                    new GetImagesByFolderIdRequest(SelectedFolder.Id));
                Images = new ObservableCollection<Image>(images);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading images for selected folder: {ex.Message}");
        }
    }
}
