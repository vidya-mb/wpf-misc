using ImageRedef.Fluent.Models;
using ImageRedef.Fluent.Helpers;
using Microsoft.Win32;

using ImageRedef.Fluent.ViewModels;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        foreach (Photo photo in SelectedPhotos)
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

        if (dialog.ShowDialog() == true)
        {
            foreach (Photo photo in SelectedPhotos)
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

        if (dialog.ShowDialog() == true)
        {
            foreach (Photo photo in SelectedPhotos)
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

        switch (navItem.ItemType)
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

public partial class ProImageGalleryViewModel : ObservableObject
{
    public IServiceProvider ServiceProvider { get; set; }
    public ICollectionView PhotosView { get; set; }
    [ObservableProperty]
    private NavigationItem _navigationItem;
    [ObservableProperty]
    private ObservableCollection<ImageItem> _images;

    [ObservableProperty]
    private ObservableCollection<ImageItem> _selectedPhotos;
    [ObservableProperty]
    private string _headerText;
    [ObservableProperty]
    private string _infoText;

    [ObservableProperty]
    private bool _isLoading;
    [ObservableProperty]
    private int _photosCount;
    [ObservableProperty]
    private int _selectedPhotosCount;

    public ProImageGalleryViewModel()
    {
        Images = new ObservableCollection<ImageItem>();
        SelectedPhotos = new ObservableCollection<ImageItem>();
    }

    public ProImageGalleryViewModel(NavigationItem navigationItem)
    {
        NavigationItem = navigationItem;
        InitializeData(navigationItem);
    }

    [RelayCommand]
    public void DeletePhotos()
    {
        // FAKE : Deletion Command
        //foreach (Photo photo in SelectedPhotos)
        //{
        //    Photos.Remove(photo);
        //}

        //SelectedPhotos.Clear();
        //SelectedPhotosCount = 0;
        //SetInfoText();
    }

    [RelayCommand]
    public void MovePhotos()
    {
        OpenFolderDialog dialog = new OpenFolderDialog()
        {
            Title = "Select folder to move the photos...",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        };

        if (dialog.ShowDialog() == true)
        {
            //foreach (Photo photo in SelectedPhotos)
            //{
            //    Photos.Remove(photo);
            //    //File.Move(photo.FilePath, Path.Combine(dialog.FolderName, photo.FileName));
            //}

            //SelectedPhotos.Clear();
            //SelectedPhotosCount = 0;
            //SetInfoText();
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

        if (dialog.ShowDialog() == true)
        {
            //foreach (Photo photo in SelectedPhotos)
            //{
            //    Photos.Remove(photo);
            //    //File.Copy(photo.FilePath, Path.Combine(dialog.FolderName, photo.FileName));
            //}

            //SelectedPhotos.Clear();
            //SelectedPhotosCount = 0;
            //SetInfoText();
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


    public void InitializeData(NavigationItem item)
    {
        item ??= NavigationItem;
        HeaderText = item.Name;
        SelectedPhotos = new ObservableCollection<ImageItem>();

        _ = LoadImagesAsync(item.FilePath).ContinueWith(SetInfoText);

    }

    private void SetInfoText(Task task)
    {
        if (task.IsCompletedSuccessfully)
        {
            InfoText = $"{Images.Count} photos";
        }
        else
        {
            InfoText = "No photos found!";
        }
    }

    private async Task LoadImagesAsync(string dirPath)
    {
        string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        string directoryPath = dirPath; // Change this to your photos directory

        try
        {
            var files = Directory.GetFiles(directoryPath)
                .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()));

            foreach (var file in files)
            {
                await LoadImageAsync(file);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading images: {ex.Message}");
        }
    }

    private async Task LoadImageAsync(string filePath)
    {
        //var imageItem = new ImageItem { IsLoading = true };
        //Application.Current.Dispatcher.Invoke(() => Images.Add(imageItem));

        await Task.Run(() =>
        {
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

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Images.Add(new ImageItem { ImageSource = bitmap });
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                    MessageBox.Show($"Error loading image {filePath}: {ex.Message}"));
            }
        });
    }
}

public class ImageItem : INotifyPropertyChanged
{
    private BitmapImage _imageSource;
    private bool _isLoading;

    public BitmapImage ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}