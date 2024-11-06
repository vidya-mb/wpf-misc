using ImageRedef.Fluent.Models;

namespace ImageRedef.Fluent.ViewModels
{
    public partial class PhotoEditorViewModel : ObservableObject
    {
        public PhotoEditorViewModel() { }

        public PhotoEditorViewModel(Photo photo)
        {
            Photo = photo;
            Title = photo.FileName;
            Metadata = new PhotoMetadata(Photo.FilePath);
        }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private Photo? _photo;

        [ObservableProperty]
        private PhotoMetadata? _metadata;

        [ObservableProperty]
        private int _scale = 100;

        [ObservableProperty]
        private ThemeMode _currentState = ThemeMode.System;

        partial void OnPhotoChanged(Photo? oldValue, Photo? newValue)
        {
            if(newValue is not null)
            {
                Title = newValue.FileName;
                Metadata = new PhotoMetadata(newValue.FilePath);
            }

        }

        [RelayCommand]
        public void Save()
        {

        }

        [RelayCommand]
        public void SaveAs()
        {

        }

        [RelayCommand]
        public void CopyToClipboard()
        {

        }

        [RelayCommand]
        public void Rotate()
        {

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
    }
}