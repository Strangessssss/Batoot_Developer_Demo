using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Batoot_Developer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void ProjectListingView_OnLoaded(object sender, RoutedEventArgs e)
    {
        Height = 550;
        Width = 500;
        MaxHeight = 550;
        MinHeight = 550;
        Background = new SolidColorBrush(Colors.LightBlue);
    }
}