using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule.Drivers
{
	[OrchardFeature("Synopsis")]
	public class SynopsisDriver : ContentPartDriver<SynopsisPart>
	{
		protected override DriverResult Display(SynopsisPart part, string displayType, dynamic shapeHelper)
		{
			return ContentShape("Parts_Synopsis",
				() => shapeHelper.Parts_Synopsis(
						Synopsis : part.Synopsis,
						Thumbnail : part.ThumbnailUrl ));
		}

		protected override DriverResult Editor(SynopsisPart part, dynamic shapeHelper)
		{
			return ContentShape("Parts_Synopsis_Edit",
				() => shapeHelper.EditorTemplate(
					TemplateName: "Parts/Synopsis",
					Model: part,
					Prefix: Prefix));
		}

		protected override DriverResult Editor(SynopsisPart part, IUpdateModel updater, dynamic shapeHelper)
		{
			updater.TryUpdateModel(part, Prefix, null, null);
			return Editor(part, shapeHelper);
		}
	}
}