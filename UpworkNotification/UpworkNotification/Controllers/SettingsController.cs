using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace UpworkNotification.Controllers
{
    public class SettingsController 
    {
        private static readonly Lazy<SettingsController> instance = new Lazy<SettingsController>();
        public static SettingsController Instance { get => instance.Value; }

        /// <summary>
        /// Disappear time in milliseconds
        /// </summary>
        public int PanelTimeout { set; get; }
        /// <summary>
        /// Rss Upwork Url
        /// </summary>
        public string RssUrl { set; get; }
        /// <summary>
        /// In secconds
        /// </summary>
        public int UpdateTimer { set; get; }

        private FileIniDataParser Parser { set; get; }
        private IniData Data { set; get; }
        public void ReadSettings()
        {
            Parser = new FileIniDataParser();
            Data = Parser.ReadFile("Settings.ini"); 
            this.PanelTimeout = Int32.Parse(Data["Settings"]["PanelTimeout"]);
            this.RssUrl = Data["Settings"]["RssUrl"];
            this.UpdateTimer = Int32.Parse(Data["Settings"]["UpdateTimer"]);
        }

        public void WriteSettings()
        {
            Data["Settings"]["PanelTimeout"] = PanelTimeout.ToString();
            Data["Settings"]["RssUrl"] = this.RssUrl;
            Data["Settings"]["UpdateTimer"] = this.UpdateTimer.ToString();


            Parser.WriteFile("Settings.ini", Data);
        }

    }
}
