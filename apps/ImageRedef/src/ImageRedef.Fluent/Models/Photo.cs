using System.IO;

namespace ImageRedef.Fluent.Models;

public partial class Photo : ObservableObject
{
    [ObservableProperty]
    private string _fileName;

    [ObservableProperty]
    private string _filePath;

    [ObservableProperty]
    private BitmapImage _imageSource;
    
    [ObservableProperty]
    private bool _isLoading;


    public Photo(string path)
    {
        FilePath = path;
        FileName = Path.GetFileName(path);
    }

    public static MediaType GetMediaType(string path)
    {
        string extension = Path.GetExtension(path).ToLower();

        return extension switch
        {
            ".jpeg" or ".jpg" => MediaType.Jpeg,
            ".png" => MediaType.Png,
            _ => MediaType.Unknown,
        };
    }

}

public enum MediaType
{
    Unknown,
    Jpeg,
    Png
}