using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("TwitterWidget")]
	public class TwitterWidgetPart : ContentPart<TwitterWidgetRecord>
	{
		[Required]
		public string TwitterUserName
		{
			get { return Record.TwitterUser; }
			set { Record.TwitterUser = value; }
		}

		[Required]
		[DefaultValue(5)]
		public int MaxPosts
		{
			get { return Record.MaxPosts; }
			set { Record.MaxPosts = value; }
		}

		[Required]
		[DefaultValue(60)]
		public int CacheMinutes
		{
			get { return Record.CacheMinutes; }
			set { Record.CacheMinutes = value; }
		}
	}
}