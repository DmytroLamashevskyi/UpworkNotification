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
        public MainWindow()
        {
            InitializeComponent();
        }
          

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            RssController rss = new RssController();
            var list = rss.Parse(SettingsController.Instance.RssUrl); 
            myNotifyIcon.ShowCustomBalloon(new MessageControle().ShowData(list.FirstOrDefault()), PopupAnimation.Slide, SettingsController.Instance.PanelTimeout); 
        }
    }
}
