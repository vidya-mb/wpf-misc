using System.Windows.Shell;
using ImageRedef.Fluent.Controls;
using ImageRedef.Fluent.ViewModels;

namespace ImageRedef.Fluent.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class PhotoEditorWindow : Window
{
    public PhotoEditorWindow(PhotoEditorViewModel viewModel, IServiceProvider serviceProvider)
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

    public PhotoEditorViewModel ViewModel { get; }

    private readonly IServiceProvider _serviceProvider;

    private void EditingModeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var lvi = EditingModeList.SelectedItem as ListViewItem;
        string? tag = lvi?.Tag as string;
        
        if(string.IsNullOrEmpty(tag))
        {
            throw new Exception("Tag for the ListViewItem should not be null");
        }

        switch(tag)
        {
            case "Metadata":
                MetadataModeOptions.Visibility = Visibility.Visible;
                CropModeOptions.Visibility = Visibility.Collapsed;
                AdjustmentModeOptions.Visibility = Visibility.Collapsed;
                AnimationModeOptions.Visibility = Visibility.Collapsed;
                break;
            case "Crop":
                MetadataModeOptions.Visibility = Visibility.Collapsed;
                CropModeOptions.Visibility = Visibility.Visible;
                AdjustmentModeOptions.Visibility = Visibility.Collapsed;
                AnimationModeOptions.Visibility = Visibility.Collapsed;
                break;
            case "Adjustment":
                MetadataModeOptions.Visibility = Visibility.Collapsed;
                CropModeOptions.Visibility = Visibility.Collapsed;
                AdjustmentModeOptions.Visibility = Visibility.Visible;
                AnimationModeOptions.Visibility = Visibility.Collapsed;
                break;
            case "Animation":
                MetadataModeOptions.Visibility = Visibility.Collapsed;
                CropModeOptions.Visibility = Visibility.Collapsed;
                AdjustmentModeOptions.Visibility = Visibility.Collapsed;
                AnimationModeOptions.Visibility = Visibility.Visible;
                break;
            default:
                MetadataModeOptions.Visibility = Visibility.Collapsed;
                CropModeOptions.Visibility = Visibility.Collapsed;
                AdjustmentModeOptions.Visibility = Visibility.Collapsed;
                AnimationModeOptions.Visibility = Visibility.Collapsed;
                break;
        }
    }

    private void DropDownButton_Loaded(object sender, RoutedEventArgs e)
    {
        if(sender is DropDownButton db)
        {
            db.SelectedIndex = 1;
        }
    }

    private void CustomRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Debug.WriteLine("Reached Here");
    }

    private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.ToggleTheme();
        this.ThemeMode = ViewModel.CurrentState;
    }
}