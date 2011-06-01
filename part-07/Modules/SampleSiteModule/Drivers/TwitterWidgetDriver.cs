using System;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Services;
using SampleSiteModule.Models;
using SampleSiteModule.Services;

namespace SampleSiteModule.Drivers
{
	/// <summary>
	/// Content part driver for the twitter widget part
	/// </summary>
	[OrchardFeature("TwitterWidget")]
	public class TwitterWidgetDriver : ContentPartDriver<TwitterWidgetPart>
	{
		private const string TwitterUserCacheKey = "TwitterWidgetPostsFor-{0}";
		private readonly ITwitterService _twitter;
		private readonly ICacheManager _cache;
		private readonly IClock _clock;

		public TwitterWidgetDriver(ITwitterService twitter, ICacheManager cache, IClock clock)
		{
			_twitter = twitter;
			_cache = cache;
			_clock = clock;
		}

		protected override DriverResult Display(TwitterWidgetPart part, string displayType, dynamic shapeHelper)
		{
			// Get tweets, from the cache or from twitter
			var tweets = _cache.Get(String.Format(TwitterUserCacheKey, part.TwitterUserName), ctx => 
				{
					ctx.Monitor( _clock.When( TimeSpan.FromMinutes(part.CacheMinutes)));
					return _twitter.GetLatestTweetsFor(part.TwitterUserName, part.MaxPosts);
				});

			return ContentShape("Parts_TwitterWidget",
				() => shapeHelper.Parts_TwitterWidget(
						TwitterUserName: part.TwitterUserName ?? String.Empty,
						Tweets: tweets));
		}

		protected override DriverResult Editor(TwitterWidgetPart part, dynamic shapeHelper)
		{
			return ContentShape("Parts_TwitterWidget_Edit",
				() => shapeHelper.EditorTemplate(
					TemplateName: "Parts/TwitterWidget",
					Model: part,
					Prefix: Prefix));
		}

		protected override DriverResult Editor(TwitterWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
		{
			updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}