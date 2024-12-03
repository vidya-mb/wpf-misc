using FluentAssertions;
using System.Windows;
using System.Windows.Media;

namespace Fluent.UITests;

[Collection("Application Tests Collection")]
public class ApplicationThemeModeTests : IDisposable
{
    ApplicationFixture _fixture;
    
    public ApplicationThemeModeTests(ApplicationFixture fixture)
    {
        _fixture = fixture;
    }

    [WpfFact]
    public void Application_ThemeMode_Default()
    {
        _fixture.Execute(() =>
        {
            Window window = new Window();
            _fixture.App.MainWindow = window;
            window.ApplyTemplate();
        });
        
        _fixture.VerifyThemeModeAndResources("None", 0);
        Window window = _fixture.GetMainWindow();
        
        _fixture.Execute(() =>
        {
            window.Should().NotBeNull();
            window.Background.Should().BeNull();
            window.Resources.MergedDictionaries.Should().BeEmpty();
        });
    }


    #region Helper Methods

    private void Verify_WindowProperties(Window window, ThemeMode themeMode)
    {
        if (themeMode == ThemeMode.None)
        {
            window.Background.ToString().Should().Be(Brushes.White.ToString());
            window.Foreground.ToString().Should().Be(Brushes.Black.ToString());
            window.Resources.MergedDictionaries.Should().HaveCount(0);
            return;
        }

        window.Background.Should().Be(Brushes.Transparent);

    }

    private void Verify_ApplicationResources(Application application, ThemeMode themeMode)
    {
        if (themeMode == ThemeMode.None)
        {
            application.Resources.MergedDictionaries.Should().BeEmpty();
            return;
        }

        application.Resources.MergedDictionaries.Should().HaveCount(1);
        Uri source = application.Resources.MergedDictionaries[0].Source;
        source.AbsoluteUri.ToString()
            .Should().EndWith(FluentThemeResourceDictionaryMap[themeMode]);
    }

    private void Verify_WindowResources(Window window, ThemeMode themeMode)
    {
        if (themeMode == ThemeMode.None)
        {
            window.Resources.MergedDictionaries.Should().BeEmpty();
            return;
        }

        window.Resources.MergedDictionaries.Should().HaveCount(1);

        Uri source = window.Resources.MergedDictionaries[0].Source;
        source.AbsoluteUri.ToString()
            .Should().Be(FluentThemeResourceDictionaryMap[themeMode]);
    }

    public void Dispose()
    {
        _fixture.ResetApplicationInstance();
    }

    #endregion

    #region Test Data

    public static IEnumerable<object[]> ThemeModes => new List<object[]>
    {
        new object[] { ThemeMode.None },
        new object[] { ThemeMode.System },
        new object[] { ThemeMode.Light },
        new object[] { ThemeMode.Dark }
    };

    public static IEnumerable<object[]> ThemeModePairs => new List<object[]>
    {
        new object[] { ThemeMode.None, ThemeMode.None },
        new object[] { ThemeMode.None, ThemeMode.Light },
        new object[] { ThemeMode.None, ThemeMode.Dark },
        new object[] { ThemeMode.None, ThemeMode.System },
        new object[] { ThemeMode.Light, ThemeMode.None },
        new object[] { ThemeMode.Light, ThemeMode.Light },
        new object[] { ThemeMode.Light, ThemeMode.Dark },
        new object[] { ThemeMode.Light, ThemeMode.System },
        new object[] { ThemeMode.Dark, ThemeMode.None },
        new object[] { ThemeMode.Dark, ThemeMode.Light },
        new object[] { ThemeMode.Dark, ThemeMode.Dark },
        new object[] { ThemeMode.Dark, ThemeMode.System },
        new object[] { ThemeMode.System, ThemeMode.None },
        new object[] { ThemeMode.System, ThemeMode.Light },
        new object[] { ThemeMode.System, ThemeMode.Dark },
        new object[] { ThemeMode.System, ThemeMode.System }
    };

    private static Dictionary<ThemeMode, string> FluentThemeResourceDictionaryMap
        = new Dictionary<ThemeMode, string>
            {
                { ThemeMode.None, ""},
                { ThemeMode.System, "pack://application:,,,/PresentationFramework.Fluent;component/Themes/Fluent.xaml"},
                { ThemeMode.Light, "pack://application:,,,/PresentationFramework.Fluent;component/Themes/Fluent.Light.xaml"},
                { ThemeMode.Dark, "pack://application:,,,/PresentationFramework.Fluent;component/Themes/Fluent.Dark.xaml"},
            };

    #endregion
}