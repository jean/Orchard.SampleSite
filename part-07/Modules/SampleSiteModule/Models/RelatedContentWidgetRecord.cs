using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetRecord : ContentPartRecord
	{    
        public virtual string TagList { get; set; }
        public virtual int MaxItems { get; set; }
		public virtual bool ExcludeCurrentItemIfMatching { get; set; }
		public virtual bool MustHaveAllTags { get; set; }
		public virtual bool ShowListOnly { get; set; }
	}
}