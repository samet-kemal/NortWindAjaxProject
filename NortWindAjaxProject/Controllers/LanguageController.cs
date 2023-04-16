using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using NortWindAjaxProject.Services;
using System.Globalization;

namespace NortWindAjaxProject.Controllers
{

	public class LanguageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public IActionResult SetAppLanguage(CultureInfo culture)
        {
            var controller = (_httpContextAccessor.HttpContext.Request.Cookies["currentController"]);

            // httpContext.Response.Cookies.Append("currentUrl", "~/" + culture.ToString() + "/" + controller.ToString(),cookieOptions);
            var url="~/"+culture.ToString()+"/"+controller;

            return LocalRedirect(url);
        }


    }
}
