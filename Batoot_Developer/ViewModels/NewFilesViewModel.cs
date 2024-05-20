using Batoot_Developer.HelperClasses;
using Batoot_Developer.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Batoot_Developer.ViewModels;

[INotifyPropertyChanged]
public partial class NewFilesViewModel: BaseViewModel
{
    [ObservableProperty] private string? _fileName;
    
    [RelayCommand]
    private void Create()
    {
        if (FileName == null || !DirectoryValidator.IsValidDirectoryName(FileName)) return;
        WeakReferenceMessenger.Default.Send(new FileMessage(){ FileName = FileName });
    }
}