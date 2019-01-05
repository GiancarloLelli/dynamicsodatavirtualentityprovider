using CRMDevLabs.VirtualEntity.Web.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System.Web.Http;

namespace CRMDevLabs.VirtualEntity.Web.Full
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // OData Model Configuration
            var builder = new ODataConventionModelBuilder();
            builder.Inspect();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // OData Route
            config.MapODataServiceRoute(
                routeName: "DefaultODataRoute",
                routePrefix: "OData",
                model: builder.GetEdmModel()
            );
        }
    }
}
