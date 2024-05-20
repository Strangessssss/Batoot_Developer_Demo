using System.Windows;
using Batoot_Developer.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;

namespace Batoot_Developer.Views;

public partial class CreationNewProjectWindow : Window
{
    public CreationNewProjectWindow()
    {
        InitializeComponent();
        ResizeMode = ResizeMode.NoResize;
        WeakReferenceMessenger.Default.Register<OpenDialogueMessage>(this, OpenDialogue);
    }

    private void OpenDialogue(object recipient, OpenDialogueMessage message)
    {
        var dialog = new OpenFolderDialog();
        dialog.ShowDialog();
        WeakReferenceMessenger.Default.Send(dialog);
    }

    private void CreationNewProjectWindow_OnClosed(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage());
    }
}