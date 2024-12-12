
namespace Fluent.UITests.ControlTests;

public class BaseControlTests
{
    public BaseControlTests()
    {
        foreach (ColorMode mode in Enum.GetValues(typeof(ColorMode)))
        {
            Window window = new Window() { Width=800, Height=600 };
            SetColorMode(window, mode);
            StackPanel sp = new StackPanel() { Name = "RootPanel" };
            window.Content = sp;
            window.Show();
            TestWindows.Add(mode, window);
        }
    }

    public virtual List<FrameworkElement> GetStyleParts(Control element)
    {
        return new List<FrameworkElement>();
    }

    public virtual void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
    {
        return;
    }

    protected void AddControlToView(Window window, FrameworkElement fe)
    {
        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Add(fe);
    }

    protected void RemoveControlFromView(Window window, FrameworkElement fe)
    {
        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children?.Remove(fe);
    }

    public void ClearView(Window window)
    {
        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Clear();
    }

    public void ResetWindow(Window window)
    {
        if (window is null) return;
        window.Content = null;
        window.ThemeMode = ThemeMode.None;
        window.Resources.Clear();
        window.Resources.MergedDictionaries.Clear();
    }

    private void SetColorMode(Window window, ColorMode mode)
    {
        switch (mode)
        {
            case ColorMode.Light:
                window.ThemeMode = ThemeMode.Light;
                break;
            case ColorMode.Dark:
                window.ThemeMode = ThemeMode.Dark;
                break;
            //case ColorMode.HC:
            //    window.ThemeMode = ThemeMode.System;
            //    var uri = new Uri(HighContrastThemeDictionaryUri, UriKind.RelativeOrAbsolute);
            //    ResourceDictionary? rd = Application.LoadComponent(uri) as ResourceDictionary;
            //    if (rd is null)
            //    {
            //        throw new ArgumentException("HighContrast ThemeDictionary did not load correctly.");
            //    }
            //    window.Resources.MergedDictionaries.Add(rd);
            //    break;
        }
    }

    #region Test Data

    public static IEnumerable<object[]> ColorModes_TestData => new List<object[]>
    {
        new object[] { ColorMode.Light },
        new object[] { ColorMode.Dark }
        //new object[] { ColorMode.HC }
    };

    #endregion

    #region Private Members

    //private ControlTestsFixture _fixture;
    public Dictionary<ColorMode, Window> TestWindows { get; private set; } = new Dictionary<ColorMode, Window>();
    private const string HighContrastThemeDictionaryUri = @"/PresentationFramework.Fluent;component/Themes/Fluent.HC.xaml";
    private const string ThemeDictionaryUri = "pack://application:,,,/PresentationFramework.Fluent;component/Themes/";

    #endregion
}
