using Fluent.UITests.FluentAssertions;
using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xunit.Abstractions;

namespace Fluent.UITests.ControlTests
{
    public class RichTextBoxTests : BaseControlTests
    {
        public RichTextBoxTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            SetupRichTextbox();
            //RichTextBox = new RichTextBox(); // Initialize RichTextBox to avoid CS8618
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(RichTextBox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_IsEnabled_False_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            RichTextBox.Document = new FlowDocument(new Paragraph(new Run("Hello World")));
            RichTextBox.IsEnabled = false;
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Disabled");
            VerifyControlProperties(RichTextBox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_PointerOver_True_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);

            RichTextBox.Document = new FlowDocument(new Paragraph(new Run("Hello World")));

            RichTextBox.IsReadOnly = true;
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "PointerOver");
            VerifyControlProperties(RichTextBox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_IsFocused_True_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            RichTextBox.Focus(); // Set focus to the RichTextBox

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Focused");
            VerifyControlProperties(RichTextBox, rd);
        }

        #region CustomizedTests
        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_CustomSolidColorBrush_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            SetSolidColorBrushProperties();
            ResourceDictionary rd = GetTestDataDictionary(colorMode, "CustomizedBrush");
            VerifyControlProperties(RichTextBox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void RichTextBox_Custom_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            SetCustomizedProperties();
            ResourceDictionary rd = GetTestDataDictionary(colorMode, "CustomRichTextBox");
            VerifyControlProperties(RichTextBox, rd);
        }

        #endregion

        #region Override Methods

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element); 

            Border? border = element.Template.FindName("MainBorder", element) as Border;
            border.Should().NotBeNull();

            templateParts.Add(border);

            Border? accentBorder = element.Template.FindName("AccentBorder", element) as Border;
            accentBorder.Should().NotBeNull();

            templateParts.Add(accentBorder);

            ScrollViewer? scrollViewer = element.Template.FindName("PART_ContentHost", element) as ScrollViewer;
            scrollViewer.Should().NotBeNull();

            templateParts.Add(scrollViewer);

