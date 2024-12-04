using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fluent.UITests.Utilities;

public static class FluentTestData
{
    public static IEnumerable<object[]> ThemeModes => new List<object[]>
    {
        new object[] { ThemeMode.None },
        new object[] { ThemeMode.Dark },
        new object[] { ThemeMode.Light },
        new object[] { ThemeMode.System }
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
};
