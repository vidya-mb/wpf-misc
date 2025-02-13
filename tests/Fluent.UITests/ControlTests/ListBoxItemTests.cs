using Fluent.UITests.TestUtilities;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xunit.Abstractions;

namespace Fluent.UITests.ControlTests
{
    public class ListBoxItemTest : BaseControlTests
    {

        public ListBoxItemTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            SetupTestListBoxItem();
        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void ListBoxItem_Initialization_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestWindow.Show();
            TestListBoxItem.VerticalAlignment = VerticalAlignment.Stretch;
            TestListBoxItem.VerticalContentAlignment = VerticalAlignment.Center;
            ResourceDictionary rd = GetTestDataDictionary(colorMode, "");
            VerifyControlProperties(TestListBoxItem, rd);
        }


        private void SetupTestListBoxItem()
        {
            TestListBoxItem = new ListBoxItem()
            {
                Padding = new Thickness(12),
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(10),
                //VerticalContentAlignment =VerticalAlignment.Top,

            };
            AddControlToView(TestWindow, TestListBoxItem);
        }

        public override List<FrameworkElement> GetStyleParts(Control element)
        {
            List<FrameworkElement> templateParts = new List<FrameworkElement>();
            templateParts.Add(element);

            Border? border = element.Template.FindName("Border", element) as Border;
            border.Should().NotBeNull();
            templateParts.Add(border);
            return templateParts;
        }

        public override void VerifyControlProperties(FrameworkElement element, ResourceDictionary expectedProperties)
        {
            ListBoxItem? listboxItems = element as ListBoxItem;

            if (listboxItems is null) return;

            List<FrameworkElement> parts = GetStyleParts(listboxItems);

            ListBoxItem? part_ListBoxItem  = parts[0] as ListBoxItem;
            Border? part_ContentBorder = parts[1] as Border;


            using (new AssertionScope())
            {
                part_ListBoxItem.Should().NotBeNull();

                BrushComparer.Equal(part_ListBoxItem.Background, (Brush)expectedProperties["ListBoxItemBackground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ListBoxItem.Background, (Brush)expectedProperties["ListBoxItemBackground"]))
                {
                    Console.WriteLine("part_ListBoxItem.Background does not match expected value");
                    BrushComparer.LogBrushDifference(part_ListBoxItem.Background, (Brush)expectedProperties["ListBoxItemBackground"]);
                }

                BrushComparer.Equal(part_ListBoxItem.Foreground, (Brush)expectedProperties["ListBoxItemForeground"]).Should().BeTrue();
                if (!BrushComparer.Equal(part_ListBoxItem.Foreground, (Brush)expectedProperties["ListBoxItemForeground"]))
                {
                    Console.WriteLine("part_ListBoxItem.Foreground does not match expected value");
                    BrushComparer.LogBrushDifference(part_ListBoxItem.Foreground, (Brush)expectedProperties["ListBoxItemForeground"]);
                }
                part_ListBoxItem.BorderThickness.Should().Be((Thickness)expectedProperties["ListBoxItemBorderThickness"]);
                part_ListBoxItem.HorizontalAlignment.Should().Be((HorizontalAlignment)expectedProperties["ListBoxIem_HorizontalAlignment"]);
                part_ListBoxItem.VerticalAlignment.Should().Be((VerticalAlignment)expectedProperties["ListBoxIem_VerticalAlignment"]);
                part_ListBoxItem.HorizontalContentAlignment.Should().Be((HorizontalAlignment?)expectedProperties["ListBoxIem_HorizontalContentAlignment"]);
                part_ListBoxItem.VerticalContentAlignment.Should().Be((VerticalAlignment?)expectedProperties["ListBoxIem_VerticalContentAlignment"]);
               

            }


        }

        [WpfTheory]
        [MemberData(nameof(ColorModes_TestData))]
        public void ListboxItem_IsselectedTrue_Test(ColorMode colorMode)
        {
            SetColorMode(TestWindow, colorMode);
            TestListBoxItem.IsSelected = true;
            TestListBoxItem.VerticalAlignment = VerticalAlignment.Stretch;
            TestListBoxItem.VerticalContentAlignment = VerticalAlignment.Center;
           
            TestWindow.Show();

            ResourceDictionary rd = GetTestDataDictionary(colorMode, "IsSelected");
            VerifyControlProperties(TestListBoxItem, rd);
        }
       
         
        private ListBoxItem TestListBoxItem { get; set; }
        private Dictionary<ColorMode, ListBoxItem> TestListBoxesItems { get; set; } = new Dictionary<ColorMode, ListBoxItem>();

        private ITestOutputHelper _outputHelper;

        protected override string TestDataDictionaryPath => @"/Fluent.UITests;component/ControlTests/Data/ListBoxItemTests.xaml";



    }
}
