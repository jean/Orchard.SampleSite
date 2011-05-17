using System.Web.Mvc;
using Orchard;
using Orchard.Mvc.Filters;

namespace SampleSite
{
	/// <summary>
	/// This filter just adds an IsHomepage flag to the Layout shape if the page
	/// being rendered is the homepage. We check this in the layout to determine
	/// whether to render the content area or not (this theme is used to just render
	/// widgets in zones on the homepage)
	/// </summary>
	public class HomepageLayoutFilter : FilterProvider, IResultFilter
	{
		private readonly IWorkContextAccessor _wca;

		public HomepageLayoutFilter(IWorkContextAccessor wca)
		{
			_wca = wca;
		}

		public void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var workContext = _wca.GetContext();
			var routeValues = filterContext.RouteData.Values;

			if (((string)routeValues["area"]) == "HomePage")
				workContext.Layout.IsHomepage = true;
		}

		public void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}
	}
}
