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
        foreach(ColorModes mode in Enum.GetValues(typeof(ColorModes)))
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

    public void AddControlToView(FrameworkElement fe)
    {
        StackPanel? panel = TestWindow?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Add(fe);
    }

    public void RemoveControlFromView(FrameworkElement fe)
    {
        StackPanel? panel = TestWindow?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children?.Remove(fe);
    }

    public void ClearView()
    {
        StackPanel? panel = TestWindow?.Content as StackPanel;
        panel.Should().NotBeNull();
        panel.Children.Clear();
    }



    private ControlTestsFixture _fixture;
    public Dictionary<ColorModes, Window> TestWindows { get; private set; } = new Dictionary<ColorModes, Window>();

    public static IEnumerable<object[]> ColorModes_TestData => new List<object[]>
    {
        new object[] { ColorModes.Light },
        new object[] { ColorModes.Dark },
        new object[] { ColorModes.HC }
    };

    public enum ColorModes
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

