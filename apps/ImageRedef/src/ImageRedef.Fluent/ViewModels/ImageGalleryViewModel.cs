using ImageRedef.Fluent.Models;
using ImageRedef.Fluent.Helpers;
using Microsoft.Win32;

using ImageRedef.Fluent.ViewModels;
using System.IO;
using System.ComponentModel;

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
    public ICollectionView PhotosView { get; set; }

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
    public void DeletePhotos()
    {
        // FAKE : Deletion Command
        foreach(Photo photo in SelectedPhotos)
        {
            Photos.Remove(photo);
        }

        SelectedPhotos.Clear();
        SelectedPhotosCount = 0;
        SetInfoText();
    }

    [RelayCommand]
    public void MovePhotos()
    {
        OpenFolderDialog dialog = new OpenFolderDialog()
        {
            Title = "Select folder to move the photos...",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        };

        if(dialog.ShowDialog() == true)
        {
            foreach(Photo photo in SelectedPhotos)
            {
                Photos.Remove(photo);
                //File.Move(photo.FilePath, Path.Combine(dialog.FolderName, photo.FileName));
            } 

            SelectedPhotos.Clear();
            SelectedPhotosCount = 0;
            SetInfoText();
        }
    }

    [RelayCommand]
    public void CopyPhotos()
    {
        OpenFolderDialog dialog = new OpenFolderDialog()
        {
            Title = "Select folder to move the photos...",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        };

        if(dialog.ShowDialog() == true)
        {
            foreach(Photo photo in SelectedPhotos)
            {
                Photos.Remove(photo);
                //File.Copy(photo.FilePath, Path.Combine(dialog.FolderName, photo.FileName));
            } 

            SelectedPhotos.Clear();
            SelectedPhotosCount = 0;
            SetInfoText();
        }

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
        SetInfoText();
    }

    partial void OnSelectedPhotosChanged(ObservableCollection<Photo> value)
    {
        SelectedPhotosCount = value?.Count ?? 0;
        SetInfoText();
    }

    private void InitializeData(NavigationItem item)
    {
        HeaderText = item.Name;
        Photos = LoadPhotos(item);
        PhotosView = CollectionViewSource.GetDefaultView(Photos);
        SelectedPhotos = new ObservableCollection<Photo>();
        SetInfoText();
    }

    private void ResetData()
    {
        Photos = new ObservableCollection<Photo>();
        SelectedPhotos = new ObservableCollection<Photo>();
        SetInfoText();
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
        InfoText = $"{PhotosCount} photos";
    }

}