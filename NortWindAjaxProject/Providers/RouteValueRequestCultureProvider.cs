using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NortWindAjaxProject.Services
{
    public class RouteValueRequestCultureProvider : RequestCultureProvider
    {
        private CultureInfo defaultCulture;
        private CultureInfo defaultUICulture;


        public RouteValueRequestCultureProvider(List<CultureInfo> requestCulture)
        {
            this.defaultCulture = requestCulture[0];
            this.defaultUICulture = requestCulture[1];
        }

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            foreach (var cookie in httpContext.Request.Cookies.Keys)
            {
                httpContext.Response.Cookies.Delete(cookie);
            }

            //Parsing language from url path, which looks like "/en/home/index"
            PathString url = httpContext.Request.Path;

            // Test any culture in route
            if (url.ToString().Length <= 1)
            {
                return  Task.FromResult<ProviderCultureResult>(new ProviderCultureResult(this.defaultCulture.TwoLetterISOLanguageName, this.defaultUICulture.TwoLetterISOLanguageName));
            }

            var parts = httpContext.Request.Path.Value.Split('/');
            var culture = parts[1];

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            if (parts.Length >= 3)
            {
                var controller = parts[2];
                httpContext.Response.Cookies.Append("currentController", controller.ToString(), cookieOptions);

            }
            else
            {
                // httpContext.Response.Cookies.Append("currentUrl", "~/" + culture.ToString() + "/" + controller.ToString(),cookieOptions);
                httpContext.Response.Cookies.Append("currentController", "", cookieOptions);
            }

            // Test if the culture is properly formatted
            if (!Regex.IsMatch(culture, @"^[a-z]{2}(-[A-Z]{2})*$")) //=> tr-TR ,en-EN
            {

                return Task.FromResult<ProviderCultureResult>(new ProviderCultureResult(this.defaultCulture.TwoLetterISOLanguageName, this.defaultUICulture.TwoLetterISOLanguageName));

                 
            }

            return Task.FromResult<ProviderCultureResult>(new ProviderCultureResult(culture, culture));
        }
    }
}
