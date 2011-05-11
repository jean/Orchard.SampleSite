using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Services
{
	[OrchardFeature("TwitterWidget")]
	public class TwitterService : ITwitterService
	{
		const string FeedUrl = "http://api.twitter.com/1/statuses/user_timeline/{0}.rss";

		/// <summary>
		/// gets the latest tweets based on the configuration specified in the TwitterWidgetPart.
		/// Uses Linq2XML to load the feed url and select the tweets.
		/// </summary>
		/// <param name="twitterUserName">Name of the twitter user.</param>
		/// <param name="maxPosts">The max posts.</param>
		/// <returns></returns>
		public IList<Tweet> GetLatestTweetsFor(string twitterUserName, int maxPosts)
		{
			XDocument doc = XDocument.Load(String.Format(FeedUrl, twitterUserName));
			return (from item in doc.Descendants("item") 
						 select new Tweet
						 {
							 Text = TrimUserFrom((string) item.Element("title")),
							 DateStamp = (DateTime) item.Element("pubDate"),
							 Link = (string) item.Element("link")
						 })
						 .Take( maxPosts )
						 .ToList();
		}

		private string TrimUserFrom(string tweet)
		{
			int position = tweet.IndexOf(':');
			if (position == -1) return tweet;

			return tweet.Substring(position + 1);
		}
	}
}