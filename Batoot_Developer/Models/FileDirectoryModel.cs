using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Batoot_Developer.Models;

public partial class FileDirectoryModel : ObservableObject
{
    public FileDirectoryModel(string path, bool isDirectory)
    {
        IsDirectory = isDirectory;
        FilePath = path;
        if (!isDirectory) return;
        ChildNodes = new ObservableCollection<FileDirectoryModel>();
        foreach (var dir in Directory.GetDirectories(path))
        {
            _childNodes?.Add(new FileDirectoryModel(dir, true){ FileName = dir.Split("\\").Last()});
        }
        foreach (var dir in Directory.GetFiles(path))
        {
            _childNodes?.Add(new FileDirectoryModel(dir, false){ FileName = dir.Split("\\").Last()});
        }
    }
    
    [ObservableProperty] private string? _fileName;
    [ObservableProperty] private string? _filePath;
    [ObservableProperty] private ObservableCollection<FileDirectoryModel>? _childNodes;
    public bool IsDirectory { get; set; }
    
}