using System.Windows;
using System.Windows.Input;
using TODO.Utils;
using TODO.ViewModel;

namespace TODO.View;

public partial class WindowControlView
{
    public bool IsMinimizeButtonVisible
    {
        get => (bool)GetValue(IsMinimizeButtonVisibleProperty);
        set => SetValue(IsMinimizeButtonVisibleProperty, value);
    }
    
    public bool IsMaximizeButtonVisible
    {
        get => (bool)GetValue(IsMaximizeButtonVisibleProperty);
        set => SetValue(IsMaximizeButtonVisibleProperty, value);
    }
    
    public bool IsCloseButtonVisible
    {
        get => (bool)GetValue(IsCloseButtonVisibleProperty);
        set => SetValue(IsCloseButtonVisibleProperty, value);
    }
    
    public bool IsExitButtonVisible
    {
        get => (bool)GetValue(IsExitButtonVisibleProperty);
        set => SetValue(IsExitButtonVisibleProperty, value);
    }

    public static readonly DependencyProperty IsMinimizeButtonVisibleProperty =
        RegisterVisibilityProperty(nameof(IsMinimizeButtonVisible));
    public static readonly DependencyProperty IsMaximizeButtonVisibleProperty =
        RegisterVisibilityProperty(nameof(IsMaximizeButtonVisible));
    public static readonly DependencyProperty IsCloseButtonVisibleProperty =
        RegisterVisibilityProperty(nameof(IsCloseButtonVisible));
    public static readonly DependencyProperty IsExitButtonVisibleProperty =
        RegisterVisibilityProperty(nameof(IsExitButtonVisible));

    public Visibility MinimizeButtonVisibility { get; private set; }
    public Visibility MaximizeButtonVisibility { get; private set; }
    public Visibility CloseButtonVisibility { get; private set; }
    public Visibility ExitButtonVisibility { get; private set; }

    public WindowControlView()
    {
        InitializeComponent();
        UpdateVisibilities();
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var window = Window.GetWindow(this);
        if (window == null) return;
        
        if (e.ClickCount == 2)
        {
            if (DataContext is WindowControlViewModel vm && Maximizable())
            {
                vm.MaximizeRestoreCommand.Execute(null);
            }
            return;
        }

        if (window.WindowState == WindowState.Maximized && e.ClickCount != 1 && Maximizable())
        {
            var mousePos = PointFromScreen(e.GetPosition(this));
            WindowInteropHelperExtensions.RestoreFromMaximized(window, mousePos);
        }

        WindowInteropHelperExtensions.DragMoveWithInterop(window);
    }

    private bool Maximizable()
    {
        return IsMaximizeButtonVisible;
    }

    private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WindowControlView control)
        {
            control.UpdateVisibilities();
        }
    }

    private void UpdateVisibilities()
    {
        MinimizeButtonVisibility = IsMinimizeButtonVisible.ToVisibility();
        MaximizeButtonVisibility = IsMaximizeButtonVisible.ToVisibility();
        CloseButtonVisibility = IsCloseButtonVisible.ToVisibility();
        ExitButtonVisibility = IsExitButtonVisible.ToVisibility();
    }

    private static DependencyProperty RegisterVisibilityProperty(string name, bool defaultValue = false) =>
        DependencyProperty.Register(name, typeof(bool), typeof(WindowControlView),
            new PropertyMetadata(defaultValue, OnVisibilityPropertyChanged));
}