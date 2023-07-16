using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace WinUI3TrayIconExample.TrayIcon
{
    public class TrayFlyoutWindow
    {
        #region constants
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const uint WS_CAPTION = 0x00C00000;
        private const uint WS_THICKFRAME = 0x00040000;
        private const uint WS_MINIMIZEBOX = 0x00020000;
        private const uint WS_MAXIMIZEBOX = 0x00010000;
        private const uint WS_SYSMENU = 0x00080000;
        private const uint WS_BORDER = 0x00800000;
        private const uint WS_DLGFRAME = 0x00400000;
        private const uint WS_EX_DLGMODALFRAME = 0x00000001;
        private const uint WS_EX_CLIENTEDGE = 0x00000200;
        private const uint WS_EX_STATICEDGE = 0x00020000;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOOWNERZORDER = 0x0200;
        #endregion
        #region DLLImports
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion

        private IntPtr hWnd;
        public Window newWindow;
        private MenuFlyout menuFlyout;
        private StackPanel stackPanel;

        public TrayFlyoutWindow()
        {
            newWindow = new Window();

            InitializeWindowContent();

            hWnd = WindowNative.GetWindowHandle(newWindow);
            SetWindowStyle();


            newWindow.Activate();
            OpenFlyout();
        }

        private void InitializeWindowContent()
        {






        }

        private void SetWindowStyle()
        {
            IntPtr style = GetWindowLong(hWnd, GWL_STYLE);
            style = (IntPtr)(style.ToInt64() & ~(WS_CAPTION | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU));
            SetWindowLong(hWnd, GWL_STYLE, style);

            // Remove extended styles
            IntPtr exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle = (IntPtr)(exStyle.ToInt64() & ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE));
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);

            // Redraw the window with the changed styles
            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);

            //TODO: Make window transparent

            // Set window to 1,1 pixel
            newWindow.AppWindow.Resize(new Windows.Graphics.SizeInt32(50, 50)); // 1,1
        }

        private void OpenFlyout()
        {
            menuFlyout = new();

            // Create the MenuFlyoutItems
            MenuFlyoutItem menuItem1 = new MenuFlyoutItem { Text = "Menu Item 1" };
            MenuFlyoutItem menuItem2 = new MenuFlyoutItem { Text = "Menu Item 2" };

            // Add the MenuFlyoutItems to the MenuFlyout
            menuFlyout.Items.Add(menuItem1);
            menuFlyout.Items.Add(menuItem2);

            ContentControl container = new() { Content = menuFlyout };
            newWindow.Content = container;
            menuFlyout.ShowAt(container);
        }
    }
}
