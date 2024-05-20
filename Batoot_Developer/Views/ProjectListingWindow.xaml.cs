using System.Windows;
using System.Windows.Input;

namespace Batoot_Developer.Views;

public partial class ProjectListingWindow : Window
{
    public ProjectListingWindow()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}