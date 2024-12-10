using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fluent.UITests.Controls;

public class BaseControlTests : IClassFixture<ControlTestsFixture>
{
    public BaseControlTests(ControlTestsFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetWindows();
        foreach(ColorMode mode in Enum.GetValues(typeof(ColorMode)))
        {
            TestWindows.Add(mode, _fixture.Windows[(int)mode]);
        }
    }

    public virtual Style FindControlStyle(ThemeMode mode)
    {
        return new Style();
    }

    public virtual List<UIElement> GetStyleParts(UIElement element)
    {
        return new List<UIElement>();
    }

    public virtual void VerifyControlProperties(UIElement element, Dictionary<DependencyProperty, object> expectedProperties)
    {

    }

    public void AddControlToView(Window window, FrameworkElement fe)
    {
        StackPanel? panel = window?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Add(fe);
    }

    public void RemoveControlFromView(Window window, FrameworkElement fe)
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

    protected void SetWindowColorMode(Window window, ColorMode colorMode)
    {
        switch(colorMode)
        {
            case ColorMode.Light:
                window.ThemeMode = ThemeMode.Light;
                break;
            case ColorMode.Dark:
                window.ThemeMode = ThemeMode.Dark;
                break;
            case ColorMode.HC:
                window.ThemeMode = ThemeMode.System;
                Uri highContrastDictionaryUri = new Uri(@"/PresentationFramework.Fluent;component/Themes/Fluent.HC.xaml");
                ResourceDictionary? highContrastDictionary = Application.LoadComponent(highContrastDictionaryUri) as ResourceDictionary;
                if (highContrastDictionary != null)
                {
                    window.Resources.MergedDictionaries.Add(highContrastDictionary);
                }
                break;
        }
    }

    protected void ResetWindowColorMode(Window window)
    {
        window.ThemeMode = ThemeMode.None;
        window.Resources.Clear();
        window.Resources.MergedDictionaries.Clear();
    }

    private ControlTestsFixture _fixture;
    public Dictionary<ColorMode, Window> TestWindows { get; private set; } = new Dictionary<ColorMode, Window>();

    public static IEnumerable<object[]> ColorModes_TestData => new List<object[]>
    {
        new object[] { ColorMode.Light },
        new object[] { ColorMode.Dark },
        new object[] { ColorMode.HC }
    };

    public enum ColorMode
    { 
        Light,
        Dark,
        HC
    };
}


public class ControlTestsFixture : IDisposable
{
    public ControlTestsFixture()
    {
        for (int i = 0; i < 3; i++)
        {
            Window window = new Window();
            StackPanel sp = new StackPanel() { Name = "RootPanel" };
            window.Content = sp;
            window.Show();
            Windows.Add(window);
        }
    }

    public void ResetWindows()
    {
        foreach (Window window in Windows)
        {
            window.Content = null;
            window.ThemeMode = ThemeMode.None;
        }
    }

    public void Dispose()
    {
        foreach (Window window in Windows)
        {
            window.Close();
        }
    }

    public List<Window> Windows { get; set; } = new List<Window>();
}

