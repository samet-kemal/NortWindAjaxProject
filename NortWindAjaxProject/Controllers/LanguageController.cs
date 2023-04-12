using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace NortWindAjaxProject.Controllers
{
	public class LanguageController : Controller
	{
        [HttpPost]
        public IActionResult SetAppLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }



        /*
        public IActionResult ChangeLanguage(string culture)
        {
            if (culture!=null)
            {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
             );
                return Ok(culture);
            }
            else
            {
                return BadRequest();
            }

        }*/
    }
}
