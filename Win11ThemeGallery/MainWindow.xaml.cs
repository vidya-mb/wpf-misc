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
using Win11ThemeGallery.Views;

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
        _navigationService.NavigationOccured += _navigationService_NavigationOccured;
        _navigationService.SetFrame(this.RootContentFrame);
        _navigationService.NavigateTo(typeof(DashboardPage));
    }

    private bool _selectionChangedFromSource = false;

    private void _navigationService_NavigationOccured(object? sender, NavigationOccuredEventArgs e)
    {
        NavigationItem navItem = ViewModel.GetNavigationItemFromPageType(e.PageType);
        if (navItem != null)
        {
            _selectionChangedFromSource = true;
            var _item = ControlsList.ItemContainerGenerator.ContainerFromItem(navItem);


            Queue<TreeViewItem> _queue = new Queue<TreeViewItem>();
            ItemsControl itemsControl = ControlsList;

            foreach(object item in itemsControl.Items)
            {
                _queue.Enqueue(itemsControl.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem);
            }

            while(_queue.Count > 0)
            {
                TreeViewItem item = _queue.Dequeue();
                if(item != null)
                {
                    if (item.DataContext == navItem)
                    {
                        item.IsSelected = true;
                        item.IsExpanded = true;
                        item.UpdateLayout();
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < item.Items.Count; i++)
                        {
                            _queue.Enqueue(item.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem);
                        }
                    }
                }
            }

        }
    }

    private IServiceProvider _serviceProvider;
    private INavigationService _navigationService;

    public MainWindowViewModel ViewModel { get; }

    private void ControlsList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (_selectionChangedFromSource)
        {
            _selectionChangedFromSource = false;
            return;
        }

        if (ControlsList.SelectedItem is NavigationItem navItem)
        {
            _navigationService.NavigateTo(navItem.PageType);
        }
    }

    private void SearchBox_KeyUp(object sender, KeyEventArgs e)
    {
        ViewModel.UpdateSearchText(SearchBox.Text);
    }

    private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
    {
        SearchBox.Text = "";
        ViewModel.UpdateSearchText(SearchBox.Text);
    }
}