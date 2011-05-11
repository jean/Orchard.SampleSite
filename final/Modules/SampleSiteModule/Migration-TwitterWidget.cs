using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule
{
	[OrchardFeature("TwitterWidget")]
	public class Migrations : DataMigrationImpl
	{
		public int Create()
		{
			// ** Version one - create the twitter widget ** //

			// Define the persistence table as a content part record with
			// my specific fields.
			SchemaBuilder.CreateTable("TwitterWidgetRecord", 
				table => table
					.ContentPartRecord()
					.Column("TwitterUser", DbType.String)
					.Column("MaxPosts", DbType.Int32)
					.Column("CacheMinutes", DbType.Int32));

			// Tell the content def manager that our TwitterWidgetPart is attachable
			ContentDefinitionManager.AlterPartDefinition(typeof(TwitterWidgetPart).Name,
				builder => builder.Attachable());

			// Tell the content def manager that we have a content type called TwitterWidget
			// the parts it contains and that it should be treated as a widget
			ContentDefinitionManager.AlterTypeDefinition("TwitterWidget",
				cfg => cfg
					.WithPart("TwitterWidgetPart")
					.WithPart("WidgetPart")
					.WithPart("CommonPart")
					.WithSetting("Stereotype", "Widget"));

			return 1;
		}
	}
}