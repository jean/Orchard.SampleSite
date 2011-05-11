using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("TwitterWidget")]
	public class TwitterWidgetRecord : ContentPartRecord
	{
		public virtual string TwitterUser { get; set; }
		public virtual int MaxPosts { get; set; }
		public virtual int CacheMinutes { get; set; }
	}
}