## WPF Win11 Theme Gallery App

### Current Issues

#### Urgent
- [] Mica backdrop -> navigation -> backdrop off.

- [DONE] TextBlock colour on load is not as per theme
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

- [DROP] Backdrop change from settings page

- [] Search box functionality
- [] Connect navigation with mainwindow
- [] Canvas path change canvas page

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