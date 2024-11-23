using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace Fluent.UITests;
public class ResourceDictionaryTests
{
    [WpfTheory]
    [MemberData(nameof(Fluent_RD_SourceList))]
    public void Fluent_ResourceDictionary_LoadTests(string source)
    {
        LoadFluentResourceDictionary(source);
    }

    [WpfFact]
    public void Fluent_ResourceDictionary_MatchKeysTest()
    {
        ResourceDictionary fluentLightDictionary = LoadFluentResourceDictionary((string)Fluent_RD_SourceList[1][0]);
        ResourceDictionary fluentDarkDictionary = LoadFluentResourceDictionary((string)Fluent_RD_SourceList[2][0]);
        //ResourceDictionary? fluentHighContrastDictionary = LoadFluentResourceDictionary((string)Fluent_RD_SourceList[2][0]);

        // Extract count and list of keys
        List<string> fluentStringResourceKeys = new List<string>();
        List<object> fluentObjectResourceKeys = new List<object>();
        int fluentResourceKeysCount = GetResourceKeysFromResourceDictionary(fluentLightDictionary,
            out fluentStringResourceKeys, out fluentObjectResourceKeys);

        // Checking that keys collection should not be null, and count should not be 0
        fluentResourceKeysCount.Should().NotBe(0);

        int fluentDarkResourceKeysCount = GetResourceKeysFromResourceDictionary(fluentDarkDictionary,
            out List<string> fluentDarkStringResourceKeys, out List<object> fluentDarkObjectResourceKeys);

        Match_ResourceKeys(fluentStringResourceKeys, fluentDarkStringResourceKeys).Should().BeTrue();
        Match_ResourceKeys(fluentObjectResourceKeys, fluentDarkObjectResourceKeys).Should().BeTrue();
        fluentResourceKeysCount.Should().Be(fluentDarkResourceKeysCount);

        // Add test for HC ResourceDictionary
    }

    private bool Match_ResourceKeys(List<object> expectedKeys, List<object> actualKeys)
    {
        int missingCount = 0;
        foreach(object key in actualKeys)
        {
            bool found = false;
            foreach(object value in expectedKeys)
            {
                if(key.Equals(value))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"{key.ToString()} not found");
                Console.WriteLine(nameof(key));
                missingCount++;
            }
        }
        Console.WriteLine($"Missing Count : {missingCount}");
        return missingCount == 0;
    }

    private bool Match_ResourceKeys(List<string> expectedKeys, List<string> actualKeys)
    {
        foreach (string key in actualKeys)
        {
            int index = expectedKeys.FindIndex(x => x == key);
            if (index == -1)
            {
                Console.WriteLine($"{key} not found");
                return false;
            }
        }

        foreach (string key in expectedKeys)
        {
            int index = actualKeys.FindIndex(x => x == key);
            if (index == -1)
            {
                Console.WriteLine($"{key} not found");
                return false;
            }
        }

        return true;
    }

    public static IList<object[]> Fluent_RD_SourceList
    = new List<object[]>
        {
            new object[] { "/PresentationFramework.Fluent;component/Themes/Fluent.xaml" },
            new object[] { "/PresentationFramework.Fluent;component/Themes/Fluent.Light.xaml" },
            new object[] { "/PresentationFramework.Fluent;component/Themes/Fluent.Dark.xaml" },
            new object[] { "/PresentationFramework.Fluent;component/Themes/Fluent.HC.xaml" }
        };

    private static ResourceDictionary LoadFluentResourceDictionary(string source)
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
}
