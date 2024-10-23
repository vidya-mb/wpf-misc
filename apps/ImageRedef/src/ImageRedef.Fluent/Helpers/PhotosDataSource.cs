
using System.IO;
using ImageRedef.Fluent.Models;

namespace ImageRedef.Fluent.Helpers;

public static class PhotosDataSource
{
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
        foreach(string subDirectory in subDirectories)
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

        foreach(string file in files)
        {
            if(Photo.GetMediaType(file) != MediaType.Unknown)
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

    private static string UserPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
}