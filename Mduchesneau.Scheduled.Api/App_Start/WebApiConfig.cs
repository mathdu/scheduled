using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Mduchesneau.Scheduled.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            foreach (string method in new[] { "GET", "POST", "PUT", "DELETE" })
                config.Routes.MapHttpRoute("Default" + method, "{controller}", new { action = method }, constraints: new { method = new HttpMethodConstraint(new HttpMethod(method)) });
        }
    }
}
