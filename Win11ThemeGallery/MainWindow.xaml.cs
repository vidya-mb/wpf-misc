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

namespace Win11ThemeGallery;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider)
    {
        SystemThemeWatcher.Watch(this);
        _serviceProvider = serviceProvider;
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    private IServiceProvider _serviceProvider;

    public MainWindowViewModel ViewModel { get; }

    private void ControlsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ControlsList.SelectedItem is string selectedItem)
        {
            string? controlName = selectedItem;
            SetPage(controlName);
        }
    }

    private void SetPage(string? controlName)
    {
        if (!string.IsNullOrEmpty(controlName))
        {
            switch(controlName)
            {
                case "Button":
                    //RootContentFrame.Source = new Uri(".\\Views\\ButtonPage.xaml", UriKind.RelativeOrAbsolute);
                    //RootContentFrame.DataContext = _serviceProvider.GetRequiredService<ButtonPageViewModel>();
                    RootContentFrame.Content = _serviceProvider.GetRequiredService<ButtonPage>();
                    break;
                case "Settings":
                    RootContentFrame.Content = _serviceProvider.GetRequiredService<SettingsPage>();
                    break;
                default:
                    break;
            }
        }
    }
}