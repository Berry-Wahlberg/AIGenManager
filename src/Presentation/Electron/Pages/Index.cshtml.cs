using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using BerryAIGen.Common;
using BerryAIGen.Common.Query;
using BerryAIGen.Database;
using BerryAIGen.Database.Models;

namespace BerryAIGen.Electron.Pages;

public class IndexModel : PageModel
{
    public List<ImageView> SearchResults { get; set; } = new List<ImageView>();
    public int ResultsCount { get; set; } = 0;
    public double SearchTime { get; set; } = 0;
    public string CurrentQuery { get; set; } = string.Empty;
    public string ViewMode { get; set; } = "grid";
    public int ThumbnailSize { get; set; } = 128;
    public bool IsFiltersOpen { get; set; } = false;
    public bool IsPreviewOpen { get; set; } = false;
    public ImageView? SelectedItem { get; set; } = null;

    private readonly DataStore _dataStore;

    public IndexModel()
    {
        // Initialize data store
        _dataStore = new DataStore(AppInfo.DatabasePath);
    }

    public void OnGet(string query = "", string viewMode = "grid", int thumbnailSize = 128)
    {
        CurrentQuery = query;
        ViewMode = viewMode;
        ThumbnailSize = thumbnailSize;

        if (!string.IsNullOrEmpty(query))
        {
            SearchImages(query);
        }
    }

    public IActionResult OnPostSearch(string query)
    {
        CurrentQuery = query;
        SearchImages(query);
        return new PartialViewResult
        {
            ViewName = "_SearchResults",
            ViewData = ViewData,
            TempData = TempData
        };
    }

    public IActionResult OnPostSelectItem(int id)
    {
        SelectedItem = SearchResults.FirstOrDefault(item => item.Id == id);
        IsPreviewOpen = true;
        return new PartialViewResult
        {
            ViewName = "_PreviewPanel",
            ViewData = ViewData,
            TempData = TempData
        };
    }

    public IActionResult OnPostToggleFilters()
    {
        IsFiltersOpen = !IsFiltersOpen;
        return new JsonResult(new { isOpen = IsFiltersOpen });
    }

    public IActionResult OnPostTogglePreview()
    {
        IsPreviewOpen = !IsPreviewOpen;
        return new JsonResult(new { isOpen = IsPreviewOpen });
    }

    public IActionResult OnPostChangeViewMode(string viewMode)
    {
        ViewMode = viewMode;
        return new PartialViewResult
        {
            ViewName = "_ThumbnailGrid",
            ViewData = ViewData,
            TempData = TempData
        };
    }

    public IActionResult OnPostChangeThumbnailSize(int size)
    {
        ThumbnailSize = size;
        return new PartialViewResult
        {
            ViewName = "_ThumbnailGrid",
            ViewData = ViewData,
            TempData = TempData
        };
    }

