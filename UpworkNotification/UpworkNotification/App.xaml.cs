using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UpworkNotification.Controllers;

namespace UpworkNotification
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnExit(object sender, ExitEventArgs e)
        { 
            SettingsController.Instance.WriteSettings();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        { 
            SettingsController.Instance.ReadSettings();
        }
    }
}
