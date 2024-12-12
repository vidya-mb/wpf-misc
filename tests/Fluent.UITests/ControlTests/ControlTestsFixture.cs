﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.UITests.ControlTests;

public enum ColorMode
{
    Light,
    Dark,
};

public class ControlTestsFixture : IDisposable
{
    public ControlTestsFixture()
    {

        foreach (ColorMode mode in Enum.GetValues(typeof(ColorMode)))
        {
            Window window = new Window();
            SetColorMode(window, mode);
            StackPanel sp = new StackPanel() { Name = "RootPanel" };
            window.Content = sp;
            window.Show();
            Windows.Add(mode, window);
        }
    }

    public void ResetWindows()
    {
        foreach (Window window in Windows.Values)
        {
            ResetWindow(window);
        }
    }

    public void Dispose()
    {
        foreach (Window window in Windows.Values)
        {
            window.Close();
        }
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

    public void ResetWindow(Window window)
    {
        if (window is null) return;
        window.Content = null;
        window.ThemeMode = ThemeMode.None;
        window.Resources.Clear();
        window.Resources.MergedDictionaries.Clear();
    }

    public int LastIndexOfFluentThemeDictionary(ResourceDictionary rd)
    {
        // Throwing here because, here we are passing application or window resources,
        // and even though when the field is null, a new RD is created and returned.
        ArgumentNullException.ThrowIfNull(rd);

        for (int i = rd.MergedDictionaries.Count - 1; i >= 0; i--)
        {
            if (rd.MergedDictionaries[i].Source != null)
            {
                if (rd.MergedDictionaries[i].Source.ToString().StartsWith(ThemeDictionaryUri,
                                                                            StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public Dictionary<ColorMode, Window> Windows { get; set; } = new Dictionary<ColorMode, Window>();

    private const string HighContrastThemeDictionaryUri = @"/PresentationFramework.Fluent;component/Themes/Fluent.HC.xaml";
    private const string ThemeDictionaryUri = "pack://application:,,,/PresentationFramework.Fluent;component/Themes/";

}
