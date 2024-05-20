using CommunityToolkit.Mvvm.ComponentModel;

namespace Batoot_Developer.Models
{
    public sealed partial class ProjectModel: ObservableObject
    {
        [ObservableProperty] private string? _name;
        [ObservableProperty] private string? _path;
    }
}