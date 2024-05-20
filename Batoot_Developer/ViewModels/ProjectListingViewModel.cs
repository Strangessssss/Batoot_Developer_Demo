using System.Collections.ObjectModel;
using System.IO;
using Batoot_Developer.HelperClasses;
using Batoot_Developer.Messages;
using Batoot_Developer.Models;
using Batoot_Developer.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Batoot_Developer.ViewModels;
[INotifyPropertyChanged]
public partial class ProjectListingViewModel: BaseViewModel
{
    private CreationNewProjectWindow? _openCreationNewProjectWindow;
    private string _documentsPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Batoot.Developer.Projects";
    private bool _isOn;
    
    public ProjectListingViewModel()
    {
        if (!Directory.Exists(_documentsPath))
        {
            Directory.CreateDirectory(_documentsPath);
        }
        
        WeakReferenceMessenger.Default.Register<ProjectsCountRequestMessage>(this, SendCount);
        WeakReferenceMessenger.Default.Register<CloseCreationProjectWindowMessage>(this, (_, _) => _openCreationNewProjectWindow?.Close());
        WeakReferenceMessenger.Default.Register<ProjectMessage>(this, AddNewProject);
        WeakReferenceMessenger.Default.Register<CloseMessage>(this, (_, _) => _isOn = false);
        RefreshAllProjects();
    }
    
    [ObservableProperty] private string? _search;
    [ObservableProperty] private ObservableCollection<ProjectModel> _allProjects = [];
    [ObservableProperty] private ObservableCollection<ProjectModel> _showedProjects = [];

    private void AddNewProject(object recipient, ProjectMessage message)
    {
        var projectPath = message.Path + $"\\{message.Name}";
        Directory.CreateDirectory(projectPath);
        DotNetCompileHelper.NewConsole(projectPath);
        RefreshAllProjects();
        FilterProjects(Search);
        _isOn = !_isOn;
    }

    private void SendCount(object recipient, ProjectsCountRequestMessage message)
    {
        WeakReferenceMessenger.Default.Send(new ProjectsCountMessage() { Count = AllProjects.Count });
    }
    
    [RelayCommand]
    private void CreateNewProject()
    {
        if (_isOn) return;
       _openCreationNewProjectWindow = new CreationNewProjectWindow();
       _openCreationNewProjectWindow.Show();
       _isOn = !_isOn;
    }

    [RelayCommand]
    private void Run(ProjectModel projectModel)
    {
        if (_isOn) return;
        new TextEditorWindow().Show();
        WeakReferenceMessenger.Default.Send(new ProjectOpenMessage{ Path = projectModel.Path });
        _isOn = true;
    }
    partial void OnSearchChanged(string? value)
    {
        FilterProjects(value);
    }

    private void FilterProjects(string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            ShowedProjects = new ObservableCollection<ProjectModel>(AllProjects);
        else
        {
            var filtered = AllProjects.Where(p => p is { Name: not null, Path: not null } && (p.Name.Contains(searchText) || p.Path.Contains(searchText)));
            ShowedProjects = new ObservableCollection<ProjectModel>(filtered);
        }
    }

    private void RefreshAllProjects()
    {
        AllProjects = new ObservableCollection<ProjectModel>();
        foreach (var projectFolder in Directory.GetDirectories(_documentsPath))
        {
            var pathSepd = projectFolder.Split("\\");
            AllProjects.Add(new ProjectModel()
            {
                Name = pathSepd[^1],
                Path = string.Join("\\", pathSepd)
            });
        }
        ShowedProjects = new ObservableCollection<ProjectModel>(AllProjects);
    }
}