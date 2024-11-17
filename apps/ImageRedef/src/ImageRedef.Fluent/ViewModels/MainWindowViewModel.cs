using ImageRedef.Fluent.Helpers;
using Microsoft.Win32;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        NavigationList = PhotosDataSource.NavigationListFromConfig();
    }

    [ObservableProperty]
    private ThemeMode _currentState = ThemeMode.System;

    [ObservableProperty]
    private ICollection<NavigationItem> _navigationList;

    [RelayCommand]
    public void AddFolder()
    {
        OpenFolderDialog dialog = new OpenFolderDialog()
        {
            Title = "Select folders to display the photos...",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        };

        dialog.Multiselect = true;

        if(dialog.ShowDialog() == true)
        {
            foreach(string directory in dialog.FolderNames)
            {
                NavigationList.Add(PhotosDataSource.CreateNavigationItemFromPath(directory));
            }
        }
    }

    [RelayCommand]
    public void ToggleTheme()
    {
        if (CurrentState == ThemeMode.System)
        {
            CurrentState = ThemeMode.Light;
        }
        else if (CurrentState == ThemeMode.Light)
        {
            CurrentState = ThemeMode.Dark;
        }
        else
        {
            CurrentState = ThemeMode.System;
        }
    }

    partial void OnCurrentStateChanged(ThemeMode oldValue, ThemeMode newValue)
    {
        Application app = Application.Current;
        if(app is not null)
            app.ThemeMode = newValue;
    }
}