using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Handlers
{
	[OrchardFeature("TwitterWidget")]
	public class TwitterWidgetRecordHandler : ContentHandler
	{
		public TwitterWidgetRecordHandler(IRepository<TwitterWidgetRecord> repository)
		{
			Filters.Add(StorageFilter.For(repository));
		}
	}
}