using System.Windows;

namespace TODO.Utils
{
    public static class WindowHelper
    {
        public static void CloseApp() => Application.Current.Shutdown();

        public static void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public static void MinimizeWindow(Window window)
        {
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        public static void MaximizeRestoreWindow(Window window)
        {
            if (window != null)
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        public static void DragWindow(Window window)
        {
            if (window != null)
            {
                window.DragMove();
            }
        }
    }
}
