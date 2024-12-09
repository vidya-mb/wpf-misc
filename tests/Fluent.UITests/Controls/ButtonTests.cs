using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fluent.UITests.Controls;

public class ButtonTests : BaseControlTests, IDisposable
{
    public ButtonTests(ControlTestsFixture fixture) : base(fixture)
    {
        TestButton = new Button();
        AddControlToView(TestButton);
    }

    [WpfFact]
    [MemberData(nameof(ThemeModes))]
    public void Button_Initialization_Test(ThemeMode themeMode)
    {
        TestWindow.ThemeMode = themeMode;
        
    }


    [WpfFact]
    [MemberData(nameof(ThemeModes))]
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
        base.VerifyControlProperties(element, expectedProperties);
    }

    public void Dispose()
    {
        RemoveControlFromView(TestButton);
        TestWindow.ThemeMode = ThemeMode.None;
    }

    private Button TestButton {  get; set; }



    #region Test Data

    public static IEnumerable<object[]> ColorModes_TestData => new List<object[]>
    {
        new object[] { ColorModes.Light },
        new object[] { ColorModes.Dark },
        new object[] { ColorModes.HC }
    };

    #endregion
}
