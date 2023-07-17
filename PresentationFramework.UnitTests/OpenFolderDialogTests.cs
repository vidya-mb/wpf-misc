using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationFramework.UnitTests
{
    public class OpenFolderDialogTests
    {
        //[WpfFact]
        //public void OpenFolderDialog_Ctor_Default()
        //{
        //    var dialog = new OpenFolderDialog();
        //    CheckOpenFolderDialogDefaults(dialog);
        //}

        //[WpfFact]
        //public void OpenFolderDialog_Reset_Invoke_Success()
        //{
        //    var dialog = new OpenFolderDialog
        //    {
        //        Multiselect = false,
        //        OverwritePrompt = false,
        //    };
        //    dialog.Reset();
        //    CheckOpenFolderDialogDefaults(dialog);
        //}

        //public static IEnumerable<object[]> ToString_TestData()
        //{
        //    yield return new object[] { new OpenFolderDialog(), "Microsoft.Win32.OpenFolderDialog: Title: , FolderName: " };
        //    yield return new object[] { new OpenFolderDialog { Title = "Title", FolderName = "FolderName" }, "Microsoft.Win32.OpenFolderDialog: Title: Title, FolderName: FolderName" };
        //}

        //[WpfTheory]
        //[MemberData(nameof(ToString_TestData))]
        //public void OpenFolderDialog_ToString_Invoke_ReturnsExpected(OpenFolderDialog dialog, string expected)
        //{
        //    Assert.Equal(expected, dialog.ToString());
        //}

        //[WpfTheory]
        //[InlineData(true, 0, 0)]
        //[InlineData(false, 0, 0)]
        //public void OpenFolderDialog_Multiselect_Set_GetReturnsExpected(bool value, int expectedOptions, int expectedOptionsAfter)
        //{
        //    var dialog = new OpenFolderDialog
        //    {
        //        Multiselect = value
        //    };
        //    Assert.Equal(value, dialog.Multiselect);

        //    dialog.Multiselect = value;
        //    Assert.Equal(value, dialog.Multiselect);

        //    dialog.Multiselect = !value;
        //    Assert.Equal(!value, dialog.Multiselect);
        //}

        //[WpfTheory]
        //[InlineData(null, new string[0])]
        //[InlineData("", new string[] { "" })]
        //[InlineData("folder", new string[] { "folder" })]
        //public void OpenFolderDialog_FolderName_Set_GetReturnsExpected(string value, string[] expectedFolderNames)
        //{
        //    var dialog = new OpenFileDialog
        //    {
        //        FolderName = value
        //    };

        //    Assert.Equal(value ?? string.Empty, dialog.FolderName);
        //    Assert.Equal(expectedFolderNames, dialog.FolderNames);

        //    if (expectedFolderNames.Length > 0)
        //    {
        //        Assert.Equal(dialog.FolderNames, dialog.FolderNames);
        //        Assert.NotSame(dialog.FolderNames, dialog.FolderNames);
        //    }
        //    else
        //    {
        //        Assert.Same(dialog.FolderNames, dialog.FolderNames);
        //    }
        //}

        //public static IEnumerable<object[]> SafeFolderName_Get_TestData()
        //{
        //    yield return new object[] { null, "", new string[0] };
        //    yield return new object[] { "", "", new string[] { "" } };
        //    yield return new object[] { "folder", "folder", new string[] { "folder" } };
        //    yield return new object[] { "C:\\Windows\\Fonts", "Fonts", new string[] { "Fonts" } };
        //}

        //[WpfTheory]
        //[MemberData(nameof(SafeFolderName_Get_TestData))]
        //public void OpenFolderDialog_SafeFolderName_GetReturnsExpected(string value, string expectedSafeFolderName, string[] expectedSafeFolderNames)
        //{
        //    var dialog = new OpenFileDialog
        //    {
        //        FolderName = value
        //    };

        //    Assert.Equal(value ?? string.Empty, dialog.FolderName);
        //    Assert.Equal(expectedSafeFolderName, dialog.SafeFolderName);

        //    if (expectedSafeFolderNames.Length > 0)
        //    {
        //        Assert.Equal(dialog.SafeFolderNames, dialog.SafeFolderNames);
        //        Assert.NotSame(dialog.SafeFolderNames, dialog.SafeFolderNames);
        //    }
        //    else
        //    {
        //        // Same or NotSame ? WinForms uses Same here ?
        //        // But on using Same the first test case fails.
        //        Assert.NotSame(dialog.SafeFolderNames, dialog.SafeFolderNames);
        //    }
        //}


        //[WpfFact]
        //public void OpenFolderDialog_OnFolderOk_Invoke_Success()
        //{
        //    throw new NotImplementedException();
        //}

        //private void CheckOpenFolderDialogDefaults(OpenFolderDialog dialog)
        //{
        //    Assert.False(dialog.Multiselect);
        //    Assert.Empty(dialog.FolderName);
        //    Assert.Empty(dialog.FolderNames);
        //    Assert.Same(dialog.FolderNames, dialog.FolderNames);

        //    // Check for options here
        //    //Assert.False(dialog.CreateTestFile);
        //}
    }
}
