using Fluent.UITests.FluentAssertions;
using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xunit.Abstractions;
using LinearGradientBrush = System.Windows.Media.LinearGradientBrush;

namespace Fluent.UITests.ControlTests
{
    public class TextBoxTests : BaseControlTests
    {
        public TextBoxTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            SetupTestButton();
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void TextBox_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(TestBoxButton, rd);
        }

        #region Override Methods

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element);

            Border? border = element.Template.FindName("ContentBorder", element) as Border;
            border.Should().NotBeNull();

            templateParts.Add(border);

            Border? borderAccent = element.Template.FindName("AccentBorder", element) as Border;
            borderAccent.Should().NotBeNull();

            templateParts.Add(borderAccent);

            Button? clearButton = element.Template.FindName("ClearButton", element) as Button;
            clearButton.Should().NotBeNull();

            templateParts.Add(clearButton);

            return templateParts;
        }

        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            TextBox? textbox = element as TextBox;
            //Button? button = element as Button;

            if (textbox is null) return;

            List<FrameworkElement> parts = GetStyleParts(textbox);

            TextBox? part_TextBox = parts[0] as TextBox;
            Border? part_ContentBorder = parts[1] as Border;
            Border? part_AccentBorder = parts[2] as Border;
            Button? part_ClearButton = parts[3] as Button;
            using (new AssertionScope())
            {
                part_TextBox.Should().NotBeNull();
                BrushComparer.Equal(part_TextBox.Background, (Brush)expectedProperties["TextControlBackground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_TextBox.Background, (Brush)expectedProperties["TextControlBackground"]))
                {
                    Console.WriteLine("part_TextBox.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_TextBox.Background, (Brush)expectedProperties["TextControlBackground"]);
                }

                BrushComparer.Equal(part_TextBox.Foreground, (Brush)expectedProperties["TextControlForeground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_TextBox.Foreground, (Brush)expectedProperties["TextControlForeground"]))
                {
                    Console.WriteLine("part_TextBox.Foreground does not match expected value");
                    BrushComparer.LogBrushDifference(part_TextBox.Foreground, (Brush)expectedProperties["TextControlForeground"]);
                }

                BrushComparer.Equal(part_TextBox.CaretBrush, (Brush)expectedProperties["TextboxCaretBrush"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_TextBox.CaretBrush, (Brush)expectedProperties["TextboxCaretBrush"]))
                {
                    Console.WriteLine("part_TextBox.CaretBrush does not match expected value");
                    BrushComparer.LogBrushDifference(part_TextBox.CaretBrush, (Brush)expectedProperties["TextboxCaretBrush"]);
                }
              
                part_TextBox.BorderThickness.Should().Be((Thickness)expectedProperties["TextBoxBorderThemeThickness"]);

                //ContentBorder properties
                part_ContentBorder.Should().NotBeNull();
                part_ContentBorder.MinWidth.Should().Be((double)expectedProperties["ContentBorder_TextControlThemeMinWidth"]);
                part_ContentBorder.MinHeight.Should().Be((double)expectedProperties["ContentBorder_TextControlThemeMinHeight"]);
                part_ContentBorder.Padding.Should().Be(expectedProperties["ContentBorder_TextControlThemePadding"]);
                part_ContentBorder.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ContentBorder_TextBox_HorizontalAlignment"]);
                part_ContentBorder.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ContentBorder_TextBox_VerticalAlignment"]);
                BrushComparer.Equal(part_ContentBorder.Background, (Brush)expectedProperties["ContentBorder_TextControlBackground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ContentBorder.Background, (Brush)expectedProperties["ContentBorder_TextControlBackground"]))
                {
                    Console.WriteLine("part_ContentBorder.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_ContentBorder.Background, (Brush)expectedProperties["ContentBorder_TextControlBackground"]);
                }                
                
                part_ContentBorder.BorderThickness.Should().Be((Thickness)expectedProperties["ContentBorder_ThemeThickness"]);
                part_ContentBorder.CornerRadius.Should().Be((CornerRadius)expectedProperties["ContentBorder_CornerRadius"]);       
                
                //ContentHost.ScrollViewer properties                            
                // Get the ScrollViewer from the visual tree of the TextBox
                var scrollViewer = FindScrollViewer(part_TextBox);         
                ScrollViewer PART_ContentHost_scrollViewer = (ScrollViewer)expectedProperties["PART_ContentHost:ScrollViewer"];
                scrollViewer.HorizontalScrollBarVisibility.Should().Be(PART_ContentHost_scrollViewer.HorizontalScrollBarVisibility);
                scrollViewer.VerticalAlignment.Should().Be(PART_ContentHost_scrollViewer.VerticalAlignment);
                scrollViewer.IsDeferredScrollingEnabled.Should().Be(PART_ContentHost_scrollViewer.IsDeferredScrollingEnabled);
                scrollViewer.IsTabStop.Should().Be(PART_ContentHost_scrollViewer.IsTabStop); //Test fails as IsTabStop returns true expected is false                
                scrollViewer.CanContentScroll.Should().Be(PART_ContentHost_scrollViewer.CanContentScroll); //Test fails as CanContentScroll returns true expected is false
                scrollViewer.Padding.Should().Be(PART_ContentHost_scrollViewer.Padding); //Test fails as padding returns 10,5,6,6 expected is 10,5,10,6
                scrollViewer.VerticalScrollBarVisibility.Should().Be(PART_ContentHost_scrollViewer.VerticalScrollBarVisibility);               

                //AccentBorder properties
                part_AccentBorder.Should().NotBeNull();
                part_AccentBorder.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["accentBorder_HorizontalAlignment"]);
                part_AccentBorder.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["accentBorder_VerticalAlignment"]);
                BrushComparer.Equal(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorderBrush"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorderBrush"]))
                {
                    Console.WriteLine("part_AccentBorder.BorderBrush does not match expected value");
                    BrushComparer.LogBrushDifference(part_AccentBorder.BorderBrush, (Brush)expectedProperties["accentBorderBrush"]);
                }
                part_AccentBorder.BorderThickness.Should().Be((Thickness)expectedProperties["TextBoxAccentBorderThemeThickness"]);
                part_AccentBorder.CornerRadius.Should().Be((CornerRadius)expectedProperties["accentBorder_CornerRadius"]);

                //ClearButton properties
                part_ClearButton.Should().NotBeNull();
                part_ClearButton.MinWidth.Should().Be((double)expectedProperties["ClearButton_TextControlThemeMinWidth"]);
                part_ClearButton.MinHeight.Should().Be((double)expectedProperties["ClearButton_TextControlThemeMinHeight"]);
                part_ClearButton.Margin.Should().Be(expectedProperties["ClearButton_Margin"]);
                part_ClearButton.Padding.Should().Be(expectedProperties["ClearButton_Padding"]);
                part_ClearButton.HorizontalAlignment.Should().Be((HorizontalAlignment?)expectedProperties["ClearButton_HorizontalAlignment"]);
                part_ClearButton.VerticalAlignment.Should().Be((VerticalAlignment?)expectedProperties["ClearButton_VerticalAlignment"]);
                part_ClearButton.HorizontalContentAlignment.Should().Be((HorizontalAlignment?)expectedProperties["ClearButton_HorizontalContentAlignment"]);
                part_ClearButton.VerticalContentAlignment.Should().Be((VerticalAlignment?)expectedProperties["ClearButton_VerticalContentAlignment"]);
                part_ClearButton.IsTabStop.Should().Be((bool)expectedProperties["ClearButton_IsTabStop"]);
                
                BrushComparer.Equal(part_ClearButton.BorderBrush, (Brush)expectedProperties["ClearButton_BorderBrush"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ClearButton.BorderBrush, (Brush)expectedProperties["ClearButton_BorderBrush"]))
                {
                    Console.WriteLine("part_ClearButton.BorderBrush does not match expected value");
                    BrushComparer.LogBrushDifference(part_ClearButton.BorderBrush, (Brush)expectedProperties["ClearButton_BorderBrush"]);
                }
                
                BrushComparer.Equal(part_ClearButton.Background, (Brush)expectedProperties["ClearButton_Background"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ClearButton.Background, (Brush)expectedProperties["ClearButton_Background"]))
                {
                    Console.WriteLine("part_ClearButton.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_ClearButton.Background, (Brush)expectedProperties["ClearButton_Background"]);
                }

                BrushComparer.Equal(part_ClearButton.Foreground, (Brush)expectedProperties["ClearButton_Foreground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ClearButton.Foreground, (Brush)expectedProperties["ClearButton_Foreground"]))
                {
                    Console.WriteLine("part_ClearButton.Foreground does not match expected value");
                    BrushComparer.LogBrushDifference(part_ClearButton.Foreground, (Brush)expectedProperties["ClearButton_Foreground"]);
                }
            }
        }

        #endregion


        private void SetupTestButton()
        {
            TestBoxButton = new TextBox() { Text = "Hello" };
            AddControlToView(TestWindow, TestBoxButton);
        }

        private ScrollViewer FindScrollViewer(DependencyObject parent)
        {
            if (parent is ScrollViewer)
            {
                return (ScrollViewer)parent;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var scrollViewer = FindScrollViewer(child);
                if (scrollViewer != null)
                    return scrollViewer;
            }

            return null;
        }

        private TextBox TestBoxButton { get; set; }
        private Dictionary<ColorMode, TextBox> TestBoxButtons { get; set; } = new Dictionary<ColorMode, TextBox>();
        private ITestOutputHelper _outputHelper;

        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/TextboxTests.xaml";

    }
}
