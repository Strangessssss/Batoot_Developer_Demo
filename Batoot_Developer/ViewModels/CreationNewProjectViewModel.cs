using Batoot_Developer.HelperClasses;
using Batoot_Developer.Messages;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;

namespace Batoot_Developer.ViewModels;

[INotifyPropertyChanged]
public partial class CreationNewProjectViewModel: BaseViewModel
{
    public CreationNewProjectViewModel()
    {
        WeakReferenceMessenger.Default.Register<ProjectsCountMessage>(this, GetCount);
        WeakReferenceMessenger.Default.Send(new ProjectsCountRequestMessage());
        WeakReferenceMessenger.Default.Register<OpenFolderDialog>(this, (_, message) => ProjectDirectory = message.FolderName == "" ? DefaultPath: message.FolderName);
    }
    
    [ObservableProperty] private string? _projectName;
    [ObservableProperty] private string _projectDirectory = DefaultPath;
    private static readonly string DefaultPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Batoot.Developer.Projects";
    
    private void GetCount(object recipient, ProjectsCountMessage message)
    {
        ProjectName = $"BatootProject{message.Count+1}";
    }

    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseCreationProjectWindowMessage());
    }
    
    [RelayCommand]
    private void SetDirectory()
    {
        WeakReferenceMessenger.Default.Send(new OpenDialogueMessage());
    }
    
    [RelayCommand]
    private void Create()
    {
        if (ProjectName == null || !DirectoryValidator.IsValidDirectoryName(ProjectName)) return;
        ImageManager.Draw(ProjectName);
        WeakReferenceMessenger.Default.Send(new ProjectMessage()
            { Name = ProjectName, Path = ProjectDirectory });
        Close();
    }
}