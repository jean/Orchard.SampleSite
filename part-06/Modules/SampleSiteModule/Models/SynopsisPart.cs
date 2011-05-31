using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("Synopsis")]
	public class SynopsisPart : ContentPart<SynopsisRecord>
	{
		public string Synopsis
		{
			get { return Record.SynopsisText; }
			set { Record.SynopsisText = value; }
		}

		public string ThumbnailUrl
		{
			get { return Record.ThumbnailUrl; }
			set { Record.ThumbnailUrl = value; }
		}
	}
}