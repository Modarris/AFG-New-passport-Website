using AFG_New_passport_Website.Data;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly WebDbContext _context;

        public AdminAccountController(WebDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

 
   /*     [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.AdminUsers.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return RedirectToAction("Dashboard", "AdminAccount");
            }

            ViewBag.Error = "نام کاربری یا رمز اشتباه است";
            return View();
        }*/



        [HttpPost]
        public async Task<IActionResult> Publish(int id)
        {
            var newsItem = await _context.NewsItems.FindAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            newsItem.IsPublished = true;
            newsItem.PublishDate = DateTime.Now; // زمان انتشار

            _context.Update(newsItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // برگرد به لیست اخبار ادمین
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.NewsItems.FindAsync(id);
            if (news != null)
            {
                // حذف عکس از فولدر (اختیاری)
                if (news.ImagePath != "default-image.png")
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news", news.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.NewsItems.Remove(news);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "AdminAccount");
        }






        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLoggedIn");
            return RedirectToAction("Login");
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        // نمایش لیست اخبار برای ادمین
        public IActionResult Index()
        {
            var news = _context.NewsItems.Where(n => !n.IsPublished).ToList(); // فقط اخبار منتشرنشده
            return View(news);
        }



    }

}
