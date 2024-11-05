using System.IO;

namespace ImageRedef.Fluent.Models;

public partial class Photo : ObservableObject
{
    [ObservableProperty]
    private string _fileName;

    [ObservableProperty]
    private string _filePath;

    public Photo(string path)
    {
        FilePath = path;
        FileName = Path.GetFileName(path);
    }

    public static MediaType GetMediaType(string path)
    {
        string extension = Path.GetExtension(path).ToLower();

        switch(extension)
        {
            case ".jpeg":
            case ".jpg":
                return MediaType.Jpeg;
            case ".png":
                return MediaType.Png;
            default:
                return MediaType.Unknown;
        }
    }

}

public enum MediaType
{
    Unknown,
    Jpeg,
    Png
}