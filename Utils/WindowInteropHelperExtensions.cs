using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TODO.Utils;

public static class WindowInteropHelperExtensions
{
    private const int NonClientLeftButtonDownMessage = 0xA1;
    private const int TitleBarHitTestCode = 0x2;
    private const int NoAdditionalData = 0;
    
    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    public static void DragMoveWithInterop(Window? window)
    {
        if (window == null) return;
        
        ReleaseCapture();
        SendMessage(new WindowInteropHelper(window).Handle,
            NonClientLeftButtonDownMessage,
            TitleBarHitTestCode,
            NoAdditionalData);
    }

    public static void RestoreFromMaximized(Window? window, Point mousePos)
    {
        if (window == null) return;

        window.WindowState = WindowState.Normal;
        window.Left = mousePos.X - (window.ActualWidth / 2);
        window.Top = mousePos.Y - 10;
    }
}