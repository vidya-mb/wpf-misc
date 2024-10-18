using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace PresentationFramework.UnitTests
{
    public class CommonItemDialogTests
    {
        [WpfFact]
        public void CommonItemDialog_Ctor_Default()
        {
            var dialog = new OpenFileDialog();
            CheckDialogDefaults(dialog);
        }

        [WpfFact]
        public void CommonItemDialog_Reset_Invoke_Success()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open File",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = true,
                CheckFileExists = false,
            };
            dialog.Reset();
            CheckDialogDefaults(dialog);
        }

        private void CheckDialogDefaults(FileDialog dialog)
        {
            Assert.True(dialog.DereferenceLinks);
            Assert.True(dialog.ValidateNames);
            Assert.Same(dialog.CustomPlaces, dialog.CustomPlaces);
            Assert.Equal(dialog.InitialDirectory, string.Empty);
            Assert.Equal(dialog.Title, string.Empty);
            
            // Add checks for new items
        }

        public static IEnumerable<object[]> ToString_TestData()
        {
            yield return new object[] { new OpenFileDialog(), "Microsoft.Win32.OpenFileDialog: Title: , FileName: " };
            yield return new object[] { new OpenFileDialog { Title = "Title", FileName = "FileName" }, "Microsoft.Win32.OpenFileDialog: Title: Title, FileName: FileName" };
        }

        [WpfTheory]
        [MemberData(nameof(ToString_TestData))]
        public void CommonItemDialog_ToString_Invoke_ReturnsExpected(FileDialog dialog, string expected)
        {
            Assert.Equal(expected, dialog.ToString());
        }

        [WpfTheory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("1d5a0215-fa19-4e3b-8ab9-06da88c28ae7")]
        public void CommonItemDialog_ClientGuid_Set_GetReturnsExpected(Guid value)
        {
            // Not implemented for .NET 7
            Assert.NotEqual(value, value);
        }

        public static IEnumerable<object[]> InitialDirectory_TestData()
        {
            yield return new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) };
            yield return new object[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop) };
            yield return new object[] { Directory.GetCurrentDirectory() };
        }

        [WpfTheory]
        [MemberData(nameof(InitialDirectory_TestData))]
        public void CommonItemDialog_InitialDirectory_Set_GetReturnsExpected(string value)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = value
            };
            Assert.Equal(value ?? string.Empty, dialog.InitialDirectory);

            // Set same.
            dialog.InitialDirectory = value;
            Assert.Equal(value ?? string.Empty, dialog.InitialDirectory);
        }

        [WpfTheory]
        [StringWithNullData]
        public void CommonItemDialog_DefaultDirectory_Set_GetReturnsExpected(string value)
        {
            // Not implemented for .NET 7
            Assert.NotEqual(value, value);
        }

        [WpfTheory]
        [StringWithNullData]
        public void CommonItemDialog_RootDirectory_Set_GetReturnsExpected(string value)
        {
            // Not implemented for .NET 7
            Assert.NotEqual(value, value);
        }

        [WpfTheory]
        [StringWithNullData]
        public void CommonItemDialog_Title_Set_GetReturnsExpected(string value)
        {
            var dialog = new OpenFileDialog
            { 
                Title = value 
            };
            Assert.Equal(value ?? string.Empty, dialog.Title);

            // Set same.
            dialog.Title = value;
            Assert.Equal(value ?? string.Empty, dialog.Title);
        }

        [WpfTheory]
        [InlineData(true, 2052, 2308)]
        [InlineData(false, 2308, 2052)]
        public void CommonItemDialog_ValidateNames_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            var dialog = new OpenFileDialog
            {
                ValidateNames = value
            };
            Assert.Equal(value, dialog.ValidateNames);

            // Set same.
            dialog.ValidateNames = value;
            Assert.Equal(value, dialog.ValidateNames);

            // Set different.
            dialog.ValidateNames = !value;
            Assert.Equal(!value, dialog.ValidateNames);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void CommonItemDialog_ShowHiddenItems_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            // NOT IMPLEMENTED IN .NET 7
            Assert.NotEqual(value, value);
        }

        [WpfTheory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 0)]
        public void CommonItemDialog_AddToRecent_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        {
            // NOT IMPLEMENTED IN .NET 7
            Assert.NotEqual(value, value);
        }


        // Fix this
        public int GetOptions(FileDialog dialog)
        {
            // Use reflection to bring the flags value
            return 0;
        }
    }
}
