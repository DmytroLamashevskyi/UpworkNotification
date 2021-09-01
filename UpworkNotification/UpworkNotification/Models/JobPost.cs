using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpworkNotification.Models
{
    public class JobPost
    {
        public string UrlId { set; get; }
        public string Title { set; get; }
        
        public string Summary { set; get; }

        public DateTime Date { set; get; }

        public List<string> Skils { set; get; } = new List<string>();

        public string Country { set; get; }
        public string Budget { set; get; }

        public string HourlyRange { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is JobPost)
                return (obj as JobPost).UrlId == this.UrlId;


            return base.Equals(obj);
        }

    }
}
