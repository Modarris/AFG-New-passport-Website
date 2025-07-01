using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string lang)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("Index", "Home");
        }
    }
}
