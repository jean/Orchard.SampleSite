using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetPart : ContentPart<RelatedContentWidgetRecord>
	{
		public string TagList
		{
			get { return Record.TagList; }
			set { Record.TagList = value; }
		}

		[Required]
		[DefaultValue(5)]
		public int MaxItems
		{
			get { return Record.MaxItems; }
			set { Record.MaxItems = value; }
		}
	}
}