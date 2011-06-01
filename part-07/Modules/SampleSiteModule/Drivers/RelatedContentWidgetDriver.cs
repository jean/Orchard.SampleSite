using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
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
        private readonly IRepository<TagRecord> _tagRepository;
        private readonly IRepository<ContentTagRecord> _contentTagRepository;
        private readonly IOrchardServices _orchard;

        public RelatedContentWidgetDriver(IRepository<TagRecord> tagRepository, IRepository<ContentTagRecord> contentTagRepository, IOrchardServices orchard)
        {
            _tagRepository = tagRepository;
            _contentTagRepository = contentTagRepository;
            _orchard = orchard;
        }

	    protected override DriverResult Display(RelatedContentWidgetPart part, string displayType, dynamic shapeHelper)
		{
            // Convert CSV tags to list
            List<string> tags = new List<string>();
            Array.ForEach( part.TagList.Split(','), t =>
            {
                if (!String.IsNullOrWhiteSpace(t))
                {
                    t = t.Trim();
                    if (!tags.Contains(t))
                        tags.Add(t);
                }
            });
            
            // Get all of the tags we're interested in
            IQueryable<TagRecord> query = _tagRepository.Table.Where(t => tags.Contains(t.TagName));
            IList<int> matches = query.Select(t => t.Id).ToList();

            // Get a normed list of content items with tags
            // TODO: We should probably get a distinct list of TagsPartRecord.Id instead and take the top x of that
            // TODO: and then load that from the content manager.
            // TODO: should order by date descending
            IEnumerable<ContentItem> items = _contentTagRepository.Fetch(x => matches.Contains(x.TagRecord.Id))
                .Select(t => _orchard.ContentManager.Get(t.TagsPartRecord.Id, VersionOptions.Published))
                .Where(c => c != null)
                .Distinct().Take(5);

            // Create a list and add our items in
            var list = shapeHelper.List();
            list.AddRange(items.Select( bp => _orchard.ContentManager.BuildDisplay(bp, "Summary")));

			return ContentShape("Parts_RelatedContentWidget",
				() => shapeHelper.Parts_RelatedContentWidget(
                        ContentItems : list
                        ));
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