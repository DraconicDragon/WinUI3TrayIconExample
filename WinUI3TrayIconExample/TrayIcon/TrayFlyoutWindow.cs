using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using WinRT.Interop;
using WinUIEx;
using static WinUI3TrayIconExample.TrayIcon.Win32Constants;
using static WinUI3TrayIconExample.TrayIcon.Win32DllImports;

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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        public IntPtr hWnd;
        public Window newWindow = new WindowEx()
        {
            IsAlwaysOnTop = true, IsResizable = false, 
        };
        public MenuFlyout menuFlyout;
        public ContentControl container;


        public TrayFlyoutWindow()
        {
            hWnd = WindowNative.GetWindowHandle(newWindow);
            newWindow.SetExtendedWindowStyle(ExtendedWindowStyle.Transparent);
            newWindow.SetWindowOpacity(0);
            newWindow.ExtendsContentIntoTitleBar = true;

            newWindow.Content.LostFocus += Content_LostFocus;
            
            OpenFlyout();
            menuFlyout.Closed += MenuFlyout_Closed;
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

            container = new() { Content = menuFlyout};
            newWindow.Content = container;
           
        }

        private void MenuFlyout_Closed(object sender, object e)
        {
            newWindow.AppWindow.Hide();
        }
        private void Content_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            newWindow.AppWindow.Hide();
        }
    }
}
