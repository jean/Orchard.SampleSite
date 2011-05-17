using System;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using SampleSiteModule.Services;

namespace SampleSiteModule.Models
{
	[OrchardFeature("TwitterWidget")]
	public class TwitterWidgetDriver : ContentPartDriver<TwitterWidgetPart>
	{
		protected ITwitterService Twitter{ get; private set; }

		public TwitterWidgetDriver(ITwitterService twitter)
		{
			Twitter = twitter;
		}

		// GET
		protected override DriverResult Display(TwitterWidgetPart part, string displayType, dynamic shapeHelper)
		{
			return ContentShape("Parts_TwitterWidget",
				() => shapeHelper.Parts_TwitterWidget(
						TwitterUserName: part.TwitterUserName ?? String.Empty,
						Tweets: Twitter.GetLatestTweetsFor(part)));
		}

		// GET
		protected override DriverResult Editor(TwitterWidgetPart part, dynamic shapeHelper)
		{
			return ContentShape("Parts_TwitterWidget_Edit",
				() => shapeHelper.EditorTemplate(
					TemplateName: "Parts/TwitterWidget",
					Model: part,
					Prefix: Prefix));
		}

		// POST
		protected override DriverResult Editor(TwitterWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
		{
			updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}