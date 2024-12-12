using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Fluent.UITests.ControlTests;

public class ButtonTests : BaseControlTests, IDisposable
{
    public ButtonTests()
    {
        foreach (ColorMode colorMode in Enum.GetValues(typeof(ColorMode)))
        {
            TestButtons[colorMode] = new Button() { Content = "Hello" };
            AddControlToView(TestWindows[colorMode], TestButtons[colorMode]);
        }
    }

    [WpfTheory]
    [MemberData(nameof(ColorModes_TestData))]
    public void Button_Initialization_Test(ColorMode colorMode)
    {
        ResourceDictionary rd = GetTestDataDictionary(colorMode, "");

        Button button = TestButtons[colorMode];
        button.ApplyTemplate();
        Thread.Sleep(1000);
        VerifyControlProperties(button, rd);
    }


    [WpfTheory]
    [MemberData(nameof(ColorModes_TestData))]
    public void Button_IsEnabled_False_Test(ColorMode colorMode)
    {
        ResourceDictionary rd = GetTestDataDictionary(colorMode, "Disabled");

        Button button = TestButtons[colorMode];
        button.IsEnabled = false;
        button.ApplyTemplate();

        VerifyControlProperties(button, rd);
    }

    public override List<FrameworkElement> GetStyleParts(Control element)
    {
        List<FrameworkElement> templateParts = new List<FrameworkElement>();
        templateParts.Add(element);

        Border? border = element.Template.FindName("ContentBorder", element) as Border;
        border.Should().NotBeNull();

        templateParts.Add(border);

        ContentPresenter? contentPresenter = element.Template.FindName("ContentPresenter", element) as ContentPresenter;
        contentPresenter.Should().NotBeNull();

        templateParts.Add(contentPresenter);

        return templateParts;
    }

    public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
    {
        Button? button = element as Button;
        
        if (button is null) return;

        List<FrameworkElement> parts = GetStyleParts(button);

        Button? part_Button = parts[0] as Button;
        Border? part_ContentBorder = parts[1] as Border;
        ContentPresenter? part_ContentPresenter = parts[2] as ContentPresenter;
    
        using(new AssertionScope())
        {
            part_Button.Should().NotBeNull();
            AreBrushesEqual(part_Button.Background, (Brush)expectedProperties["Button_Background"]);
            AreBrushesEqual(part_Button.Foreground, (Brush)expectedProperties["Button_Foreground"]);
            part_Button.BorderThickness.Should().Be((Thickness)expectedProperties["Button_BorderThickness"]);
            part_Button.IsTabStop.Should().Be((bool)expectedProperties["Button_IsTabStop"]);
            part_Button.Padding.Should().Be(expectedProperties["Button_Padding"]);
            part_Button.Margin.Should().Be(expectedProperties["Button_Margin"]);
            part_Button.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["Button_HorizontalAlignment"]);
            part_Button.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["Button_VerticalAlignment"]);
            part_Button.HorizontalContentAlignment.Should().Be((HorizontalAlignment)expectedProperties["Button_HorizontalContentAlignment"]);
            part_Button.VerticalContentAlignment.Should().Be((VerticalAlignment)expectedProperties["Button_VerticalContentAlignment"]);
            part_Button.Focusable.Should().Be((bool)expectedProperties["Button_Focusable"]);
            part_Button.Cursor.Should().Be((Cursor)expectedProperties["Button_Cursor"]);
        
            part_ContentBorder.Should().NotBeNull();
            AreBrushesEqual(part_ContentBorder.Background, (Brush)expectedProperties["ContentBorder_Background"]);
            part_ContentBorder.BorderThickness.Should().Be((Thickness)expectedProperties["ContentBorder_BorderThickness"]);
            part_ContentBorder.CornerRadius.Should().Be((CornerRadius)expectedProperties["ContentBorder_CornerRadius"]);
            part_ContentBorder.Margin.Should().Be((Thickness)expectedProperties["ContentBorder_Margin"]);
            part_ContentBorder.Padding.Should().Be((Thickness)expectedProperties["ContentBorder_Padding"]);
            part_ContentBorder.Focusable.Should().Be((bool)expectedProperties["ContentBorder_Focusable"]);
            part_ContentBorder.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ContentBorder_HorizontalAlignment"]);
            part_ContentBorder.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ContentBorder_VerticalAlignment"]);
        
