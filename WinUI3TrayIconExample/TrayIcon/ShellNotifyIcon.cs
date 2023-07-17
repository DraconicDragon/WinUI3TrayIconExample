using System;
using System.Runtime.InteropServices;
using static WinUI3TrayIconExample.TrayIcon.Win32Constants;
using static WinUI3TrayIconExample.TrayIcon.Win32DllImports;

namespace WinUI3TrayIconExample.TrayIcon
{
    partial class ShellNotifyIcon : TrayFlyoutWindow
    {
        private IntPtr windowHandle;
        private IntPtr notifyIconHandle;
        private NotifyIconData notifyIconData;
        private WndProcDelegate wndProcDelegate;
        private TrayFlyoutWindow trayWindow;

        public ShellNotifyIcon()
        {
            trayWindow = new();

            menuFlyout.Closed += MenuFlyout_Closed;
            //newWindow.Content.LostFocus += Content_LostFocus;


            wndProcDelegate = new WndProcDelegate(WndProc);
            windowHandle = CreateWindow();
            SetWndProc();
            notifyIconHandle = LoadIcon();
            notifyIconData = new NotifyIconData
            {
                cbSize = (uint)Marshal.SizeOf(typeof(NotifyIconData)),
                hWnd = windowHandle,
                uID = 1,
                uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP | NIF_SHOWTIP,
                uCallbackMessage = WM_APP + 1,
                hIcon = notifyIconHandle,
                szTip = "System Tray Icon"
            };
            Shell_NotifyIcon(NIM_ADD, ref notifyIconData);
            newWindow.Closed += (sender, args) => Dispose();
        }



        private void SetWndProc()
        {
            GCHandle.Alloc(wndProcDelegate);
            SetWindowLong(windowHandle, -4, Marshal.GetFunctionPointerForDelegate(wndProcDelegate));
        }

        // Correctly disposes off of the notifyIcon
        public void Dispose()
        {
            Shell_NotifyIcon(NIM_DELETE, ref notifyIconData);
            DestroyIcon(notifyIconHandle);
            DestroyWindow(windowHandle);
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