            return templateParts;
        }

        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            
           RichTextBox? richTextBox = element as RichTextBox;
            
            if (richTextBox is null) return;

            List<FrameworkElement> parts = GetStyleParts(richTextBox);

            RichTextBox? part_richTextBox = parts[0] as RichTextBox;
            Border? part_MainBorder = parts[1] as Border;
            Border? part_AccentBorder = parts[2] as Border;
            ScrollViewer? part_ContentHostScrollViewer = parts[3] as ScrollViewer;

            using (new AssertionScope())
            {
                //validate RichTextBox properties
                VerifyRichTextBoxProperties(part_richTextBox, expectedProperties);
                //validate main border properties
                VerifyMainBorderProperties(part_MainBorder, expectedProperties);
                //validate accent border properties
                VerifyAccentBorderProperties(part_AccentBorder, expectedProperties);
                //validate scroll viewer properties
                VerifyScrollViewerProperties(part_ContentHostScrollViewer, expectedProperties);
            }
        }

        private static void VerifyRichTextBoxProperties(RichTextBox? part_richTextBox, ResourceDictionary expectedProperties)
        {
            part_richTextBox.Should().NotBeNull();
            
            BrushComparer.Equal(part_richTextBox.Foreground, (Brush)expectedProperties["RichTextBoxForeground"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_richTextBox.Foreground, (Brush)expectedProperties["RichTextBoxForeground"]))
            {
                Console.WriteLine("part_richTextBox.Foreground does not match expected value");
                BrushComparer.LogBrushDifference(part_richTextBox.Foreground, (Brush)expectedProperties["RichTextBoxForeground"]);
            }
            BrushComparer.Equal(part_richTextBox.CaretBrush, (Brush)expectedProperties["RichTextBoxCaretBrush"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_richTextBox.CaretBrush, (Brush)expectedProperties["RichTextBoxCaretBrush"]))
            {
                Console.WriteLine("part_richTextBox.CaretBrush does not match expected value");
                BrushComparer.LogBrushDifference(part_richTextBox.CaretBrush, (Brush)expectedProperties["RichTextBoxCaretBrush"]);
            }
            BrushComparer.Equal(part_richTextBox.Background, (Brush)expectedProperties["RichTextBoxBackground"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_richTextBox.Background, (Brush)expectedProperties["RichTextBoxBackground"]))
            {
                Console.WriteLine("part_richTextBox.Background does not match expected value");
                BrushComparer.LogBrushDifference(part_richTextBox.Background, (Brush)expectedProperties["RichTextBoxBackground"]);
            }
            part_richTextBox.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["RichTextBox_HorizontalAlignment"]);
            part_richTextBox.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["RichTextBox_VerticalAlignment"]);
            part_richTextBox.MinHeight.Should().Be((Double)expectedProperties["RichTextBox_MinHeight"]);
            part_richTextBox.BorderThickness.Should().Be((Thickness)expectedProperties["RichTextBoxBorderThickness"]);
            part_richTextBox.Padding.Should().Be(expectedProperties["RichTextBoxPadding"]);
            part_richTextBox.FocusVisualStyle.Should().BeNull();
           // part_richTextBox.TextWrapping.Should().Be((TextWrapping)expectedProperties["RichTextBoxTextWrapping"]);
        
        }

        private static void VerifyMainBorderProperties(Border? part_MainBorder, ResourceDictionary expectedProperties)
        {
            part_MainBorder.Should().NotBeNull();
            part_MainBorder.Padding.Should().Be(expectedProperties["MainBorder_RichTextBoxPadding"]);
            BrushComparer.Equal(part_MainBorder.Background, (Brush)expectedProperties["MainBorder_RichTextBoxBackground"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_MainBorder.Background, (Brush)expectedProperties["MainBorder_RichTextBoxBackground"]))
            {
                Console.WriteLine("part_MainBorder.Background does not match expected value");
                BrushComparer.LogBrushDifference(part_MainBorder.Background, (Brush)expectedProperties["MainBorder_RichTextBoxBackground"]);
            }
            part_MainBorder.BorderThickness.Should().Be((Thickness)expectedProperties["MainBorder_RichTextBoxThickness"]);
            part_MainBorder.CornerRadius.Should().Be(expectedProperties["MainBorder_RichTextBoxCornerRadius"]);
            part_MainBorder.Focusable.Should().Be((bool)expectedProperties["MainBorder_RichTextBoxFocusable"]);           
        }

        private static void VerifyAccentBorderProperties(Border? part_AccentBorder, ResourceDictionary expectedProperties)
        {
            part_AccentBorder.Should().NotBeNull();
            part_AccentBorder.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["accentBorder_HorizontalAlignment"]);
            part_AccentBorder.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["accentBorder_VerticalAlignment"]);
            BrushComparer.Equal(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorder_BorderBrush"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorder_BorderBrush"]))
            {
                Console.WriteLine("part_AccentBorder.Background does not match expected value");
                BrushComparer.LogBrushDifference(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorder_BorderBrush"]);
            }
            part_AccentBorder.BorderThickness.Should().Be((Thickness)expectedProperties["accentBorder_Thickness"]);
            part_AccentBorder.CornerRadius.Should().Be(expectedProperties["accentBorder_CornerRadius"]);
        }
        private static void VerifyScrollViewerProperties(ScrollViewer? part_ContentHostScrollViewer, ResourceDictionary expectedProperties)
        {
            part_ContentHostScrollViewer.Should().NotBeNull();
            part_ContentHostScrollViewer.Margin.Should().Be((Thickness)expectedProperties["PART_ContentHost_RichTextBoxMargin"]);
            part_ContentHostScrollViewer.Padding.Should().Be(expectedProperties["PART_ContentHost_RichTextBoxPadding"]);
            BrushComparer.Equal(part_ContentHostScrollViewer.Foreground, (Brush)expectedProperties["PART_ContentHost_RichTextBoxForeground"]).Should().BeTrue();
            if (!BrushComparer.Equal(part_ContentHostScrollViewer.Foreground, (Brush)expectedProperties["PART_ContentHost_RichTextBoxForeground"]))
            {
                Console.WriteLine("part_ContentHostScrollViewer.Foreground does not match expected value");
                BrushComparer.LogBrushDifference(part_ContentHostScrollViewer.Foreground, (Brush)expectedProperties["PART_ContentHost_RichTextBoxForeground"]);
            }
            part_ContentHostScrollViewer.HorizontalScrollBarVisibility.Should().Be((ScrollBarVisibility)expectedProperties["PART_ContentHost_RichTextBox_HorizontalScrollBarVisibility"]);
            part_ContentHostScrollViewer.VerticalScrollBarVisibility.Should().Be((ScrollBarVisibility)expectedProperties["PART_ContentHost_RichTextBox_VerticalScrollBarVisibility"]);
        }

        private void SetSolidColorBrushProperties()
        {
            RichTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Aqua"));
            RichTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Maroon"));
            //RichTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green"));
            RichTextBox.CaretBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Yellow"));
        }
        private void SetCustomizedProperties()
        {
            RichTextBox.BorderThickness = new Thickness(4);
            RichTextBox.Padding = new Thickness(8, 5, 10, 10);
            RichTextBox.MinHeight = 40;
            RichTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            RichTextBox.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
           //RichTextBox.FocusVisualStyle= null;
        }

        #endregion

        #region Private Methods

        private void SetupRichTextbox(ColorMode mode)
        {
            RichTextBoxes[mode] = new RichTextBox() { };
            AddControlToView(TestWindows[mode], RichTextBoxes[mode]);
        }

        private void SetupRichTextbox()
        {
            RichTextBox = new RichTextBox() {  };
            AddControlToView(TestWindow, RichTextBox);
        }

        #endregion

        #region Private Properties

        private RichTextBox RichTextBox { get; set; }
        private Dictionary<ColorMode, RichTextBox> RichTextBoxes { get; set; } = new Dictionary<ColorMode, RichTextBox>();
        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/RichTextBoxTests.xaml";

        #endregion

        private ITestOutputHelper _outputHelper;
    }
}
