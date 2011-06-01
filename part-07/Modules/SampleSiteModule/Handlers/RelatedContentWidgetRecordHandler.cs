using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Handlers
{
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetRecordHandler : ContentHandler
	{
		public RelatedContentWidgetRecordHandler(IRepository<RelatedContentWidgetRecord> repository)
		{
			Filters.Add(StorageFilter.For(repository));
		}
	}
}