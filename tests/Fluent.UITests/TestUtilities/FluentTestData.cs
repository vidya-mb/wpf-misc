
namespace Fluent.UITests.Utilities;

public static class FluentTestData
{
    public static List<ThemeMode> ThemeModes => new List<ThemeMode>
    {
        ThemeMode.None,
        ThemeMode.Light,
        ThemeMode.Dark,
        ThemeMode.System
    };

    public static List<object[]> ThemeModePairs => new List<object[]>
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
};
