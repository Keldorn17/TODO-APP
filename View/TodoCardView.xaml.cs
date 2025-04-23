using System.Windows.Input;
using TODO.ViewModel;

namespace TODO.View;

public partial class TodoCardView
{
    public TodoCardView()
    {
        InitializeComponent();
    }
    
    private void OnPreviewMouseWheel(object? sender, MouseWheelEventArgs e)
    {
        if (DataContext is not TodoCardViewModel viewModel) return;
        viewModel.CycleCategory(e.Delta > 0);
        e.Handled = true;
    }
}