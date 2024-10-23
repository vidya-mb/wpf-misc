namespace ImageRedef.Fluent.Models;

public partial class PhotoMetadata : ObservableObject
{
    [ObservableProperty]
    private string _fileName;

    [ObservableProperty]
    private string _directory;

    [ObservableProperty]
    private string _fullPath;

    [ObservableProperty]
    private DateTime _createdDate;

    [ObservableProperty]
    private DateTime _modifiedDate;

    [ObservableProperty]
    private DateTime _lastWriteDate;

    [ObservableProperty]
    private int _height;

    [ObservableProperty]
    private int _width;

    [ObservableProperty]
    private int _bitDepth;
}