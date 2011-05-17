using System.Collections.Generic;
using Orchard;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Services
{
	public interface ITwitterService : IDependency
	{
		IList<Tweet> GetLatestTweetsFor(TwitterWidgetPart part);
	}
}