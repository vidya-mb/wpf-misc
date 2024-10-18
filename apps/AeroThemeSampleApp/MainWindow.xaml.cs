using AeroThemeSampleApp.ViewModels;
using AeroThemeSampleApp.Views;
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

namespace AeroThemeSampleApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IServiceProvider _serviceProvider;

    public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = this;

        _serviceProvider = serviceProvider;
        var page = _serviceProvider.GetRequiredService(typeof(UserDashboardPage)); 
        this.RootContentFrame.Navigate(page);
    }

    public MainWindowViewModel ViewModel { get; }

    private void SearchBox_KeyUp(object sender, KeyEventArgs e)
    {
        //ViewModel.UpdateSearchText(SearchBox.Text);
    }

    private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
    {
        SearchBox.Text = "";
        //ViewModel.UpdateSearchText(SearchBox.Text);
    }

    private void ControlsList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //if (ControlsList.SelectedItem is NavigationItem navItem)
        //{
        //    _navigationService.NavigateTo(navItem.PageType);
        //}
    }
}