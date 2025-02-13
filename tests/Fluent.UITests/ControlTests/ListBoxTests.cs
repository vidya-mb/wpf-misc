using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xunit.Abstractions;

namespace Fluent.UITests.ControlTests
{
     public class ListBoxTests : BaseControlTests
    {
        public ListBoxTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper; 
            SetupTestListBox();
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void ListBox_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(TestListBox, rd);
        }

        
        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void ListBox_custom_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            SetCustomListbox();
            ResourceDictionary rd = GetTestDataDictionary(colorMode, "CustomListbox");
            VerifyControlProperties(TestListBox, rd);
        }




        #region Override Methods

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element);

            ScrollViewer? scrollViewer = element.Template.FindName("PART_ContentHost", element) as ScrollViewer;
            scrollViewer.Should().NotBeNull();

            templateParts.Add(scrollViewer);

            return templateParts;
        }

        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            ListBox? listbox = element as ListBox;

            if (listbox is null) return;

            List<FrameworkElement> parts = GetStyleParts(listbox);

            ListBox? part_ListBox = parts[0] as ListBox;
            ScrollViewer? part_ContentHostScrollViewer = parts[1] as ScrollViewer;

            using (new AssertionScope())
            {
                part_ListBox.Should().NotBeNull();

                BrushComparer.Equal(part_ListBox.Background, (Brush)expectedProperties["ListBoxBackground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ListBox.Background, (Brush)expectedProperties["ListBoxBackground"]))
                {
                    Console.WriteLine("part_ListBox.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_ListBox.Background, (Brush)expectedProperties["ListBoxBackground"]);
                }

                BrushComparer.Equal(part_ListBox.Foreground, (Brush)expectedProperties["TextFillColorPrimaryBrush"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ListBox.Foreground, (Brush)expectedProperties["TextFillColorPrimaryBrush"]))
                {
                    Console.WriteLine("part_ListBox.Foreground does not match expected value");
                    BrushComparer.LogBrushDifference(part_ListBox.Foreground, (Brush)expectedProperties["TextFillColorPrimaryBrush"]);
                }
                part_ListBox.BorderThickness.Should().Be((Thickness)expectedProperties["ListBoxBorderThemeThickness"]);
                part_ListBox.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ListBox_HorizontalAlignment"]);
                part_ListBox.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ListBox_VerticalAlignment"]);
                part_ListBox.HorizontalContentAlignment.Should().Be((HorizontalAlignment?)expectedProperties["HorizontalContentAlignment"]);
                part_ListBox.VerticalContentAlignment.Should().Be((VerticalAlignment?)expectedProperties["VerticalContentAlignment"]);

              
                part_ListBox.MinWidth.Should().Be((double)expectedProperties["ListBox_Minwidth"]);
                part_ListBox.MinHeight.Should().Be((double)expectedProperties["ListBox_MinHeight"]);
                part_ListBox.Padding.Should().Be(expectedProperties["ListBoxPadding"]);

                part_ContentHostScrollViewer.Should().NotBeNull();
               
                    part_ContentHostScrollViewer.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["PART_ContentHost_ListBox_VerticalAlignment"]);
                    ////Test fails as CanContentScroll returns true expected is false
                    part_ContentHostScrollViewer.CanContentScroll.Should().Be((bool)expectedProperties["PART_ContentHost_ListBox_CanContentScroll"]);

                    part_ContentHostScrollViewer.HorizontalScrollBarVisibility.Should().Be((ScrollBarVisibility)expectedProperties["PART_ContentHost_ListBox_HorizontalScrollBarVisibility"]);
                    part_ContentHostScrollViewer.VerticalScrollBarVisibility.Should().Be((ScrollBarVisibility)expectedProperties["PART_ContentHost_ListBox_VerticalScrollBarVisibility"]);
                ////ListboxItem properties
                //part_ListBox.Items.Should().Be((Thickness)expectedProperties["ListBoxItemPadding"]);
                ////part_ListBox.BorderThickness.Should().Be((Thickness)expectedProperties["ListBoxBorderThemeThickness"]);
                //part_ListBox.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ListBox_HorizontalAlignment"]);
                //part_ListBox.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ListBox_VerticalAlignment"]);
                //part_ListBox.HorizontalContentAlignment.Should().Be((HorizontalAlignment?)expectedProperties["HorizontalContentAlignment"]);
                //part_ListBox.VerticalContentAlignment.Should().Be((VerticalAlignment?)expectedProperties["VerticalContentAlignment"]);


            }

        }
        private void SetCustomListbox() 
        {
            TestListBox.MinHeight = 30;
            TestListBox.MinWidth = 100;
            TestListBox.Padding = new Thickness(4);
            TestListBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
            TestListBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            TestListBox.HorizontalAlignment = HorizontalAlignment.Left;
            TestListBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            TestListBox.VerticalContentAlignment = VerticalAlignment.Bottom;
            TestListBox.VerticalAlignment = VerticalAlignment.Top;
        }



        #endregion


        private void SetupTestListBox()
        {
            TestListBox = new ListBox()
            {
                Padding = new Thickness(10),
                BorderBrush = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(0),
            };
            AddControlToView(TestWindow, TestListBox);
        }

        private void SetupTestListBoxes(ColorMode mode)
        {
            TestListBoxes[mode] = new ListBox()
            {
                Padding = new Thickness(10),
                BorderBrush = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(0),
            };
            AddControlToView(TestWindows[mode], TestListBoxes[mode]);
        }

        //private ScrollViewer FindScrollViewer(DependencyObject parent)
        //{
        //    if (parent is ScrollViewer)
        //    {
        //        return (ScrollViewer)parent;
        //    }

        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        var scrollViewer = FindScrollViewer(child);
        //        if (scrollViewer != null)
        //            return scrollViewer;
        //    }

        //    return null;
        //}

        private ListBox TestListBox { get; set; }
        private Dictionary<ColorMode, ListBox> TestListBoxes { get; set; } = new Dictionary<ColorMode, ListBox>();

        private ITestOutputHelper _outputHelper;

        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/ListBoxTests.xaml";

    }
}
