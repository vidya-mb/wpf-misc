using ImageMagick;
using MetadataExtractor;
using System.IO;

namespace ImageRedef.Fluent.Models;

public partial class PhotoMetadata : ObservableObject
{
    [ObservableProperty]
    private string _fileName;

    [ObservableProperty]
    private string _fileDirectory;

    [ObservableProperty]
    private string _filePath;

    [ObservableProperty]
    private DateTime? _createdDate;

    [ObservableProperty]
    private DateTime? _modifiedDate;

    [ObservableProperty]
    private int _height;

    [ObservableProperty]
    private int _width;

    [ObservableProperty]
    private int _bitDepth = 8;

    [ObservableProperty]
    private int _numOfChannels = 3;

    [ObservableProperty]
    private ImageMagick.ColorSpace _colorSpace;

    [ObservableProperty]
    private ImageMagick.Density? _imageDensity;

    [ObservableProperty]
    private string? _densityString;

    public PhotoMetadata() { }

    public PhotoMetadata(string path)
    {
        FileInfo fileInfo = new FileInfo(path);
        if(!fileInfo.Exists)
        {
            throw new Exception($"File {path} does not exist");
        }

        _fileName = fileInfo.Name;
        _filePath = Path.GetFullPath(path);
        _fileDirectory = fileInfo.Directory?.FullName ?? "";
        
        FetchMetadata();
    }

    private void FetchMetadata()
    {
        ArgumentNullException.ThrowIfNullOrEmpty(FilePath);
        var info = new MagickImageInfo(FilePath);

        ColorSpace = info.ColorSpace;
        Height = (int)info.Height;
        Width = (int)info.Width;
        ImageDensity = info.Density;
        DensityString = info.Density?.ToString() ?? "N/A";

        using var image = new MagickImage(FilePath);

        CreatedDate = DateTime.Parse(image.GetAttribute("date:create") ?? "");
        ModifiedDate = DateTime.Parse(image.GetAttribute("date:modify") ?? "");

        var profile = image.GetExifProfile();
    }

}