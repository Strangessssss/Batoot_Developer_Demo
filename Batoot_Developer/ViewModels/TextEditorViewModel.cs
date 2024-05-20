using System.Collections.ObjectModel;
using System.IO;
using Batoot_Developer.HelperClasses;
using Batoot_Developer.Messages;
using Batoot_Developer.Models;
using Batoot_Developer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICSharpCode.AvalonEdit.Document;

namespace Batoot_Developer.ViewModels;

[INotifyPropertyChanged]
public partial class TextEditorViewModel: BaseViewModel
{
    [ObservableProperty] private ObservableCollection<FileDirectoryModel>? _directories;
    [ObservableProperty] private ObservableCollection<FileDirectoryModel>? _openedFiles; 
    [ObservableProperty] private TextDocument? _text;
    private bool _newFileCreationWindowClosedIsOn;
    [ObservableProperty] private string? _errors = "Your errors will be here, please, don't be mistaken";
    private CurrentFileMessage? _currentFile;
    private NewFilesWindow? _newFilesWindow;
    
    
    public TextEditorViewModel()
    {
        WeakReferenceMessenger.Default.Register<ProjectOpenMessage>(this, ProjectOpen);
        WeakReferenceMessenger.Default.Register<CurrentFileMessage>(this, SetCurrentFile);
        WeakReferenceMessenger.Default.Register<NewFileCreationWindowClosedMessage>(this, (_, _) => _newFileCreationWindowClosedIsOn = false);
        WeakReferenceMessenger.Default.Register<FileMessage>(this, SaveFile);
    }
    
    private void SaveFile(object recipient, FileMessage message)
    {
        var path = $"{_currentFile?.Path?.Remove(_currentFile.Path.IndexOf(_currentFile.Path.Split("\\").Last(), StringComparison.Ordinal))}";
        if (_currentFile?.Path != null) File.Create(path + "\\" + message.FileName + ".cs").Close();
        _newFilesWindow?.Close();
        RefreshFiles(path);
    }

    private void SetCurrentFile(object recipient, CurrentFileMessage message)
    {
        if (message.Name == null || !message.Name.EndsWith(".cs")) return;
        _currentFile = message;
        OpenFile();
    }

    [RelayCommand]
    private void OpenFile()
    {
        if (_currentFile?.Path == null) return;
        if (Text != null)
            Text.Text = File.ReadAllText(_currentFile.Path);
    }
    
    [RelayCommand]
    private void NewFile()
    {
        if (_currentFile == null) return;
        if (_newFileCreationWindowClosedIsOn) return;
        _newFilesWindow = new NewFilesWindow();
        _newFilesWindow.Show();
        _newFileCreationWindowClosedIsOn = true;
    }
    
    [RelayCommand]
    private void Run()
    {
        SaveFile();
        if (_currentFile?.Path != null) DotNetCompileHelper.Run(_currentFile.Path);
    }
    
    [RelayCommand]
    private void Build()
    {
        SaveFile();
        if (_currentFile?.Path != null) DotNetCompileHelper.Build(_currentFile.Path);
    }
    
    [RelayCommand]
    private void SaveFile()
    {
        try
        {
            if (_currentFile?.Path != null) File.WriteAllText(_currentFile.Path, Text?.Text);
        }
        catch (NullReferenceException)
        {
            Errors = "Choose file to save!";
        }
    }
    
    private void ProjectOpen(object recipient, ProjectOpenMessage message)
    {
        RefreshFiles(message.Path);
        if (_currentFile != null) _currentFile.Path = message.Path + "\\Program.cs";
        OpenFile();
    }

    private void RefreshFiles(string? directory)
    {
        Directories = new();
        if (directory == null) return;
        foreach (var dir in Directory.GetDirectories(directory))
        {
            Directories.Add(new FileDirectoryModel(dir, true) { FileName = dir.Split("\\").Last() });
        }

        foreach (var dir in Directory.GetFiles(directory))
        {
            if (dir.EndsWith(".cs"))
                Directories.Add(new FileDirectoryModel(dir, false) { FileName = dir.Split("\\").Last() });
        }
        if (Directories.Any(x => x.FileName == "Program.cs"))
        {
            _currentFile = new CurrentFileMessage();
            _currentFile.Path = directory + "\\Program.cs";
            _currentFile.Name = "Program.cs";
        }
    }
}