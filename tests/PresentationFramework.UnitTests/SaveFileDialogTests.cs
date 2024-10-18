using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationFramework.UnitTests
{
    public class SaveFileDialogTests
    {
        [WpfFact]
        public void SaveFileDialog_Ctor_Default()
        {
            var dialog = new SaveFileDialog();
            CheckSaveFileDialogDefaults(dialog);
        }

        [WpfFact]
        public void SaveFileDialog_Reset_Invoke_Success()
        {
            var dialog = new SaveFileDialog
            {
                CreatePrompt = false,
                OverwritePrompt = false,
            };
            dialog.Reset();
            CheckSaveFileDialogDefaults(dialog);
        }

        public static IEnumerable<object[]> ToString_TestData()
        {
            yield return new object[] { new SaveFileDialog(), "Microsoft.Win32.SaveFileDialog: Title: , FileName: " };
            yield return new object[] { new SaveFileDialog { Title = "Title", FileName = "FileName" }, "Microsoft.Win32.SaveFileDialog: Title: Title, FileName: FileName" };
        }

        [WpfTheory]
        [MemberData(nameof(ToString_TestData))]
        public void SaveFileDialog_ToString_Invoke_ReturnsExpected(SaveFileDialog dialog, string expected)
        {
            Assert.Equal(expected, dialog.ToString());
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void SaveFileDialog_CreatePrompt_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new SaveFileDialog
            {
                CreatePrompt = value
            };
            Assert.Equal(value, dialog.CreatePrompt);

            dialog.CreatePrompt = value;
            Assert.Equal(value, dialog.CreatePrompt);

            dialog.CreatePrompt = !value;
            Assert.Equal(!value, dialog.CreatePrompt);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void SaveFileDialog_OverwritePrompt_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new SaveFileDialog
            {
                OverwritePrompt = value
            };
            Assert.Equal(value, dialog.OverwritePrompt);

            dialog.OverwritePrompt = value;
            Assert.Equal(value, dialog.OverwritePrompt);

            dialog.OverwritePrompt = !value;
            Assert.Equal(!value, dialog.OverwritePrompt);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void SaveFileDialog_CreateTestFile_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            throw new NotImplementedException();
            //var dialog = new SaveFileDialog
            //{
            //    CreateTestFile = value
            //};
            //Assert.Equal(value, dialog.CreateTestFile);

            //dialog.CreateTestFile = value;
            //Assert.Equal(value, dialog.CreateTestFile);

            //dialog.CreateTestFile = !value;
            //Assert.Equal(!value, dialog.CreateTestFile);
        }

        private void CheckSaveFileDialogDefaults(SaveFileDialog dialog)
        {
            Assert.False(dialog.CreatePrompt);
            Assert.True(dialog.OverwritePrompt);
            // Check for options here
            //Assert.False(dialog.CreateTestFile);
        }
    }
}

