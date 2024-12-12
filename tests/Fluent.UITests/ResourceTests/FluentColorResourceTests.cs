using FluentAssertions.Execution;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using Application = System.Windows.Application;

namespace Fluent.UITests.ResourceTests;
public class FluentColorResourceTests
{

    [WpfTheory]
    [MemberData(nameof(ColorDictionary_Validate_TestData))]
    public void Fluent_ColorDictionary_ValidateTest(string testSource, string actualSource)
    {
        ResourceDictionary dictionary1 = LoadResourceDictionary(testSource);
        ResourceDictionary dictionary2 = LoadResourceDictionary(actualSource);

        using(new AssertionScope())
        {
            List<object> missingKeys = new List<object>();
            foreach (object key in dictionary1.Keys)
            {
                if(dictionary2.Contains(key))
                {
                    if (dictionary1[key] is Color)
                    {
                        dictionary2[key].Should().BeOfType<Color>();
                        dictionary1[key].Should().Be(dictionary2[key]);
                    }
                }
                else
                {
                    missingKeys.Add(key);
                }
            }
            
            missingKeys.Should().BeEmpty();
        }
    }

    [WpfTheory]
    [MemberData(nameof(ThemeDictionary_Validate_TestData))]
    public void ThemeDictionary_FluentColor_ValidateTest(string testSource, string? actualSource)
    {
        actualSource.Should().NotBeNull();
        ResourceDictionary dictionary1 = LoadResourceDictionary(testSource);
        ResourceDictionary dictionary2 = LoadResourceDictionary(actualSource);

        using (new AssertionScope())
        {
            List<object> missingKeys = new List<object>();
            foreach (object key in dictionary1.Keys)
            {
                if (dictionary2.Contains(key))
                {
                    if (dictionary1[key] is Color)
                    {
                        dictionary2[key].Should().BeOfType<Color>();
                        dictionary1[key].Should().Be(dictionary2[key]);
                    }
                }
                else
                {
                    missingKeys.Add(key);
                }
            }

            missingKeys.Should().BeEmpty();
        }
    }


    #region Helper Methods

    private void Log_ExtraKeys(List<string> dictionary1ExtraStringKeys, string v)
    {
        Console.WriteLine(v);
        if (dictionary1ExtraStringKeys.Count == 0)
        {
            Console.WriteLine("None\n");
            return;
        }

        foreach (string key in dictionary1ExtraStringKeys)
        {
            Console.WriteLine(key);
        }
        Console.WriteLine();
    }

    private static ResourceDictionary LoadResourceDictionary(string source)
    {
        var uri = new Uri(source, UriKind.RelativeOrAbsolute);
        ResourceDictionary? resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
        resourceDictionary.Should().NotBeNull();
        return resourceDictionary;
    }

    private static int GetResourceKeysFromResourceDictionary(ResourceDictionary resourceDictionary,
        out List<string> stringResourceKeys,
        out List<object> objectResourceKeys)
    {
        ArgumentNullException.ThrowIfNull(resourceDictionary, nameof(resourceDictionary));
        stringResourceKeys = new List<string>();
        objectResourceKeys = new List<object>();

        int resourceDictionaryKeysCount = resourceDictionary.Count;

        foreach (object key in resourceDictionary.Keys)
        {
            if (key is string skey)
            {
                stringResourceKeys.Add(skey);
            }
            else
            {
                objectResourceKeys.Add(key);
            }
        }

        return resourceDictionaryKeysCount;
    }

    #endregion


    #region Test Data

    public static IEnumerable<object[]> ColorDictionary_Validate_TestData()
    {
        int count = ColorDictionarySourceList.Count;
        for (int i = 0; i < count; i++)
        {
            yield return new object[] { TestColorDictionarySourceList[i], ColorDictionarySourceList[i] };
        }
    }

    public static IEnumerable<object[]> ThemeDictionary_Validate_TestData()
    {
        int count = ThemeDictionarySourceList.Count;
        for (int i = 1; i < count; i++)
        {
            yield return new object[] { TestColorDictionarySourceList[i - 1], ThemeDictionarySourceList[i][0] as string };
        }
    }

    public static IList<object[]> ThemeDictionarySourceList
        = new List<object[]>
            {
                new object[] { $"{ThemeDictionaryPath}/Fluent.xaml" },
                new object[] { $"{ThemeDictionaryPath}/Fluent.Light.xaml" },
                new object[] { $"{ThemeDictionaryPath}/Fluent.Dark.xaml" },
                new object[] { $"{ThemeDictionaryPath}/Fluent.HC.xaml" },
            };

    public static IList<string> ColorDictionarySourceList
        = new List<string>
        {
            $"{ColorDictionaryPath}/Light.xaml",
            $"{ColorDictionaryPath}/Dark.xaml",
            $"{ColorDictionaryPath}/HC.xaml"
        };

    public static IList<string> TestColorDictionarySourceList
        = new List<string>
        {
            $"{TestColorDictionaryPath}/Light.Test.xaml",
            $"{TestColorDictionaryPath}/Dark.Test.xaml",
            $"{TestColorDictionaryPath}/HC.Test.xaml"
        };

    private const string ThemeDictionaryPath = @"/PresentationFramework.Fluent;component/Themes";
    private const string ColorDictionaryPath = @"/PresentationFramework.Fluent;component/Resources/Theme";
    private const string TestColorDictionaryPath = @"/Fluent.UITests;component/ResourceTests/Data";

    #endregion

}
