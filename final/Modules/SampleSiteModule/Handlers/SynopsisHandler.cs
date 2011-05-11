using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Handlers
{
	[OrchardFeature("Synopsis")]
	public class SynopsisHandler : ContentHandler
	{
		public SynopsisHandler(IRepository<SynopsisRecord> repository)
		{
			Filters.Add(StorageFilter.For(repository));
		}
	}
}