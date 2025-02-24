using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Xunit.Abstractions;

namespace Fluent.UITests.ControlTests
{
    public class ToolTipTests : BaseControlTests
    {
        public ToolTipTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            SetupToolTip();
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void ToolTip_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            Thread.Sleep(2000);
            
            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(ToolTip, rd);
        }

        #region Override Methods

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element);

            Border? border = element.Template.FindName("Border", element) as Border;
            border.Should().NotBeNull();
            templateParts.Add(border);
           
            ContentPresenter? contentPresenter = border.Child as ContentPresenter;
            contentPresenter.Should().NotBeNull();
            templateParts.Add(contentPresenter);

            return templateParts;
        }

        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            ToolTip? toolTip = element as ToolTip;
            
            if (toolTip is null) return;

            List<FrameworkElement> parts = GetStyleParts(toolTip);

            ToolTip? part_ToolTip = parts[0] as ToolTip;
            Border? part_Border = parts[1] as Border;
            ContentPresenter? part_borderContentPresenter = parts[2] as ContentPresenter;
            using (new AssertionScope())
            {
                VerifyToolTioBorderProperties(part_Border, expectedProperties);
                VerifyToolTioBorderContentPresenterProperties(part_borderContentPresenter, expectedProperties);
            }
        }

        public static void VerifyToolTioBorderProperties(Border? part_Border, ResourceDictionary expectedProperties)
        {            
            part_Border.Should().NotBeNull();
            //part_Border.Height.Should().Be((double)expectedProperties["ToolTip_Height"]);
            part_Border.MaxWidth.Should().Be((double)expectedProperties["ToolTip_MaxWidth"]);
            BrushComparer.Equal(part_Border.Background, (Brush)expectedProperties["ToolTipBackground"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_Border.Background, (Brush)expectedProperties["ToolTipBackground"]))
            {
                Console.WriteLine("part_ToolTip.Background does not match expected value");
                BrushComparer.LogBrushDifference(part_Border.Background, (Brush)expectedProperties["ToolTipBackground"]);
            }
            BrushComparer.Equal(part_Border.BorderBrush, (Brush)expectedProperties["ToolTipBorderBrush"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_Border.BorderBrush, (Brush)expectedProperties["ToolTipBorderBrush"]))
            {
                Console.WriteLine("part_Border.BorderBrush does not match expected value");
                BrushComparer.LogBrushDifference(part_Border.BorderBrush, (Brush)expectedProperties["ToolTipBorderBrush"]);
            }
            part_Border.BorderThickness.Should().Be((Thickness)expectedProperties["ToolTip_BorderThemeThickness"]);
            part_Border.CornerRadius.Should().Be((CornerRadius)expectedProperties["ToolTip_CornerRadius"]);
            part_Border.SnapsToDevicePixels.Should().Be((bool)expectedProperties["ToolTip_SnapsToDevicePixels"]);

        
        }


        public static void VerifyToolTioBorderContentPresenterProperties(ContentPresenter? part_borderContentPresenter, ResourceDictionary expectedProperties)
        {
            part_borderContentPresenter.Should().NotBeNull();
            part_borderContentPresenter.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ContentPresenter_HorizontalContentAlignment"]);
            part_borderContentPresenter.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ContentPresenter_VerticalContentAlignment"]);
            // part_Border.MaxWidth.Should().Be((double)expectedProperties["ToolTip_MaxWidth"]);



        }
        #endregion

        #region Private Methods

        private void SetupToolTip()
        {
            // Create a new Button
            Button button = new Button
            {
                Content = "Hover over me",
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            button.MouseUp += (sender, args) =>
            {
                ToolTip.IsOpen = true;
            };
            // declare content for a ToolTip
            ToolTip = new ToolTip() 
            { 
                Content = "This is a Tooltip"                
            };
            ToolTip.SetResourceReference(ToolTip.StyleProperty,typeof(ToolTip));
            // Set the ToolTip to the Button
            button.ToolTip = ToolTip;
            
            // Add the Button to the TestWindow
            AddControlToView(TestWindow, button);
            
            // Store the Button and ToolTip for further use
            Button = button;
            ToolTip = ToolTip;
            ToolTip.ApplyTemplate();
            ToolTip.IsOpen = true;
        }

        #endregion

        #region Private Properties

        private ToolTip ToolTip { get; set; }
        private Button Button { get; set; }
        private Dictionary<ColorMode, ToolTip> ToolTips { get; set; } = new Dictionary<ColorMode, ToolTip>();
        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/ToolTipTests.xaml";

        #endregion

        private ITestOutputHelper _outputHelper;
    }
}
