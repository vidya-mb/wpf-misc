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
        }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private Photo? _photo;

        [ObservableProperty]
        private int _scale = 100;


    }
}