## WPF Win11 Theme Gallery App

### Current Issues

#### Urgent
- [] Mica backdrop -> navigation -> backdrop off.

- [DONE] TextBlock colour on load is not as per theme 
    - For this uncomment line 12 in TextBlock.xaml in WPF.
    - However doing this creates issue for buttons ( Mismatch in icon and text colour )
- [DONE] Scrollbar over stackpanel for control pages
- [DONE] Listbox scrollbar above items
- [PARTIAL] Treeview no gap between elements

#### Important
- [] Missing CornerRadius in Image
- [] TextBox border linear gradient not applied
- [] TextBlock margin and padding both set only then work ?

### Work Items for the app

- [DONE] Dashboard, other control collection pages
- [DONE] Settings page styling
- [DONE] Settings at the bottom
- [DONE] Navigation back and forward buttons ?
- [DONE] Image change in image page
- [DONE] Search box functionality

- [DROP] Backdrop change from settings page

- [PARTIAL] Connect navigation with mainwindow
- [DROP] Use built-in WPF navigation

<<<<<<< HEAD
- [] Use built-in WPF navigation


### Suggestions 

Start with light theme. Then once on the page where you can switch, there we can show the switching of theme and SystemThemewatcher functionality.
[DONE] Basic Input Page Button Heading is coming black in dark theme. (Happening on every page)
[DONE] Button Page - WPF Ui button with modified content template - Doesn't show up something good. We can get rid of that.
[DONE] DLL name is showing up at the top below the heading, not required in sample app.(on every page)
Datagrid sorting and arrow marks absent. May be noticed. Unable to differentiate between header and normal rows.
[DROP] Media Canvas doesn't show up something meaningful, might need to make it better or just drop it.
[DROP] Not sure if we have done any changes for image, if there is nothing then showing in the sample app - maybe just drop. 
Menu Page - Colors and background black, unable to see the text.
Tab control page = tab boundaries not showing up properly in dark theme.
[DONE] Progress Bar = Only showing indeterminate, maybe we can show regular progress bar with some progress.
[DONE] TextBox = why are we mentioning WPFUI text box? Just WPF Text box should be fine.
[DONE] textblock = color is black on dark, not visible.
Password Box - not looking good. should be dropped for demo in my understanding.
High Contrast - datepicker control rendering red background. Datagrid red as well. Make sure we don't; show this in demo.
=======

### Suggestions 

[] Start with light theme. Then once on the page where you can switch, there we can show the switching of theme and SystemThemewatcher functionality.
[] DataGrid styling issues : Datagrid sorting and arrow marks absent. May be noticed. Unable to differentiate between header and normal rows.
[] Menu Page - Colors and background black, unable to see the text.
[] Tab control page = tab boundaries not showing up properly in dark theme.
[] Password Box - not looking good. Styling not applying.
[] XAML source code in last three buttons is incorrect (not accurate at many places)
[] High Contrast - datepicker control rendering red background. Datagrid red as well. Make sure we don't; show this in demo.

[DONE] Basic Input Page Button Heading is coming black in dark theme. (Happening on every page)
[DONE] Button Page - WPF Ui button with modified content template - Doesn't show up something good. We can get rid of that.
[DONE] DLL name is showing up at the top below the heading, not required in sample app.(on every page)
[DROP] Media Canvas doesn't show up something meaningful, might need to make it better or just drop it.
[DROP] Not sure if we have done any changes for image, if there is nothing then showing in the sample app - maybe just drop. 
[DONE] Progress Bar = Only showing indeterminate, maybe we can show regular progress bar with some progress.
[DONE] TextBox = why are we mentioning WPFUI text box? Just WPF Text box should be fine.
[DONE] textblock = color is black on dark, not visible.
[DONE] Search Control is not suggesting options
[DONE] On clicking any of the basic inputs only the button page opens up


### Suggestions - Light Theme

[DONE] (Suggestion) Can we add a slightly different background to the side menu so that it separates from the body and looks disconnected from it.
[] Datagrid Styling - DataGridCell/DataGridHeader Borders does not look consistent. (Could be by design)
>>>>>>> 48a55d625508c913f6e8bc71b24ba1af040ebf2b
