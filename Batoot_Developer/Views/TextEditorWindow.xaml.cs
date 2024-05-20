using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using Batoot_Developer.ErrorDetector;
using Batoot_Developer.Messages;
using Batoot_Developer.Models;
using CommunityToolkit.Mvvm.Messaging;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Batoot_Developer.Views;

public partial class TextEditorWindow : Window
{
    public TextEditorWindow()
    {
        InitializeComponent();
        TextEditor.Options.ConvertTabsToSpaces = true;
        TextEditor.Options.WordWrapIndentation = 1;
        TextEditor.Options.CutCopyWholeLine = true;
        var highlighting = TextEditor.SyntaxHighlighting; 
        highlighting.GetNamedColor("StringInterpolation").Foreground = new SimpleHighlightingBrush(Colors.Peru);
        highlighting.GetNamedColor("Punctuation").Foreground = new SimpleHighlightingBrush(Colors.DarkGray);
        highlighting.GetNamedColor("NumberLiteral").Foreground = new SimpleHighlightingBrush(Colors.Pink);
        highlighting.GetNamedColor("Comment").Foreground = new SimpleHighlightingBrush(Colors.LightGreen);
        highlighting.GetNamedColor("MethodCall").Foreground = new SimpleHighlightingBrush(Colors.SpringGreen);
        highlighting.GetNamedColor("GetSetAddRemove").Foreground = new SimpleHighlightingBrush(Colors.CornflowerBlue);
        highlighting.GetNamedColor("Visibility").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("ParameterModifiers").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("Modifiers").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("String").Foreground = new SimpleHighlightingBrush(Colors.Peru);
        highlighting.GetNamedColor("Char").Foreground = new SimpleHighlightingBrush(Colors.Peru);
        highlighting.GetNamedColor("Preprocessor").Foreground = new SimpleHighlightingBrush(Colors.DarkGray);
        highlighting.GetNamedColor("TrueFalse").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("Keywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("ValueTypeKeywords").Foreground = new SimpleHighlightingBrush(Colors.CornflowerBlue);
        highlighting.GetNamedColor("SemanticKeywords").Foreground = new SimpleHighlightingBrush(Colors.CornflowerBlue);
        highlighting.GetNamedColor("NamespaceKeywords").Foreground = new SimpleHighlightingBrush(Colors.CornflowerBlue);
        highlighting.GetNamedColor("ReferenceTypeKeywords").Foreground = new SimpleHighlightingBrush(Colors.CornflowerBlue);
        highlighting.GetNamedColor("ThisOrBaseReference").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("NullOrValueKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue); 
        highlighting.GetNamedColor("GotoKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("ContextKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("ExceptionKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("CheckedKeyword").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("UnsafeKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);
        highlighting.GetNamedColor("OperatorKeywords").Foreground = new SimpleHighlightingBrush(Colors.DarkGray); 
        highlighting.GetNamedColor("SemanticKeywords").Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue);

        foreach (var color in highlighting.NamedHighlightingColors) {
            color.FontWeight = null;
        }
        TextEditor.SyntaxHighlighting = null;
        TextEditor.SyntaxHighlighting = highlighting;
    }

    private void TextEditor_OnLoaded(object sender, RoutedEventArgs e)
    {
        TextEditor.Document = new TextDocument();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var file = (FileDirectoryModel)((Button)sender).DataContext;
        WeakReferenceMessenger.Default.Send(new CurrentFileMessage(){Name = file.FileName, Path = file.FilePath});
    }

    private void TextEditorWindow_OnClosed(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage());
    }

    private void ButtonBase_OnClickCloseButton(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void Fullscreen(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void TextEditor_OnTextChanged(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new TextChanged());
    }
}