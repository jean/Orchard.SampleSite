using System;
using System.Collections.Generic;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Services
{
	[OrchardFeature("TwitterWidget")]
	public class CachedTwitterService : ITwitterService
	{
		public IList<Tweet> GetLatestTweetsFor(TwitterWidgetPart part)
		{
			List<Tweet> results = new List<Tweet>()
			{
				new Tweet{ DateStamp = DateTime.Now.AddSeconds(-10), Text = "Tweet number three" },
			    new Tweet{ DateStamp = DateTime.Now.AddMinutes(-10), Text = "Tweet number two" },
			    new Tweet{ DateStamp = DateTime.Now.AddDays(-5), Text = "Tweet number one" }
			   };

			return results;
		}
	}
}