            part_ContentPresenter.Should().NotBeNull();
            AreBrushesEqual(TextElement.GetForeground(part_ContentPresenter), (Brush)expectedProperties["ContentPresenter_Foreground"]);
            part_ContentPresenter.RecognizesAccessKey.Should().Be((bool)expectedProperties["ContentPresenter_RecognizesAccessKey"]);
            part_ContentPresenter.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ContentPresenter_HorizontalAlignment"]);
            part_ContentPresenter.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ContentPresenter_VerticalAlignment"]);
            part_ContentPresenter.Margin.Should().Be((Thickness)expectedProperties["ContentPresenter_Margin"]);
            part_ContentPresenter.Focusable.Should().Be((bool)expectedProperties["ContentPresenter_Focusable"]);
        }
    }

    private bool AreBrushesEqual(Brush actualBrush, Brush expectedBrush)
    {
        if(actualBrush is null || expectedBrush is null)
        {
            if(actualBrush is null && expectedBrush is null)
            {
                return true;
            }

            return false;
        }

        if(actualBrush.GetType() != expectedBrush.GetType())
        {
            return false;
        }


        if(actualBrush is SolidColorBrush actualSCB && expectedBrush is SolidColorBrush expectedSCB)
        {
            return actualSCB.Color == expectedSCB.Color && actualSCB.Opacity == expectedSCB.Opacity;
        }

        return false;
    }

    public void Dispose()
    {
        foreach (ColorMode colorMode in Enum.GetValues(typeof(ColorMode)))
        {
            RemoveControlFromView(TestWindows[colorMode], TestButtons[colorMode]);
            ResetWindow(TestWindows[colorMode]);
        }
    }

    #region Helper Methods

    private ResourceDictionary GetTestDataDictionary(ColorMode colorMode, string testDictionaryName, string baseDictionaryName = "Default")
    {
        TestResourceDictionary? colorDictionary = GetTestDicitonary(TestDataResourceDictionary, $"ButtonTests_{colorMode}");
        colorDictionary.Should().NotBeNull();

        ResourceDictionary rd = new ResourceDictionary();

        TestResourceDictionary? baseDictionary = GetTestDicitonary(colorDictionary, baseDictionaryName);
        TestResourceDictionary? testDataDictionary = GetTestDicitonary(colorDictionary, testDictionaryName);

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
                if(rd.Contains(key))
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

    public TestResourceDictionary? GetTestDicitonary(ResourceDictionary resourceDictionary, string dictionaryName)
    {
        if (string.IsNullOrEmpty(dictionaryName)) return null;

        foreach(ResourceDictionary dictionary in resourceDictionary.MergedDictionaries)
        {
            if (dictionary is TestResourceDictionary testDicitonary)
            {
                if(testDicitonary.Name == dictionaryName)
                {
                    return testDicitonary;
                }
            }
        }

        return null;
    }

    #endregion

    #region Private Properties

    private Dictionary<ColorMode, Button> TestButtons { get; set; } = new Dictionary<ColorMode, Button>();

    private ResourceDictionary TestDataResourceDictionary
    {
        get
        {
            if(_testDataResourceDictionary is null)
            {
                _testDataResourceDictionary = new ResourceDictionary();
                _testDataResourceDictionary.Source = new Uri(TestDataDictionaryPath, uriKind: UriKind.RelativeOrAbsolute);
            }

            return _testDataResourceDictionary;
        }
    }

    #endregion

    #region Private Fields

    private ResourceDictionary? _testDataResourceDictionary = null;
    private const string TestDataDictionaryPath = @"/Fluent.UITests;component/ControlTests/Data/ButtonTests.xaml";

    #endregion
}

