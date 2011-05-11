using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace SampleSiteModule
{
	/// <summary>
	/// This is a route provider that allows our basket controller to respond
	/// to any request in the site on the path Basket/*
	/// </summary>
	public class BasketRouteProvider : IRouteProvider
	{
		public IEnumerable<RouteDescriptor> GetRoutes()
		{
			return new [] 
			{
			    new RouteDescriptor
			    {
			        Priority = 5,
			        Route = new Route( 
			            "Basket/{action}",
			            new RouteValueDictionary { { "area", "SampleSiteModule" }, {"controller", "Basket" }, { "action", "Index" }},
			            new RouteValueDictionary(),
			            new RouteValueDictionary{ {"area", "SampleSiteModule" }},
			            new MvcRouteHandler())

			    }
			};
		}

		public void GetRoutes(ICollection<RouteDescriptor> routes)
		{
			foreach (var route in GetRoutes())
				routes.Add(route);
		}
	}
}