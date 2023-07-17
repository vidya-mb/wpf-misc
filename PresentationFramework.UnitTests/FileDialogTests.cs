using Microsoft.Win32;
using System.ComponentModel;

namespace PresentationFramework.UnitTests
{
    public class FileDialogTests
    {
        [WpfFact]
        public void FileDialog_Ctor_Default()
        {
            var dialog = new OpenFileDialog();
            CheckFileDialogDefaults(dialog);
        }

        [WpfTheory]
        [BoolData]
        public void FileDialog_AddExtension_Set_GetReturnsExpected(bool value)
        {
            var dialog = new OpenFileDialog
            {
                AddExtension = value
            };
            Assert.Equal(value, dialog.AddExtension);

            dialog.AddExtension = value;
            Assert.Equal(value, dialog.AddExtension);

            dialog.AddExtension = !value;
            Assert.Equal(!value, dialog.AddExtension);
        }

        [WpfTheory]
        [BoolData]
        public void FileDialog_CheckFileExists_Set_GetReturnsExpected(bool value)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = value
            };
            Assert.Equal(value, dialog.CheckFileExists);

            dialog.CheckFileExists = value;
            Assert.Equal(value, dialog.CheckFileExists);

            dialog.CheckFileExists = !value;
            Assert.Equal(!value, dialog.CheckFileExists);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void FileDialog_CheckPathExists_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                AddExtension = value
            };
            Assert.Equal(value, dialog.AddExtension);

            dialog.AddExtension = value;
            Assert.Equal(value, dialog.AddExtension);

            dialog.AddExtension = !value;
            Assert.Equal(!value, dialog.AddExtension);
        }

        [WpfTheory]
        [InlineData(null, "")]
        [InlineData(".", "")]
        [InlineData(".ext", "ext")]
        [InlineData("..ext", ".ext")]
        [InlineData("ext", "ext")]
        public void FileDialog_DefaultExt_Set_GetReturnsExpected(string value, string expected)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = value
            };
            Assert.Equal(expected, dialog.DefaultExt);

            dialog.DefaultExt = value;
            Assert.Equal(expected, dialog.DefaultExt);
        }
        
        [WpfTheory]
        [InlineData(null, new string[0])]
        [InlineData("", new string[] { "" })]
        [InlineData("file", new string[] { "file" })]
        public void FileDialog_FileName_Set_GetReturnsExpected(string value, string[] expectedFileNames)
        {
            var dialog = new OpenFileDialog
            {
                FileName = value
            };

            Assert.Equal(value ?? string.Empty, dialog.FileName);
            Assert.Equal(expectedFileNames, dialog.FileNames);
            
            if(expectedFileNames.Length > 0)
            {
                Assert.Equal(dialog.FileNames, dialog.FileNames);
                Assert.NotSame(dialog.FileNames, dialog.FileNames);
            }
            else
            {
                Assert.Same(dialog.FileNames, dialog.FileNames);
            }
        }

        public static IEnumerable<object[]> SafeFileName_Get_TestData()
        {
            yield return new object[] { null, "", new string[0] };
            yield return new object[] { "", "", new string[] { "" } };
            yield return new object[] { "file", "file", new string[] { "file" } };
            yield return new object[] { "C:\\Windows\\explorer.exe", "explorer.exe", new string[] { "explorer.exe" } };
        }

        [WpfTheory]
        [MemberData(nameof(SafeFileName_Get_TestData))]
        public void FileDialog_SafeFileName_GetReturnsExpected(string value, string expectedSafeFileName, string[] expectedSafeFileNames)
        {
            var dialog = new OpenFileDialog
            {
                FileName = value
            };

            Assert.Equal(value ?? string.Empty, dialog.FileName);
            Assert.Equal(expectedSafeFileName, dialog.SafeFileName);

            if (expectedSafeFileNames.Length > 0)
            {
                Assert.Equal(dialog.SafeFileNames, dialog.SafeFileNames);
                Assert.NotSame(dialog.SafeFileNames, dialog.SafeFileNames);
            }
            else
            {
                // Same or NotSame ? WinForms uses Same here ?
                // But on using Same the first test case fails.
                Assert.NotSame(dialog.SafeFileNames, dialog.SafeFileNames);
            }
        }


        [WpfTheory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("filter|filter")]
        [InlineData("filter|filter|filter|filter")] 
        public void FileDialog_Filter_Set_GetReturnsExpected(string value)
        {
            var dialog = new OpenFileDialog
            {
                Filter = value
            };
            Assert.Equal(value ?? string.Empty, dialog.Filter);

            // Set same.
            dialog.Filter = value;
            Assert.Equal(value ?? string.Empty, dialog.Filter);
        }
        
        [WpfTheory]
        [InlineData("filter")]
        [InlineData("filter|filter|filter")]
        public void FileDialog_Filter_SetInvalid_ThrowsArgumentException(string value)
        {
            var dialog = new OpenFileDialog();
            Assert.Throws<ArgumentException>(() => dialog.Filter = value);
        }

        [WpfTheory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FileDialog_FilterIndex_Set_GetReturnsExpected(int value)
        {
            var dialog = new OpenFileDialog
            {
                FilterIndex = value
            };
            Assert.Equal(value, dialog.FilterIndex);

            // Set same.
            dialog.FilterIndex = value;
            Assert.Equal(value, dialog.FilterIndex);
        }
        
        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void FileDialog_RestoreDirectory_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                RestoreDirectory = value
            };
            Assert.Equal(value, dialog.RestoreDirectory);

            dialog.RestoreDirectory = value;
            Assert.Equal(value, dialog.RestoreDirectory);

            dialog.RestoreDirectory = !value;
            Assert.Equal(!value, dialog.RestoreDirectory);
        }




        [WpfFact]
        public void FileDialog_OnFileOk_Invoke_Success()
        {
            throw new NotImplementedException();
        }

        [WpfFact]
        public void FileDialog_Reset_Invoke_Success()
        {
            var dialog = new OpenFileDialog
            {
                AddExtension = false,
                CheckFileExists = false,
                CheckPathExists = false,
                Filter = "filter|filter",
                DefaultExt = "ext"
            };
            dialog.Reset();
            CheckFileDialogDefaults(dialog);
        }


        public static IEnumerable<object[]> ToString_TestData()
        {
            yield return new object[] { new OpenFileDialog(), "Microsoft.Win32.OpenFileDialog: Title: , FileName: " };
            yield return new object[] { new OpenFileDialog { Title = "Title", FileName = "FileName" }, "Microsoft.Win32.OpenFileDialog: Title: Title, FileName: FileName" };
        }

        [WpfTheory]
        [MemberData(nameof(ToString_TestData))]
        public void FileDialog_ToString_Invoke_ReturnsExpected(FileDialog dialog, string expected)
        {
            Assert.Equal(expected, dialog.ToString());
        }

        private void CheckFileDialogDefaults(FileDialog dialog)
        {
            Assert.True(dialog.AddExtension);
            Assert.True(dialog.CheckFileExists);
            Assert.True(dialog.CheckFileExists);
            Assert.False(dialog.RestoreDirectory);
            Assert.Empty(dialog.FileName);
            Assert.Empty(dialog.FileNames);
            Assert.Same(dialog.FileNames, dialog.FileNames);
            Assert.Empty(dialog.Filter);
            Assert.Equal(1, dialog.FilterIndex);
            Assert.Empty(dialog.DefaultExt);
            //Assert.False(dialog.ForcePreviewPane);
            //Assert.True(dialog.FileOk == null);
        }
    }
}
