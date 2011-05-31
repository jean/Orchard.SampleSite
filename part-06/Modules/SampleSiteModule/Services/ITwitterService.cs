using System.Collections.Generic;
using Orchard;
using SampleSiteModule.Models;

namespace SampleSiteModule.Services
{
	public interface ITwitterService : IDependency
	{
		IList<Tweet> GetLatestTweetsFor(string twitterUserName, int maxPosts);
	}
}