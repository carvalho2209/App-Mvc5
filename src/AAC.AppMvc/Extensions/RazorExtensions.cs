using System;
using System.Web;
using System.Web.Mvc;

namespace AAC.AppMvc.Extensions
{
    public static class RazorExtensions
    {
        public static bool AllowExibition(this WebViewPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(claimName, claimValue);
        }

        public static MvcHtmlString AllowExibition(this MvcHtmlString value, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(claimName, claimValue) ? value : MvcHtmlString.Empty;
        }

        public static string AllowWithExibition(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidateClaimsUser(claimName, claimValue) ? urlHelper.Action(actionName, controllerName, routeValues) : "";
        }

        public static string FormatDocument(this WebViewPage page, int typePerson, string document)
        {
            return typePerson == 1
                ? Convert.ToInt64(document).ToString(@"000\.00\.0000")
                : Convert.ToInt64(document).ToString(@"00\-0000000");
        }

        public static bool ShowInUrl(this WebViewPage value, Guid Id)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var urlTarget = urlHelper.Action("Edit", "Providers", new { id = Id });
            var urlTarget2 = urlHelper.Action("GetAddress", "Providers", new { id = Id });
            
            var urlInUse = HttpContext.Current.Request.Path;

            return urlTarget == urlInUse || urlTarget2 == urlInUse;
        }
    }
}