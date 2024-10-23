using System.IO;

namespace ImageRedef.Fluent.Models;

public class Photo
{
    public string FileName { get; set; }
    public string FilePath { get; set; }

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