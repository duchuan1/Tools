using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace WebApiServer
{
    public class TranslationDatabase
{
    private static Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "en", new Dictionary<string, string>
            {
                { "orders", "orders" },
                { "list", "list" }
            }
        },
        {
            "de", new Dictionary<string, string>
            {
                { "bestellungen", "orders" },
                { "liste", "list" }
            }
        },
        {
            "pl", new Dictionary<string, string>
            {
                { "zamowienia", "order" },
                { "lista", "list" }
            }
        },
    };

    public async Task<string> Resolve(string lang, string value)
    {
        var normalizedLang = lang.ToLowerInvariant();
        var normalizedValue = value.ToLowerInvariant();
        if (Translations.ContainsKey(normalizedLang) && Translations[normalizedLang].ContainsKey(normalizedValue))
        {
            return Translations[normalizedLang][normalizedValue];
        }

        return null;
    }
}
    public class DynamicTransformer : DynamicRouteValueTransformer
    {
        public IConfiguration Configuration { get; }
        private static string postUrl = string.Empty;
        private static string getUrl = string.Empty;
        public DynamicTransformer()
        {
            if (string.IsNullOrWhiteSpace(postUrl))
            {
                if (WebApiServer.Controllers.TestController.dicRcMsg.ContainsKey("GetUriName"))
                {
                    getUrl = WebApiServer.Controllers.TestController.dicRcMsg["GetUriName"].Split('/').Last();
                }
                if (WebApiServer.Controllers.TestController.dicRcMsg.ContainsKey("PostUriName"))
                {
                    postUrl = WebApiServer.Controllers.TestController.dicRcMsg["PostUriName"].Split('/').Last();
                }
            }
        }
        public override  ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("action")) return new ValueTask<RouteValueDictionary>(values);

            if (values["action"].ToString() == getUrl)
            {
                values["controller"] = "test";
                values["action"] = "Get";
            }
            else if (values["action"].ToString() == postUrl)
            {
                values["controller"] = "test";
                values["action"] = "Post";
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}
