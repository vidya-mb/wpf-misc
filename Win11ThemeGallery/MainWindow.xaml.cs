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
public partial class MainWindow : Window
{
    public MainWindow()
    {
        SystemThemeWatcher.Watch(this);
        ViewModel = new MainWindowViewModel();
        DataContext = this;
        InitializeComponent();
    }

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
                    // RootContentDialog.Content = new ButtonPage().Content;
                    RootContentFrame.Source = new Uri(".\\Views\\ButtonPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default:
                    break;
            }
        }
    }
}