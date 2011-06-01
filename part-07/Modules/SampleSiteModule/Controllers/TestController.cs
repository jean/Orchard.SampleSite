using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Tags.Models;
using Orchard.Themes;

namespace SampleSiteModule.Controllers
{
    public class TestController : Controller
    {
        private readonly IRepository<TagRecord> _tagRepository;
        private readonly IRepository<ContentTagRecord> _contentTagRepository;
        private readonly IContentManager _manager;
        private readonly IOrchardServices _orchard;

        public TestController(IContentManager manager, IRepository<TagRecord> tagRepository, IRepository<ContentTagRecord> contentTagRepository, IOrchardServices orchard)
        {
            _manager = manager;
            _tagRepository = tagRepository;
            _contentTagRepository = contentTagRepository;
            _orchard = orchard;
        }

        [Themed]
        public ActionResult Index()
        {
            

            /*
            _tagRepository.Get( t => tags.Contains(t.TagName
            // Get all content types that have tags associated
            var contentTypes = _manager.GetContentTypeDefinitions()
                .Where(x => x.Parts.Any(p => p.PartDefinition.Name == "TagsPart"))
                .Select(x => x.Name)
                .ToArray();

            var query = _manager.Query(VersionOptions.Published, contentTypes)
                .Join<CommonPartRecord>()
                .Join<TagsPartRecord>()
                .Join<ContentTagRecord>()
                .Where<TagsPartRecord>(t => t.Tags.Any(tp => tp.TagRecord.TagName == "featured"));
            
            query.OrderByDescending( t => t.

            var query = _contentManager.Query(VersionOptions.Published, contentTypes)
                .Join<CommonPartRecord>();

            switch (part.OrderBy)
            {
                case "Created": query = query.OrderByDescending(x => x.CreatedUtc);
                    break;
                case "Published": query = query.OrderByDescending(x => x.PublishedUtc);
                    break;
                case "Modified": query = query.OrderByDescending(x => x.ModifiedUtc);
                    break;
            }

            // build the Summary display for each content item
            var list = shapeHelper.List();
            list.AddRange(query.Slice(0, part.Count).Select(bp => _contentManager.BuildDisplay(bp, "Summary")));

            return ContentShape(shapeHelper.Parts_RecentContentWidget_List(ContentPart: part, ContentItems: list));

            IEnumerable<ContentItem> items = _manager.Query("page").Slice(5);
//            IEnumerable<ContentItem> items = _manager.Query().Where<TagsPartRecord>(r => r.Tags.Where(t => t.TagRecord.TagName == "featured").FirstOrDefault() != null).Slice(5);
                //.Contains("featured")).Slice(5);
             * */
            return View();
        }
    }
}