using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiArchitecture.Infrastructure;

namespace WebApiArchitecture
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new ApiResponseHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy
                    = IncludeErrorDetailPolicy.Always;
        }
    }
}

