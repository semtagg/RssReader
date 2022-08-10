using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssReader
{
    public class RssFeed
    {
        public string Url { get; set; }
        public int Interval { get; set; }

        public SyndicationFeed Feed
        {
            get
            {
                var reader = XmlReader.Create(Url);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();

                return feed;
            }
        }

        public RssFeed()
        {
            var configuration = Configuration.GetConfiguration();
            Url = configuration.FeedPath;
            Interval = 1000 * 60 * configuration.RefreshTimeInMinutes;
        }
    }
}
