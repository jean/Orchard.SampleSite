using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetPart : ContentPart<RelatedContentWidgetRecord>
	{
		[Required]
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

		[DefaultValue(true)]
		public bool ExcludeCurrentItemIfMatching
		{
			get { return Record.ExcludeCurrentItemIfMatching; }
			set { Record.ExcludeCurrentItemIfMatching = value; }
		}
		
		[DefaultValue(false)]
		public bool MustHaveAllTags 
		{ 
			get { return Record.MustHaveAllTags; }
			set { Record.MustHaveAllTags = value; }
		}

		[DefaultValue(true)]
		public bool ShowListOnly
		{
			get { return Record.ShowListOnly; }
			set { Record.ShowListOnly = value; }
		}
	}
}