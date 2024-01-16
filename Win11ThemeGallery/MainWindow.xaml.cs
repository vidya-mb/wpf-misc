using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Appearance;
using Win11ThemeGallery.Navigation;
using Win11ThemeGallery.ViewModels;

namespace Win11ThemeGallery;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider, INavigationService navigationService)
    {
        SystemThemeWatcher.Watch(this);
        _serviceProvider = serviceProvider;
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();

        _navigationService = navigationService;
        _navigationService.SetFrame(this.RootContentFrame);
    }

    private IServiceProvider _serviceProvider;
    private INavigationService _navigationService;

    public MainWindowViewModel ViewModel { get; }

    private void ControlsList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (ControlsList.SelectedItem is NavigationItem navItem)
        {
            _navigationService.NavigateTo(navItem.PageType);
        }
    }
}