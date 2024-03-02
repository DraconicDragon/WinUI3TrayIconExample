using Windows.UI.Text.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using WinRT.Interop;
using WinUI3TrayIconExample.TrayIcon;
using WinUIEx;

namespace WinUI3TrayIconExample
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TrayFlyoutWindow tray;
        public MainWindow()
        {
            this.InitializeComponent();
            new ShellNotifyIcon();
            tray = new TrayFlyoutWindow();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetTitleBar(null);
            this.ExtendsContentIntoTitleBar = true;
            myButton.Content = "Clicked";
            this.SystemBackdrop = new MicaBackdrop();
            //tray.newWindow.Activate();

        }
    }
}
