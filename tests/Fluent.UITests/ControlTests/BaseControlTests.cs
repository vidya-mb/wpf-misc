
using Fluent.UITests.TestUtilities;
using System.Windows.Media;

namespace Fluent.UITests.ControlTests;

public class BaseControlTests : IDisposable
{
    public BaseControlTests()
    {
        SetupTestWindow();
    }

    public virtual List<FrameworkElement> GetStyleParts(Control element)
    {
        return new List<FrameworkElement>();
    }

    public virtual void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
    {
        return;
    }

    #region Protected Methods

    protected void AddControlToView(Window window, FrameworkElement fe)
    {
        ArgumentNullException.ThrowIfNull(window);

        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Add(fe);
    }

    protected void RemoveControlFromView(Window window, FrameworkElement fe)
    {
        if (window is null) return;

        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children?.Remove(fe);
    }

    protected void ClearView(Window window)
    {
        if (window is null) return;

        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children?.Clear();
    }

    protected Window SetupTestWindow()
    {
        Window window = new Window() { Width = 800, Height = 600 };
        StackPanel sp = new StackPanel() { Name = "RootPanel" };
        sp.HorizontalAlignment = HorizontalAlignment.Stretch;
        sp.Orientation = Orientation.Vertical;
        window.Content = sp;
        TestWindow = window;

        return window;
    }

    protected Window SetupTestWindow(ColorMode mode)
    {
        Window window = new Window() { Width = 800, Height = 600 };
        StackPanel sp = new StackPanel() { Name = "RootPanel" };
        SetColorMode(window, mode);
        window.Content = sp;
        TestWindows[mode] = window;
        return window;
    }

    protected void ResetTestWindow(Window window)
    {
        if (window is null) return;
        window.Content = null;
        window.ThemeMode = ThemeMode.None;
        window.Resources.Clear();
        window.Resources.MergedDictionaries.Clear();
        window.Close();
    }

    protected ResourceDictionary GetTestDataDictionary(ColorMode colorMode, string testDictionaryName, string baseDictionaryName = "Default")
    {
        TestDictionary? colorDictionary = GetTestDictionary(TestDataResourceDictionary, $"{colorMode}");
        colorDictionary.Should().NotBeNull();

        ResourceDictionary rd = new ResourceDictionary();

        TestDictionary? baseDictionary = GetTestDictionary(colorDictionary, baseDictionaryName);
        TestDictionary? testDataDictionary = GetTestDictionary(colorDictionary, testDictionaryName);

        if (baseDictionary is not null)
        {
            foreach (object key in baseDictionary.Keys)
            {
                rd.Add(key, baseDictionary[key]);
            }
        }

        if (testDataDictionary is not null)
        {
            foreach (object key in testDataDictionary.Keys)
            {
                if (rd.Contains(key))
                {
                    rd[key] = testDataDictionary[key];
                }
                else
                {
                    rd.Add(key, testDataDictionary[key]);
                }
            }
        }

        return rd;
    }

    #endregion

    #region Private Methods

    private TestDictionary? GetTestDictionary(ResourceDictionary resourceDictionary, string dictionaryName)
    {
        if (string.IsNullOrEmpty(dictionaryName)) return null;

        foreach (ResourceDictionary dictionary in resourceDictionary.MergedDictionaries)
        {
            if (dictionary is TestDictionary testDicitonary)
            {
                if (testDicitonary.Name == dictionaryName)
                {
                    return testDicitonary;
                }
            }
        }

        return null;
    }

    protected void SetColorMode(Window window, ColorMode mode)
    {
        switch (mode)
        {
            case ColorMode.Light:
                window.ThemeMode = ThemeMode.Light;
                break;
            case ColorMode.Dark:
                window.ThemeMode = ThemeMode.Dark;
                break;
            case ColorMode.HC:
                window.ThemeMode = ThemeMode.None;
                var uri = new Uri(HighContrastThemeDictionaryUri, UriKind.RelativeOrAbsolute);
                ResourceDictionary? rd = Application.LoadComponent(uri) as ResourceDictionary;
                if (rd is null)
                {
                    throw new ArgumentException("HighContrast ThemeDictionary did not load correctly.");
                }
                window.Resources.MergedDictionaries.Add(rd);
                break;
        }
        window.ApplyTemplate();
    }

    public void Dispose()
    {
        foreach(ColorMode mode in Enum.GetValues(typeof(ColorMode)))
        {
            if (TestWindows.ContainsKey(mode))
            {
                ResetTestWindow(TestWindows[mode]);
            }
        }
    }

    #endregion

    #region Test Data

    public static IEnumerable<object[]> ColorModes_TestData => new List<object[]>
    {
        new object[] { ColorMode.Light },
        new object[] { ColorMode.Dark },
        new object[] { ColorMode.HC }
    };

    #endregion

    #region Public Properties

    public Dictionary<ColorMode, Window> TestWindows { get; private set; } = new Dictionary<ColorMode, Window>();
    public Window TestWindow { get; set; }
    
    protected virtual string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/";
    
    protected ResourceDictionary TestDataResourceDictionary
    {
        get
        {
            if (_testDataResourceDictionary is null)
            {
                _testDataResourceDictionary = new ResourceDictionary();
                _testDataResourceDictionary.Source = new Uri(TestDataDictionaryPath, uriKind: UriKind.RelativeOrAbsolute);
            }

            return _testDataResourceDictionary;
        }
    }

    #endregion

    #region Private Members

    //private ControlTestsFixture _fixture;

    private ResourceDictionary? _testDataResourceDictionary = null;

    private const string HighContrastThemeDictionaryUri = @"/PresentationFramework.Fluent;component/Themes/Fluent.HC.xaml";

    #endregion
}
