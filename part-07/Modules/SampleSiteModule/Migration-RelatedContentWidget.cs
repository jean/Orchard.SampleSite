using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule
{
	[OrchardFeature("RelatedContent")]
	public class MigrationsRelatedContentWidget : DataMigrationImpl
	{
		public int Create()
		{
			// Define the persistence table as a content part record with
			// my specific fields.
			SchemaBuilder.CreateTable("RelatedContentWidgetRecord", 
				table => table
					.ContentPartRecord()
                    .Column("TagList", DbType.String, a => a.Unlimited())
                    .Column("MaxItems", DbType.Int32));

			// Tell the content def manager that our widget is attachable
			ContentDefinitionManager.AlterPartDefinition(typeof(RelatedContentWidgetPart).Name,
				builder => builder.Attachable());

			// Tell the content def manager that we have a content type 
			// the parts it contains and that it should be treated as a widget
			ContentDefinitionManager.AlterTypeDefinition("RelatedContentWidget",
				cfg => cfg
					.WithPart("RelatedContentWidgetPart")
					.WithPart("WidgetPart")
					.WithPart("CommonPart")
					.WithSetting("Stereotype", "Widget"));

			return 1;
		}
	}
}