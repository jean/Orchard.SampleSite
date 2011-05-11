using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("Synopsis")]
	public class SynopsisRecord : ContentPartRecord
	{
		public virtual string SynopsisText { get; set; }
		public virtual string ThumbnailUrl { get; set; }
	}
}