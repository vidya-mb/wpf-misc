
using System.IO;
using System.Text.Json;
using ImageMagick.Drawing;
using ImageRedef.Fluent.Models;

namespace ImageRedef.Fluent.Helpers;

public static class PhotosDataSource
{
    class JsonConfig
    {
        public ObservableCollection<NavigationItem> NavigationList { get; set; }
    }

    public static ObservableCollection<NavigationItem> NavigationListFromConfig()
    {
        var configFilePath = "config.json";
        if (!File.Exists(configFilePath)) return DefaultNavigationList();
        var jsonContent = File.ReadAllText(configFilePath);

        var navListFromConfig = JsonSerializer.Deserialize<JsonConfig>(jsonContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return navListFromConfig?.NavigationList ?? DefaultNavigationList();
    }
    public static ObservableCollection<NavigationItem> DefaultNavigationList()
    {
        ObservableCollection<NavigationItem> navItems;
        navItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem { IconGlyph = "\uE7C5", Name = "All Photos",
                                    ItemType = NavigationItemType.AllPhotos },
            new NavigationItem { IconGlyph = "\uE734", Name = "Favourites",
                                    ItemType = NavigationItemType.Favourite },
        };
        navItems.Add(CreateNavigationItemFromPath(UserPicturesPath));
        return navItems;
    }

    public static NavigationItem CreateNavigationItemFromPath(string? directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        NavigationItem item = new NavigationItem();
        item.Name = Path.GetFileName(directory);
        item.FilePath = directory;
        item.IconGlyph = "\uE838";
        item.ItemType = NavigationItemType.Folder;
        item.Items = new ObservableCollection<NavigationItem>();

        string[] subDirectories = Directory.GetDirectories(directory);
        foreach (string subDirectory in subDirectories)
        {
            item.Items.Add(CreateNavigationItemFromPath(subDirectory));
        }

        return item;
    }

    public static ObservableCollection<Photo> GetPhotos(string directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        ObservableCollection<Photo> photos = new ObservableCollection<Photo>();
        IEnumerable<string> files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
            if (Photo.GetMediaType(file) != MediaType.Unknown)
            {
                Photo photo = new Photo(file);
                photos.Add(photo);
            }
        }

        return photos;
    }

    public static ObservableCollection<Photo> GetAllPhotos()
    {
        return GetPhotos(UserPicturesPath);
    }

    public static ObservableCollection<Photo> GetFavouritePhotos()
    {
        return GetPhotos(UserPicturesPath);
    }


    public static IAsyncEnumerable<Photo> GetAllPhotosAsync()
    {
        return GetPhotosAsync(UserPicturesPath);
    }

    public static IAsyncEnumerable<Photo> GetFavouritePhotosAsync()
    {
        return GetPhotosAsync(UserPicturesPath);
    }


    public static async IAsyncEnumerable<Photo> GetPhotosAsync(string directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        var files = Directory.GetFiles(directory)
            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()));

        foreach (var file in files)
        {
            var photo = await Task.Run(() => LoadBitmapCachedImage(file));
            yield return photo;
        }
    }

    private static Photo LoadBitmapCachedImage(string filePath)
    {
        var photo = new Photo(filePath) { };

        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = new Uri(filePath);
            bitmap.DecodePixelWidth = 200; // Reduce memory usage by loading smaller versions
            bitmap.EndInit();
            bitmap.Freeze(); // Important for cross-thread usage

            photo.ImageSource = bitmap;
        }
        catch
        {
            //ignore the exception
        }

        return photo;

    }






    private static string UserPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
}