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
    }

    private Button TestButton {  get; set; }
}
