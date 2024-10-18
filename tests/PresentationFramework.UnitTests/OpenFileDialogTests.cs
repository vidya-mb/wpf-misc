using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Xunit;

namespace PresentationFramework.UnitTests
{
    public class OpenFileDialogTests
    {
        [WpfFact]
        public void OpenFileDialog_Ctor_Default()
        {
            var dialog = new OpenFileDialog();
            CheckOpenFileDialogDefaults(dialog);
        }

        [WpfFact]
        public void OpenFileDialog_Reset_Invoke_Success()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
            };
            dialog.Reset();
            CheckOpenFileDialogDefaults(dialog);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void OpenFileDialog_ShowReadOnly_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                ShowReadOnly = value
            };
            Assert.Equal(value, dialog.ShowReadOnly);

            dialog.ShowReadOnly = value;
            Assert.Equal(value, dialog.ShowReadOnly);

            dialog.ShowReadOnly = !value;
            Assert.Equal(!value, dialog.ShowReadOnly);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void OpenFileDialog_ReadOnlyChecked_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                ReadOnlyChecked = value
            };
            Assert.Equal(value, dialog.ReadOnlyChecked);

            dialog.ReadOnlyChecked = value;
            Assert.Equal(value, dialog.ReadOnlyChecked);

            dialog.ReadOnlyChecked = !value;
            Assert.Equal(!value, dialog.ReadOnlyChecked);
        }


        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void OpenFileDialog_Multiselect_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = value
            };
            Assert.Equal(value, dialog.Multiselect);

            dialog.Multiselect = value;
            Assert.Equal(value, dialog.Multiselect);

            dialog.Multiselect = !value;
            Assert.Equal(!value, dialog.Multiselect);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void OpenFileDialog_ForcePreviewPane_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            throw new NotImplementedException();
            //var dialog = new OpenFileDialog
            //{
            //    ForcePreviewPane = value
            //};
            //Assert.Equal(value, dialog.ForcePreviewPane);

            //dialog.ForcePreviewPane = value;
            //Assert.Equal(value, dialog.ForcePreviewPane);

            //dialog.ForcePreviewPane = !value;
            //Assert.Equal(!value, dialog.ForcePreviewPane);
        }

        public static IEnumerable<object[]> ToString_TestData()
        {
            yield return new object[] { new OpenFileDialog(), "Microsoft.Win32.OpenFileDialog: Title: , FileName: " };
            yield return new object[] { new OpenFileDialog { Title = "Title", FileName = "FileName" }, "Microsoft.Win32.OpenFileDialog: Title: Title, FileName: FileName" };
        }

        [WpfTheory]
        [MemberData(nameof(ToString_TestData))]
        public void OpenFileDialog_ToString_Invoke_ReturnsExpected(OpenFileDialog dialog, string expected)
        {
            Assert.Equal(expected, dialog.ToString());
        }

        private void CheckOpenFileDialogDefaults(OpenFileDialog dialog)
        {
            Assert.False(dialog.Multiselect);
            Assert.False(dialog.ShowReadOnly);
            Assert.False(dialog.ReadOnlyChecked);
            // Check for options here
            //Assert.False(dialog.ForcePreviewPane);
        }
    }
}
