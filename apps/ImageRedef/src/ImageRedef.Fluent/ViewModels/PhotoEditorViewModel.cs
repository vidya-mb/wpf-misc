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

        partial void OnPhotoChanged(Photo? oldValue, Photo? newValue)
        {
            if(newValue is not null)
            {
                Title = newValue.FileName;
                Metadata = new PhotoMetadata(newValue.FilePath);
            }

        }
    }
}