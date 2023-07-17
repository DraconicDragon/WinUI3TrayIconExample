using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WinUI3TrayIconExample.TrayIcon.Win32DllImports;
using static WinUI3TrayIconExample.TrayIcon.Win32Constants;
using Windows.Foundation;

namespace WinUI3TrayIconExample.TrayIcon
{
    partial class ShellNotifyIcon
    {
        private IntPtr CreateWindow()
        {
            var wndClass = new WndClassEx
            {
                cbSize = (uint)Marshal.SizeOf(typeof(WndClassEx)),
                style = 0,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProcDelegate),
                hInstance = Marshal.GetHINSTANCE(typeof(MainWindow).Module),
                hIcon = LoadIcon(),
                hCursor = LoadCursor(),
                lpszClassName = "SystemTrayIconWindowClass"
            };
            RegisterClassEx(ref wndClass);
            return CreateWindowEx(0, "SystemTrayIconWindowClass", "", 0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, wndClass.hInstance, IntPtr.Zero);
        }

        private IntPtr LoadIcon()
        {
            string iconPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icon.ico");
            return LoadImage(IntPtr.Zero, iconPath, 1, 0, 0, 0x00000010 | 0x00000020);
        }

        private IntPtr LoadCursor()
        {
            return Win32DllImports.LoadCursor(IntPtr.Zero, "#32512");
        }

        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == 0x8000 + 1) // WM_APP + 1
            {
                if (lParam.ToInt32() == WM_LBUTTONDOWN)
                {
                    Console.WriteLine("NotifyIcon Left Clicked");
                }
                else if (lParam.ToInt32() == WM_RBUTTONDOWN)
                {
                    Console.WriteLine("NotifyIcon Right Clicked");
                    // create new window
                    
                    //TODO: Some stupid focus stuff going on
                    //TODO: separate win32 code and the winui stuffs in the "nor´mal" c# aaaaaaaaaaaaaaaaaaa im tired
                    newWindow.Activate();
                    newWindow.AppWindow.MoveAndResize(new Windows.Graphics.RectInt32((int)GetCursorPosition().X, (int)GetCursorPosition().Y, 1, 1));
                    menuFlyout.ShowAt(container, new Point(0, 0));
                }
            }

            return DefWindowProc(hWnd, msg, wParam, lParam);
        }
    }
}
