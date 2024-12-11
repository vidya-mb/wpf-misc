using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fluent.UITests.ControlTests;

public class ButtonTests : BaseControlTests, IDisposable
{
    public ButtonTests(ControlTestsFixture fixture) : base(fixture)
    {
        foreach(ColorMode colorMode in Enum.GetValues(typeof(ColorMode)))
        {
            TestButtons[colorMode] = new Button();
            AddControlToView(TestWindows[colorMode], TestButtons[colorMode]);
        }
    }

    [WpfFact]
    [MemberData(nameof(ColorModes_TestData))]
    public void Button_Initialization_Test(ColorMode colorMode)
    {
        Button button = TestButtons[colorMode];
        VerifyControlProperties(button, )
    }


    [WpfFact]
    [MemberData(nameof(ColorModes_TestData))]
    public void Button_IsEnabled_False_Test(ThemeMode themeMode)
    {

    }




    public override Style FindControlStyle(ThemeMode mode)
    {
        return base.FindControlStyle(mode);
    }

    public override List<UIElement> GetStyleParts(UIElement element)
    {
        return base.GetStyleParts(element);
    }

    public override void VerifyControlProperties(UIElement element, Dictionary<DependencyProperty, object> expectedProperties)
    {
        
    }

    public void Dispose()
    {
        foreach (ColorMode colorMode in Enum.GetValues(typeof(ColorMode)))
        {
            RemoveControlFromView(TestWindows[colorMode], TestButtons[colorMode]);
            ResetWindow(TestWindows[colorMode]);
        }
    }

    private Dictionary<ColorMode,Button> TestButtons {  get; set; } = new Dictionary<ColorMode,Button>();

    #region Test Data

    public static 

    #endregion
}
