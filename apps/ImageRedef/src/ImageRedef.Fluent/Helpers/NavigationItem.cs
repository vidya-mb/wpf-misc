
namespace ImageRedef.Fluent.Helpers;

public class NavigationItem
{
    public string? IconGlyph { get; set; }
    public string Name { get; set; } = "";
    public string? FilePath { get; set; } = "";
    public NavigationItemType ItemType { get; set; }

    public ObservableCollection<NavigationItem> Items { get; set; }
        = new ObservableCollection<NavigationItem>();

    public bool HasItems => Items.Count > 0;
}

public enum NavigationItemType
{
    AllPhotos,
    Favourite,
    Folder
}