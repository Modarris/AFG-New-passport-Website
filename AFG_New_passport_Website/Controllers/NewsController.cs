using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AFG_New_passport_Website.Controllers
{
    public class NewsController : Controller
    {
        private readonly WebDbContext _context;

        public NewsController(WebDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 9;

            // فقط اخبار پابلیش‌شده را می‌گیریم
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

        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(News model, IFormFile ImagePath)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }

                return View(model);
            }

            string fileName = null;
            if (ImagePath != null && ImagePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder); // احتیاط

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }
            }

            var news = new News
            {
                Title = model.Title,
                Content = model.Content,
                PublishDate = model.PublishDate,
                ImagePath = fileName ?? "default-image.png",
                IsPublished = false 
            };

            _context.NewsItems.Add(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "AdminAccount"); 
        }



        [HttpPost]
        public IActionResult Publish(int id)
        {
            var news = _context.NewsItems.FirstOrDefault(n => n.Id == id);
            if (news != null)
            {
                news.IsPublished = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home"); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var news = await _context.NewsItems.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

    
        // GET: News/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.NewsItems.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.NewsItems.FindAsync(id);
            if (news != null)
            {
              
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

            return RedirectToAction(nameof(Index));
        }

    }

}
