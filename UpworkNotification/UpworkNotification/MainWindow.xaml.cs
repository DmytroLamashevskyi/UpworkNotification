using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UpworkNotification.Controles;
using UpworkNotification.Controllers;

namespace UpworkNotification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly RssController rss = new RssController();
        public MainWindow()
        {
            InitializeComponent();

            var timer = new SmartDispatcherTimer();
            timer.IsReentrant = false;
            timer.Interval = TimeSpan.FromSeconds(SettingsController.Instance.UpdateTimer);
            timer.TickTask = async () =>
            {
                var list = rss.Parse(SettingsController.Instance.RssUrl);
                if (list == null) return;
                myNotifyIcon.ShowCustomBalloon(new MessageControle().ShowData(list.FirstOrDefault()), PopupAnimation.Slide, SettingsController.Instance.PanelTimeout);

            };
            timer.Start();
        }

        private async void UpdateRssData(object sender, EventArgs e)
        { 
            var list = rss.Parse(SettingsController.Instance.RssUrl);
            if (list == null) return;
            myNotifyIcon.ShowCustomBalloon(new MessageControle().ShowData(list.FirstOrDefault()), PopupAnimation.Slide, SettingsController.Instance.PanelTimeout);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            RssController rss = new RssController();
            var list = rss.Parse(SettingsController.Instance.RssUrl); 
            myNotifyIcon.ShowCustomBalloon(new MessageControle().ShowData(list.FirstOrDefault()), PopupAnimation.Slide, SettingsController.Instance.PanelTimeout); 
        }
    }
}
