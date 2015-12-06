using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DJO.PoolLeague
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            yield return new RouteDescriptor
            {
                Route = new Route("PoolLeague/NewGame",
                        new RouteValueDictionary {
                            {"area", "DJO.PoolLeague"},
                            {"controller", "PoolGame"},
                            {"action", "NewGame"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "DJO.PoolLeague"}
                        },
                        new MvcRouteHandler())
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

    }
}