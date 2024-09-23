using Avalonia.Controls;

namespace Calculadora.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var control = this.FindControl<TextBox>("Result");
        if (control is not null)
        {
            control.AttachedToVisualTree += (sender, args) => control.Focus();
        }
    }
}