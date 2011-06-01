using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using SampleSiteModule.Models;

namespace SampleSiteModule
{
    [OrchardFeature("Synopsis")]
    public class MigrationsSynopsis : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("SynopsisRecord",
                table => table
                    .ContentPartRecord()
                    .Column("SynopsisText", DbType.String, c => c.Unlimited())
                    .Column("ThumbnailUrl", DbType.String));

            // Tell the content def manager that our part is attachable
            ContentDefinitionManager.AlterPartDefinition(typeof(SynopsisPart).Name,
                builder => builder.Attachable());

            return 1;
        }
    }
}