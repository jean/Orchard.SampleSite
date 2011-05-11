using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace SampleSiteModule
{
	public class MigrationsProductTypes : DataMigrationImpl
	{
		public int Create()
		{
			// Define the product list type which will
			// contain body details, common, route, menu and be a container
			ContentDefinitionManager.AlterTypeDefinition("ProductList",
				cfg => cfg
					.WithPart("BodyPart")
					.WithPart("CommonPart")
					.WithPart("RoutePart")
					.WithPart("MenuPart")
					.WithPart("ContainerPart")
					.Creatable());

			// Define the product type which will
			// contain body details, common, route, be containable and have
			// the reviews and image gallery parts.
			ContentDefinitionManager.AlterTypeDefinition("Product",
				cfg => cfg
					.WithPart("BodyPart")
					.WithPart("CommonPart")
					.WithPart("RoutePart")
					.WithPart("ContainablePart")
					.WithPart("ReviewsPart")
					.WithPart("ImageGalleryPart")
					.Creatable());

			return 1;
		}

		public int UpdateFrom1()
		{
			ContentDefinitionManager.AlterTypeDefinition("Product",
				cfg => cfg.WithPart("SynopsisPart"));

			return 2;
		}
	}
}