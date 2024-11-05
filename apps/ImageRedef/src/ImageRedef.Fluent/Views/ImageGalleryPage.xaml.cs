
using ImageRedef.Fluent.Models;
using ImageRedef.Fluent.ViewModels;

namespace ImageRedef.Fluent.Views;

public partial class ImageGalleryPage : Page
{
    public ImageGalleryPage(ImageGalleryViewModel galleryViewModel, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        ViewModel = galleryViewModel;
        DataContext = this;
        InitializeComponent();

        if(ViewModel.PhotosCount == 0)
        {
            PhotosListView.Visibility = Visibility.Hidden;
        }
    }

    private IServiceProvider _serviceProvider;

    public ImageGalleryViewModel ViewModel { get; }

    private void PhotosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPhotos = new ObservableCollection<Photo>();
        foreach (Photo photo in PhotosListView.SelectedItems)
        {
            selectedPhotos.Add(photo);
        }
        ViewModel.SelectedPhotos = selectedPhotos;
        UpdateMenuItems();
    }

    private void UpdateMenuItems()
    {
        switch(ViewModel.SelectedPhotosCount)
        {
            case 0:
                ClearSelectionMenuItem.Visibility = Visibility.Collapsed;
                DeleteMenuItem.Visibility = Visibility.Collapsed;
                MoveCopyMenuItem.Visibility = Visibility.Collapsed;
                EditPhotoMenuItem.Visibility = Visibility.Collapsed;
                FilterMenuItem.Visibility = Visibility.Visible;
                ViewMenuItem.Visibility = Visibility.Visible;
                break;
            case 1:
                ClearSelectionMenuItem.Visibility = Visibility.Visible;
                DeleteMenuItem.Visibility = Visibility.Visible;
                MoveCopyMenuItem.Visibility = Visibility.Visible;
                EditPhotoMenuItem.Visibility = Visibility.Visible;
                FilterMenuItem.Visibility = Visibility.Collapsed;
                ViewMenuItem.Visibility = Visibility.Collapsed;
                break;
            default:
                ClearSelectionMenuItem.Visibility = Visibility.Visible;
                DeleteMenuItem.Visibility = Visibility.Visible;
                MoveCopyMenuItem.Visibility = Visibility.Visible;
                EditPhotoMenuItem.Visibility = Visibility.Collapsed;
                FilterMenuItem.Visibility = Visibility.Collapsed;
                ViewMenuItem.Visibility = Visibility.Collapsed; 
                break;
        }
    }

    private void EditPhotoMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if(ViewModel.SelectedPhotosCount == 0)
        {
            throw new InvalidOperationException("No selected photo for editing");
        }

        var photoWindowViewModel = _serviceProvider.GetRequiredService<PhotoEditorViewModel>();
        photoWindowViewModel.Photo = ViewModel.SelectedPhotos.First();
        Window window = new PhotoEditorWindow(photoWindowViewModel, _serviceProvider);
        window.Show();
    }

    private void ClearSelectionMenuItem_Click(object sender, RoutedEventArgs e)
    {
        PhotosListView.UnselectAll();
        ViewModel.SelectedPhotos.Clear();
        ViewModel.SelectedPhotosCount = 0;
        UpdateMenuItems();
    }
}

