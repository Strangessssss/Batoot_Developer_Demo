using System.Windows;
using Batoot_Developer.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace Batoot_Developer.Views;

public partial class NewFilesWindow : Window
{
    public NewFilesWindow()
    {
        InitializeComponent();
    }

    private void NewFiles_OnClosed(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new NewFileCreationWindowClosedMessage());
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}