    public IActionResult OnPostOpenFile(int id)
    {
        try
        {
            var image = SearchResults.FirstOrDefault(item => item.Id == id);
            if (image != null && !string.IsNullOrEmpty(image.Path))
            {
                // Open file using system default application
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = image.Path,
                    UseShellExecute = true
                });
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false, error = "File not found" });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error opening file: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostRenameFile(int id, string newName)
    {
        try
        {
            var image = SearchResults.FirstOrDefault(item => item.Id == id);
            if (image != null && !string.IsNullOrEmpty(image.Path))
            {
                var directory = System.IO.Path.GetDirectoryName(image.Path);
                if (directory != null)
                {
                    var newPath = System.IO.Path.Combine(directory, newName);
                    
                    // Rename the file using DataStore MoveImage method
                    _dataStore.MoveImage(id, newPath, null);
                    
                    // Update in search results
                    image.Path = newPath;
                    
                    return new JsonResult(new { success = true });
                }
            }
            return new JsonResult(new { success = false, error = "File not found" });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error renaming file: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostDeleteFile(int id)
    {
        try
        {
            var image = SearchResults.FirstOrDefault(item => item.Id == id);
            if (image != null)
            {
                // Delete image using DataStore RemoveImages method
                _dataStore.RemoveImages(new List<int> { id });
                
                // Remove from search results
                SearchResults.Remove(image);
                ResultsCount = SearchResults.Count;
                
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false, error = "File not found" });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error deleting file: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    // Image Organization - Tags
    public IActionResult OnPostAddTag(int imageId, int tagId)
    {
        try
        {
            // Add tag to image
            _dataStore.AddImageTag(imageId, tagId);
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error adding tag: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostRemoveTag(int imageId, int tagId)
    {
        try
        {
            // Remove tag from image
            _dataStore.RemoveImageTag(imageId, tagId);
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error removing tag: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    // Image Organization - Ratings
    public IActionResult OnPostSetRating(int imageId, int rating)
    {
        try
        {
            // Set image rating
            _dataStore.SetRating(imageId, rating);
            
            // Update in search results
            var image = SearchResults.FirstOrDefault(item => item.Id == imageId);
            if (image != null)
            {
                image.Rating = rating;
            }
            
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error setting rating: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    // Image Organization - Albums
    public IActionResult OnPostAddToAlbum(int imageId, int albumId)
    {
        try
        {
            // Add image to album
            _dataStore.AddImagesToAlbum(albumId, new List<int> { imageId });
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error adding to album: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostRemoveFromAlbum(int imageId, int albumId)
    {
        try
        {
            // Remove image from album
            _dataStore.RemoveImagesFromAlbum(albumId, new List<int> { imageId });
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error removing from album: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostCreateAlbum(string albumName)
    {
        try
        {
            // Create new album object
            var album = new BerryAIGen.Database.Models.Album
            {
                Name = albumName,
                Order = 0,
                LastUpdated = DateTime.Now
            };
            
            // Create new album
            var createdAlbum = _dataStore.CreateAlbum(album);
            return new JsonResult(new { success = true, albumId = createdAlbum.Id });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error creating album: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    // Batch Processing Methods
    public IActionResult OnPostBatchDelete(IEnumerable<int> imageIds)
    {
        try
        {
            // Delete multiple images
            _dataStore.RemoveImages(imageIds);
            
            // Remove from search results
            foreach (var imageId in imageIds)
            {
                var imageToRemove = SearchResults.FirstOrDefault(item => item.Id == imageId);
                if (imageToRemove != null)
                {
                    SearchResults.Remove(imageToRemove);
                }
            }
            ResultsCount = SearchResults.Count;
            
            return new JsonResult(new { success = true, deletedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch deleting images: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostBatchAddTag(IEnumerable<int> imageIds, int tagId)
    {
        try
        {
            // Add tag to multiple images
            foreach (var imageId in imageIds)
            {
                _dataStore.AddImageTag(imageId, tagId);
            }
            
            return new JsonResult(new { success = true, updatedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch adding tag: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostBatchRemoveTag(IEnumerable<int> imageIds, int tagId)
    {
        try
        {
            // Remove tag from multiple images
            foreach (var imageId in imageIds)
            {
                _dataStore.RemoveImageTag(imageId, tagId);
            }
            
            return new JsonResult(new { success = true, updatedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch removing tag: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostBatchSetRating(IEnumerable<int> imageIds, int rating)
    {
        try
        {
            // Set rating for multiple images
            foreach (var imageId in imageIds)
            {
                _dataStore.SetRating(imageId, rating);
                
                // Update in search results
                var image = SearchResults.FirstOrDefault(item => item.Id == imageId);
                if (image != null)
                {
                    image.Rating = rating;
                }
            }
            
            return new JsonResult(new { success = true, updatedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch setting rating: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostBatchAddToAlbum(IEnumerable<int> imageIds, int albumId)
    {
        try
        {
            // Add multiple images to album
            _dataStore.AddImagesToAlbum(albumId, imageIds);
            
            return new JsonResult(new { success = true, updatedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch adding to album: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    public IActionResult OnPostBatchRemoveFromAlbum(IEnumerable<int> imageIds, int albumId)
    {
        try
        {
            // Remove multiple images from album
            _dataStore.RemoveImagesFromAlbum(albumId, imageIds);
            
            return new JsonResult(new { success = true, updatedCount = imageIds.Count() });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error batch removing from album: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }

    // Update Checking Mechanism
    public async Task<IActionResult> OnPostCheckForUpdates()
    {
        try
        {
            var updateChecker = new UpdateChecker();
            var hasUpdate = await updateChecker.CheckForUpdate();
            
            if (hasUpdate && updateChecker.LatestRelease != null)
            {
                return new JsonResult(new {
                    success = true,
                    hasUpdate = true,
                    latestVersion = updateChecker.LatestRelease.tag_name,
                    releaseNotes = updateChecker.LatestRelease.body,
                    downloadUrl = updateChecker.LatestRelease.html_url,
                    publishedAt = updateChecker.LatestRelease.published_at
                });
            }
            
            return new JsonResult(new {
                success = true,
                hasUpdate = false
            });
        }
        catch (Exception ex)
        {
            Logger.Log($"Error checking for updates: {ex.Message}");
            return new JsonResult(new {
                success = false,
                error = ex.Message
            });
        }
    }

    private void SearchImages(string query)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        try
        {
            // Use the database search service with basic query options
            var queryOptions = new QueryOptions
            {
                Query = query,
                SearchView = SearchView.Search
            };
            
            var sorting = new Sorting("Date Created", "Z-A");
            var paging = new Paging { PageSize = 100, Offset = 0 };
            
            SearchResults = _dataStore.Search(queryOptions, sorting, paging).ToList();
            ResultsCount = SearchResults.Count;
        }
        catch (Exception ex)
        {
            Logger.Log($"Error searching images: {ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
            SearchTime = stopwatch.Elapsed.TotalSeconds;
        }
    }
}
