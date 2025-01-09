using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xunit.Abstractions;

namespace Fluent.UITests.ControlTests
{
    public class CheckboxTests : BaseControlTests
    {
        private ITestOutputHelper _outputHelper;
        public CheckboxTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            SetupTestButton();
            TestCheckbox.Should().NotBeNull();
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void Checkbox_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(TestCheckbox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void Checkbox_Checked_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestCheckbox.IsChecked = true;
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Checked");
            VerifyControlProperties(TestCheckbox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void Checkbox_Indeterminate_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestCheckbox.IsChecked = null;           
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Indeterminate");
            VerifyControlProperties(TestCheckbox, rd);
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void Checkbox_IsEnabled_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestCheckbox.IsEnabled = false;
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "Disabled");
            VerifyControlProperties(TestCheckbox, rd);
        }
        #region Override Methods

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element);

            Border? rootBorder = element.Template.FindName("RootBorder", element) as Border;
            rootBorder.Should().NotBeNull();

            templateParts.Add(rootBorder);

            Border? border = element.Template.FindName("ControlBorderIconPresenter", element) as Border;
            border.Should().NotBeNull();

            templateParts.Add(border);

            Border? strokeBorder = element.Template.FindName("StrokeBorder", element) as Border;
            strokeBorder.Should().NotBeNull();

            templateParts.Add(strokeBorder);

            ContentPresenter? contentPresenter = element.Template.FindName("ContentPresenter", element) as ContentPresenter;
            contentPresenter.Should().NotBeNull();

            templateParts.Add(contentPresenter);

            TextBlock? txtBlock = element.Template.FindName("ControlIcon", element) as TextBlock;
            txtBlock.Should().NotBeNull();

            templateParts.Add(txtBlock);
            return templateParts;
        }
        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            CheckBox? checkbox = element as CheckBox;

            if (checkbox is null) return;

            List<FrameworkElement> parts = GetStyleParts(checkbox);

            CheckBox? part_checkbox = parts[0] as CheckBox;
            Border? part_RootBorder = parts[1] as Border;
            Border? part_ControlIconBorder = parts[2] as Border;
            Border? part_StrokeBorder = parts[3] as Border;
            ContentPresenter? part_ContentPresenter = parts[4] as ContentPresenter;
            TextBlock? part_TextBlock = parts[5] as TextBlock;

            using (new AssertionScope())
            {
                part_checkbox.Should().NotBeNull();
                BrushComparer.Equal(part_checkbox.Background, (Brush)expectedProperties["CheckBoxBackground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_checkbox.Background, (Brush)expectedProperties["CheckBoxBackground"]))
                {
                    Console.WriteLine("part_checkbox.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_checkbox.Background, (Brush)expectedProperties["CheckBoxBackground"]);
                }
                BrushComparer.Equal(part_checkbox.Foreground, (Brush)expectedProperties["CheckBoxForeground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_checkbox.Foreground, (Brush)expectedProperties["CheckBoxForeground"]))
                {
                    Console.WriteLine("part_checkbox.Foreground does not match expected value");
                    BrushComparer.LogBrushDifference(part_checkbox.Foreground, (Brush)expectedProperties["CheckBoxForeground"]);
                }
                BrushComparer.Equal(part_checkbox.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_checkbox.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush"]))
                {
                    Console.WriteLine("part_checkbox.BorderBrush does not match expected value");
                    BrushComparer.LogBrushDifference(part_checkbox.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush"]);
                }
                part_checkbox.BorderThickness.Should().Be((Thickness)expectedProperties["CheckBox_Thickness"]);
                part_checkbox.Padding.Should().Be(expectedProperties["CheckBox_Padding"]);
                part_checkbox.MinHeight.Should().Be((Double)expectedProperties["CheckBox_Height"]);
                part_checkbox.MinWidth.Should().Be((Double)expectedProperties["CheckBox_MinWidth"]);
                part_checkbox.FontWeight.Should().Be(expectedProperties["CheckBox_Fontweight"]);
                part_checkbox.Cursor.Should().Be(expectedProperties["CheckBox_Cursor"]);
                part_checkbox.IsTabStop.Should().Be((bool)expectedProperties["Checkbox_IsTabStop"]);
                //rootborder
                part_RootBorder.Should().NotBeNull();
                part_RootBorder.CornerRadius.Should().Be(expectedProperties["CheckBox_CornerRadius"]);
                //content icon border
                part_ControlIconBorder.Should().NotBeNull();
                BrushComparer.Equal(part_ControlIconBorder.Background, (Brush)expectedProperties["CheckBoxBackground_Fill"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ControlIconBorder.Background, (Brush)expectedProperties["CheckBoxBackground_Fill"]))
                {
                    Console.WriteLine("part_ControlIconBorder.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_ControlIconBorder.Background, (Brush)expectedProperties["CheckBoxBackground_Fill"]);
                }
                part_ControlIconBorder.Width.Should().Be((Double)expectedProperties["CheckBoxSize"]);
                part_ControlIconBorder.Height.Should().Be((Double)expectedProperties["CheckBoxSize"]);
                //stroke border
                part_StrokeBorder.Should().NotBeNull();
                BrushComparer.Equal(part_StrokeBorder.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush_Stroke"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_StrokeBorder.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush_Stroke"]))
                {
                    Console.WriteLine("part_checkbox.BorderBrush does not match expected value");
                    BrushComparer.LogBrushDifference(part_StrokeBorder.BorderBrush, (Brush)expectedProperties["CheckBoxBorderBrush_Stroke"]);
                }

                //content presenter
                part_ContentPresenter.Should().NotBeNull();

                part_ContentPresenter.RecognizesAccessKey.Should().Be((bool)expectedProperties["ContentPresenter_RecognizesAccessKey"]);
                part_ContentPresenter.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ContentPresenter_HorizontalAlignment"]);
                part_ContentPresenter.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ContentPresenter_VerticalAlignment"]);
                part_ContentPresenter.Margin.Should().Be((Thickness)expectedProperties["ContentPresenter_Margin"]);
               
                //Textblock
                part_TextBlock.Should().NotBeNull();
                part_TextBlock.FontSize.Should().Be((Double)expectedProperties["CheckBoxIconSize"]);
                part_TextBlock.Visibility.Should().Be((Visibility)expectedProperties["CheckboxTextblock_Visibility"]);
                part_TextBlock.Text.Should().Be((String)expectedProperties["CheckBoxTextblock_Text"]);
            }
        }
        #endregion
        #region Private Methods
        private void SetupTestButton()
        {
            TestCheckbox = new CheckBox() { Content = "TestCheckBox" };
            AddControlToView(TestWindow, TestCheckbox);
        }
        #endregion
        #region Private Properties

        private CheckBox TestCheckbox { get; set; }

        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/CheckboxTests.xaml";

        #endregion
    }
}
