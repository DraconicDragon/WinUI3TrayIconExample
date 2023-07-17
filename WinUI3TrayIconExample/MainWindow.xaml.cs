using Microsoft.UI.Xaml;
using WinRT.Interop;
using WinUI3TrayIconExample.TrayIcon;

namespace WinUI3TrayIconExample
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static TrayFlyoutWindow tray;
        public MainWindow()
        {
            this.InitializeComponent();
            tray = new ShellNotifyIcon();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
            //tray.newWindow.Activate();
            
        }
    }
}
