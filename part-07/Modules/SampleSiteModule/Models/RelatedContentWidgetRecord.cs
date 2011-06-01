using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
{
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetRecord : ContentPartRecord
	{    
        public virtual string TagList { get; set; }
        public virtual int MaxItems { get; set; }
	}
}