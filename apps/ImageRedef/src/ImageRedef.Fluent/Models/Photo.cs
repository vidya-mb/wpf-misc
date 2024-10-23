using MetadataExtractor;
using System.IO;

namespace ImageRedef.Fluent.Models;

public class Photo
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public PhotoMetadata Metadata { get; set; }

    public Photo(string path)
    {
        FilePath = path;
        FileName = Path.GetFileName(path);
        Metadata = InitializeMetadata(path);
    }

    private PhotoMetadata InitializeMetadata(string path)
    {
        PhotoMetadata metadata = new PhotoMetadata();

        var directories = ImageMetadataReader.ReadMetadata(path);

        foreach (var directory in directories)
        {
            foreach (var tag in directory.Tags)
            {
                Debug.WriteLine($"{directory.Name} : {tag.Name} - {tag.Description}");
            }
        }
        return metadata;
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

    public static PhotoMetadata GetPhotoMetadata(string path)
    {
        return null;
    }
}

public enum MediaType
{
    Unknown,
    Jpeg,
    Png
}