using Orchard.UI.Resources;

namespace SampleSiteModule
{
    /// <summary>
    /// Defines common resources for this module.
    /// </summary>
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add().DefineStyle("Synopsis").SetUrl("synopsis.css");
        }
    }
}