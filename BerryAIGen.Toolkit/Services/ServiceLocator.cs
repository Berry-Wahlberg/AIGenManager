using BerryAIGen.Toolkit.Common;
using BerryAIGen.Common;
using BerryAIGen.Database;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.Configuration;
using BerryAIGen.Toolkit.Models;
using BerryAIGen.Toolkit.Thumbnails;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace BerryAIGen.Toolkit.Services;

public class ServiceLocator
{
    private static NavigatorService? _navigatorService;
    private static MessageService? _messageService;
    private static DataStore? _dataStore;

    private static NavigationService? _navigationService;

    //private static ScanService? _scanManager;
    private static Settings? _settings;
    private static SearchService? _searchService;
    private static ThumbnailCache? _thumbnailCache;
    private static ThumbnailService? _thumbnailLoader;
    
    // Lazy initialization backing fields
    private static PreviewService? _previewService;
    private static ThumbnailNavigationService? _thumbnailNavigationService;
    private static TaggingService? _taggingService;
    private static NotificationService? _notificationService;
    private static ScanningService? _scanningService;
    private static FolderService? _folderService;
    private static ProgressService? _progressService;
    private static MetadataScannerService? _metadataScannerService;
    private static DatabaseWriterService? _databaseWriterService;
    private static ThumbnailService? _thumbnailService;
    private static ContextMenuService? _contextMenuService;
    private static ExternalApplicationsService? _externalApplicationsService;
    private static FileService? _fileService;
    private static AlbumService? _albumService;
    private static TagService? _tagService;
    private static WindowService? _windowService;
    private static ToastService? _toastService;

    public static DataStore? DataStore => _dataStore;
    public static Settings? Settings => _settings;
    public static ToastService? ToastService { get; set; }
    public static Dispatcher Dispatcher { get; set; }

    public static void SetDataStore(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public static void SetSettings(Settings? settings)
    {
        _settings = settings;
    }

    public static void SetNavigatorService(NavigatorService navigatorService)
    {
        _navigatorService = navigatorService;
    }

    public static PreviewService PreviewService
    {
        get { return _previewService ??= new PreviewService(); }
    }

    public static ThumbnailNavigationService ThumbnailNavigationService
    {
        get { return _thumbnailNavigationService ??= new ThumbnailNavigationService(); }
    }

    public static SearchService? SearchService { get; set; }

    public static TaggingService TaggingService
    {
        get { return _taggingService ??= new TaggingService(); }
    }

    public static NotificationService NotificationService
    {
        get { return _notificationService ??= new NotificationService(); }
    }

    public static ScanningService ScanningService
    {
        get { return _scanningService ??= new ScanningService(); }
    }

    public static MainModel? MainModel { get; set; }

    public static FolderService FolderService
    {
        get { return _folderService ??= new FolderService(); }
    }

    public static ProgressService ProgressService
    {
        get { return _progressService ??= new ProgressService(); }
    }

    public static MessageService? MessageService { get; set; }

    public static MetadataScannerService MetadataScannerService
    {
        get { return _metadataScannerService ??= new MetadataScannerService(); }
    }

    public static DatabaseWriterService DatabaseWriterService
    {
        get { return _databaseWriterService ??= new DatabaseWriterService(); }
    }

    public static ThumbnailService ThumbnailService
    {
        get { return _thumbnailService ??= new ThumbnailService(); }
    }

    public static ContextMenuService ContextMenuService
    {
        get { return _contextMenuService ??= new ContextMenuService(); }
    }

    public static ExternalApplicationsService ExternalApplicationsService
    {
        get { return _externalApplicationsService ??= new ExternalApplicationsService(); }
    }

    public static NavigatorService? NavigatorService => _navigatorService;

    public static FileService FileService
    {
        get { return _fileService ??= new FileService(); }
    }

    public static AlbumService AlbumService
    {
        get { return _albumService ??= new AlbumService(); }
    }

    public static TagService TagService
    {
        get { return _tagService ??= new TagService(); }
    }

    public static WindowService WindowService
    {
        get { return _windowService ??= new WindowService(); }
    }
}







