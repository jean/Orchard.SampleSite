using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace SampleSiteModule.Models
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