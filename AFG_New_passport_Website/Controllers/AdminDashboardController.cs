    using Microsoft.AspNetCore.Mvc;

    namespace AFG_New_passport_Website.Controllers
    {
        public class AdminDashboardController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult LoadNews()
            {
                return PartialView("_News");
            }

            public IActionResult LoadUsers()
            {
                return PartialView("_Users");
            }

            public IActionResult LoadAddNews()
            {
                return PartialView("_AddNews");
            }
        }

    }
