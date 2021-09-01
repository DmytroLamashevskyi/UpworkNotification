using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;
using UpworkNotification.Models;

namespace UpworkNotification.Controllers
{
    public class RssController
    {  
        public int LastHash { private set; get; }
        public List<JobPost> Parse(string url)
        {
            var result = new List<JobPost>();
            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            if (CheckHash(LastHash, feed.GetHashCode()))
                LastHash = feed.GetHashCode();

            foreach (var item in feed.Items)
            {
                result.Add(StringParser(item));
            }

            return result.Count > 0 ? result : null;
        }


        private JobPost StringParser(SyndicationItem item)
        {
            var result = new JobPost();

            result.Title = item.Title.Text;
            result.Date = item.PublishDate.DateTime;
            result.UrlId = item.Id; 

            var Summary = item.Summary.Text;
            Summary = Summary.Replace("<br />", "\n");
            var arraySummary = Summary.Split("<b>");
            result.Summary = arraySummary[0];
            foreach(var str in arraySummary)
            {
                var data = str.Split("</b>:");
                switch (data[0])
                {
                    case "Category": 
                        // Desktop Software Development
                        break;
                    case "Hourly Range":
                        result.HourlyRange = data[1].Trim(); 
                        break;
                    case "Country": 
                        result.Country = data[1].Split("<a href")[0].Trim(); 
                        break;
                    case "Budget": 
                        result.Budget = data[1].Trim(); 
                        break;
                    case "Skills":
                        var skilsArray = data[1].Trim().Split(','); 
                        foreach(var skill in skilsArray.ToList())
                        {
                            result.Skils.Add(skill.Trim());
                        } 
                        break;
                }
            }

            return result;
        }

        private bool CheckHash(int first, int second)
        {
            return first != second;

        }
    }
}
