using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpworkNotification.Controllers;
using UpworkNotification.Models;

namespace UpworkNotification.Controles
{
    /// <summary>
    /// Interaction logic for MessageControle.xaml
    /// </summary>
    public partial class MessageControle : UserControl
    { 
        public MessageControle()
        {
            InitializeComponent();

        }
        public JobPost JobData { get; private set; }
        public MessageControle ShowData(JobPost data)
        {
            DataContext = this;
            JobData = data;
            Title.Text = data.Title;
            Summary.Text = data.Summary; 
            Rate.Text = string.IsNullOrWhiteSpace(data.HourlyRange)?data.Budget: data.HourlyRange;
            foreach (var skil in data.Skils)
                Skils.Items.Add(skil); 
            PostDate.Text = data.Date.ToString();


            var timer = new SmartDispatcherTimer();
            TimeOut.Maximum = SettingsController.Instance.PanelTimeout;
            timer.IsReentrant = false;
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.TickTask = async () =>
            { 
                TimeOut.Value += timer.Interval.TotalMilliseconds; 
                if(TimeOut.Value == TimeOut.Maximum)
                {
                    timer.Stop();
                }
            };
            timer.Start();

            return this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl(JobData.UrlId); 
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
