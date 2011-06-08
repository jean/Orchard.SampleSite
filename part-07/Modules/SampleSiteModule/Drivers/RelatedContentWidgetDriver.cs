using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.Models;
using Orchard.Core.Routable.Models;
using Orchard.Environment.Extensions;
using Orchard.Tags.Models;
using SampleSiteModule.Models;

namespace SampleSiteModule.Drivers
{
	/// <summary>
	/// Content part driver for the related content widget
	/// </summary>
	[OrchardFeature("RelatedContent")]
	public class RelatedContentWidgetDriver : ContentPartDriver<RelatedContentWidgetPart>
	{
		private readonly IContentManager _cms;
		private readonly IWorkContextAccessor _work;

		public RelatedContentWidgetDriver(IContentManager cms, IWorkContextAccessor work)
        {
        	_cms = cms;
			_work = work;
        }

		protected override DriverResult Display(RelatedContentWidgetPart part, string displayType, dynamic shapeHelper)
		{
            // Convert CSV tags to list
            List<string> tags = new List<string>();
			if (!String.IsNullOrWhiteSpace(part.TagList))
			{
				Array.ForEach(part.TagList.Split(','), t =>
				{
					if (!String.IsNullOrWhiteSpace(t))
					{
						t = t.Trim();
						if (!tags.Contains(t))
							tags.Add(t);
					}
				});
			}

			// If we have no tags.....
			if (tags.Count < 1)
			{
				return ContentShape("Parts_RelatedContentWidget",
					() => shapeHelper.Parts_RelatedContentWidget(
							ContentItems: shapeHelper.List()
							));
			}

			// See if we can find the current page/content id to filter it out
			// from the related content if necessary.
			int currentItemId = -1;
			if( part.ExcludeCurrentItemIfMatching )
				currentItemId = TryGetCurrentContentId(-1);

			// Setup a query on the tags part
			IContentQuery<TagsPart, TagsPartRecord> query = _cms.Query<TagsPart, TagsPartRecord>();

			if (part.MustHaveAllTags)
			{
				// Add where conditions for every tag specified
				foreach (string tag in tags)
				{
					string tag1 = tag; // Prevent access to modified closure
					query.Where(tpr => tpr.Tags.Any(t => t.TagRecord.TagName == tag1));
				}
			}
			else
			{
				// Add where condition for any tag specified
				query.Where(tpr => tpr.Tags.Any(t => tags.Contains(t.TagRecord.TagName)));
			}
			
			// Finish the query (exclude current, do ordering and slice max items) and execute
			IEnumerable<TagsPart> parts = 
				query.Join<CommonPartRecord>()
				.Where(cpr => cpr.Id != currentItemId)
				.OrderByDescending(cpr => cpr.PublishedUtc)
				.Slice(part.MaxItems);

			// Create a list and push our display content items in
			var list = shapeHelper.List();
			list.AddRange(parts.Select(p => _cms.BuildDisplay(p, "Summary")));

			return ContentShape("Parts_RelatedContentWidget",
				() => shapeHelper.Parts_RelatedContentWidget(
						ShowListOnly : part.ShowListOnly,
                        ContentItems : list
                        ));
		}

		/// <summary>
		/// Helper that will attempt to work out the current content id from the url
		/// of the request.
		/// </summary>
		/// <param name="defaultIfNotFound"></param>
		/// <returns></returns>
		private int TryGetCurrentContentId(int defaultIfNotFound)
		{
			string urlPath = _work.GetContext().HttpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2);

			var routableHit = _cms
				.Query<RoutePart, RoutePartRecord>(VersionOptions.Published)
				.Where(r => r.Path == urlPath)
				.Slice(1).FirstOrDefault();

			if (routableHit != null) return routableHit.Id;

			return defaultIfNotFound;
		}

		protected override DriverResult Editor(RelatedContentWidgetPart part, dynamic shapeHelper)
		{
			return ContentShape("Parts_RelatedContentWidget_Edit",
				() => shapeHelper.EditorTemplate(
					TemplateName: "Parts/RelatedContentWidget",
					Model: part,
					Prefix: Prefix));
		}

		protected override DriverResult Editor(RelatedContentWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
		{
			updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}