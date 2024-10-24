using ImageRedef.Fluent.Helpers;
using ImageRedef.Fluent.Views;
using System.Windows.Shell;

namespace ImageRedef.Fluent;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();

        WindowChrome.SetWindowChrome(
            this,
            new WindowChrome
            {
                CaptionHeight = 50,
                CornerRadius = default,
                GlassFrameThickness = new Thickness(-1),
                ResizeBorderThickness = ResizeMode == ResizeMode.NoResize ? default : new Thickness(4),
                UseAeroCaptionButtons = true
            }
        );
    }

    public MainWindowViewModel ViewModel { get; }

    private readonly IServiceProvider _serviceProvider;

    private void NavigationList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if(NavigationList.SelectedItem is NavigationItem navItem)
        {
            var galleryViewModel = _serviceProvider.GetRequiredService<ImageGalleryViewModel>();
            galleryViewModel.ServiceProvider = _serviceProvider;
            galleryViewModel.NavigationItem = navItem;
            Page page = new ImageGalleryPage(galleryViewModel, _serviceProvider);
            RootFrame.Navigate(page);
        }
    }

    private void NavigationList_Loaded(object sender, RoutedEventArgs e)
    {
        if(_navigationListFirstLoad)
        {
            if (NavigationList.Items.Count > 0)
            {
                TreeViewItem? firstItem = NavigationList.ItemContainerGenerator.ContainerFromItem(NavigationList.Items[0]) as TreeViewItem;
                if (firstItem != null)
                {
                    firstItem.IsSelected = true;
                }
            }
        }
    }

    private bool _navigationListFirstLoad = true;
}