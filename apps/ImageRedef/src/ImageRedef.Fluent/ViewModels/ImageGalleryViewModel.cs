using ImageRedef.Fluent.Models;
using ImageRedef.Fluent.Helpers;
using ImageRedef.Fluent.ViewModels;

public partial class ImageGalleryViewModel : ObservableObject
{
    public ImageGalleryViewModel()
    {

    }

    public ImageGalleryViewModel(NavigationItem navigationItem)
    {
        NavigationItem = navigationItem;
        InitializeData(navigationItem);
    }

    public IServiceProvider ServiceProvider { get; set; }

    [ObservableProperty]
    private int _selectedPhotosCount;

    [ObservableProperty]
    private int _photosCount;

    [ObservableProperty]
    private NavigationItem _navigationItem;

    [ObservableProperty]
    private string _headerText;

    [ObservableProperty]
    private string _infoText;

    [ObservableProperty]
    private ObservableCollection<Photo> _photos;

    [ObservableProperty]
    private ObservableCollection<Photo> _selectedPhotos;

    [RelayCommand]
    public void RemoveSelection()
    {
        SelectedPhotos = new ObservableCollection<Photo>();
    }

    [RelayCommand]
    public void DeletePhotos()
    {

    }

    [RelayCommand]
    public void MovePhotos()
    {

    }

    [RelayCommand]
    public void CopyPhotos()
    {

    }

    [RelayCommand]
    public void EditPhoto()
    {

    }

    [RelayCommand]
    public void SortPhotos()
    {

    }

    [RelayCommand]
    public void FilterPhotos()
    {

    }

    partial void OnNavigationItemChanged(NavigationItem? oldValue, NavigationItem newValue)
    {
        ResetData();
        InitializeData(newValue);
    }

    partial void OnPhotosChanged(ObservableCollection<Photo> value)
    {
        PhotosCount = value?.Count ?? 0;
    }

    partial void OnSelectedPhotosChanged(ObservableCollection<Photo> value)
    {
        SelectedPhotosCount = value?.Count ?? 0;
    }

    private void InitializeData(NavigationItem item)
    {
        HeaderText = item.Name;
        Photos = LoadPhotos(item);
        SelectedPhotos = new ObservableCollection<Photo>();
        SetInfoText();
    }

    private void ResetData()
    {
        Photos = new ObservableCollection<Photo>();
        SelectedPhotos = new ObservableCollection<Photo>();
    }

    private ObservableCollection<Photo> LoadPhotos(NavigationItem navItem)
    {
        ArgumentNullException.ThrowIfNull(navItem);

        ObservableCollection<Photo> photos = new ObservableCollection<Photo>();

        switch(navItem.ItemType)
        {
            case NavigationItemType.AllPhotos:
                photos = PhotosDataSource.GetAllPhotos();
                break;
            case NavigationItemType.Favourite:
                photos = PhotosDataSource.GetFavouritePhotos();
                break;
            case NavigationItemType.Folder:
                photos = PhotosDataSource.GetPhotos(navItem.FilePath);
                break;
        }

        return photos;
    }

    private void SetInfoText()
    {

    }

}