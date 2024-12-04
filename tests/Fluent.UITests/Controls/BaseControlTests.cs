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
        _fixture.ResetWindow();
        TestWindow = fixture.Window;
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
    public Window TestWindow { get; private set; }

}


public class ControlTestsFixture : IDisposable
{
    public ControlTestsFixture()
    {
        Window = new Window();
        StackPanel sp = new StackPanel() { Name = "RootPanel" };
        Window.Content = sp;
        Window.Show();
    }

    public void ResetWindow()
    {
        Window.Content = null;
        Window.ThemeMode = ThemeMode.None;
    }

    public void Dispose()
    {
        Window.Close();
    }

    public Window Window { get; set; }
}

