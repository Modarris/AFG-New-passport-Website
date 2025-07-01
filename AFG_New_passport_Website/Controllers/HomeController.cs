using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System.Diagnostics;
using System.Globalization;

namespace AFG_New_passport_Website.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly WebDbContext _context;

        public HomeController(WebDbContext context)
        {
            _context = context;
        }
        /*    public IActionResult Index()
            {
                return View();
            }*/
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 16;

            var publishedNewsQuery = _context.NewsItems
                                             .Where(n => n.IsPublished)
                                             .OrderByDescending(n => n.PublishDate);

            var totalItems = await publishedNewsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var newsItems = await publishedNewsQuery
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            var model = new NewsIndexViewModel
            {
                NewsItems = newsItems,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Information()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Insertphoto()
        {
            return View();
        }
        public IActionResult DetailInfo
            ()
        {
            return View();
        }
       public IActionResult Guideline()
        {
            return View();
        }

    }
}