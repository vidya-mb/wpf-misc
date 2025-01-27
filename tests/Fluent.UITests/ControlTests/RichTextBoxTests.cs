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
            RichTextBox.IsEnabled = false;
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Disabled");
            VerifyControlProperties(RichTextBox, rd);
        }

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
                // Add your assertions here
            }
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
