using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Win11ThemeGallery.Navigation;
using Win11ThemeGallery.Views;

namespace Win11ThemeGallery.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "WPF UI Gallery";

    [ObservableProperty]
    private ICollection<NavigationItem> _controls = new ObservableCollection<NavigationItem>
    {
        new NavigationItem
        {
            Name = "Controls",
            PageType = null,
            Children = new ObservableCollection<NavigationItem>
            {
                new NavigationItem("Button", typeof(ButtonPage)),
                new NavigationItem("CheckBox", typeof(CheckBoxPage)),
                new NavigationItem("ComboBox", typeof(ComboBoxPage)),
                new NavigationItem("RadioButton", typeof(RadioButtonPage)),
                new NavigationItem("Slider", typeof(SliderPage)),
                new NavigationItem("Calendar", typeof(CalendarPage)),
                new NavigationItem("DatePicker", typeof(DatePickerPage)),
                new NavigationItem("TabControl", typeof(TabControlPage)),
                new NavigationItem("ProgressBar", typeof(ProgressBarPage)),
                new NavigationItem("Menu", typeof(MenuPage)),
                new NavigationItem("ToolTip", typeof(ToolTipPage)),
                new NavigationItem("Canvas", typeof(CanvasPage)),
                new NavigationItem("Expander", typeof(ExpanderPage)),
                new NavigationItem("Image", typeof(ImagePage)),
                new NavigationItem("DataGrid", typeof(DataGridPage)),
                new NavigationItem("ListBox", typeof(ListBoxPage)),
                new NavigationItem("ListView", typeof(ListViewPage)),
                new NavigationItem("TreeView", typeof(TreeViewPage)),
            }
        },
        new NavigationItem
        {
            Name = "Text",
            PageType = null,
            Children = new ObservableCollection<NavigationItem>
            {
                new NavigationItem("Label", typeof(LabelPage)),
                new NavigationItem("TextBox", typeof(TextBoxPage)),
                new NavigationItem("TextBlock", typeof(TextBlockPage)),
                new NavigationItem("RichTextEdit", typeof(RichTextEditPage)),
                new NavigationItem("PasswordBox", typeof(PasswordBoxPage)),
            }
        },
        new NavigationItem("Settings", typeof(SettingsPage)),
    };

    [ObservableProperty]
    private NavigationItem _selectedControl;

}
