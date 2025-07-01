using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace AFG_New_passport_Website.Controllers
{
    public class CountriesController : Controller
    { 
        
        private readonly WebDbContext _context;
        public CountriesController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult testCalendar()
        {
            return View();
        }
        public IActionResult jalaali()
        {
            return View();
        }


        public async Task<IActionResult> Index(int page =1)
        {
            int pageSize = 3;
            var countries = _context.Countries.OrderBy(c => c.DName).ToPagedList(page, pageSize);
            return View(countries);
        }

        public IActionResult Create()
        {
            return View();
        }

        //
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DName,EName")] Country country)
        {
            if (!ModelState.IsValid) return View(country);

            _context.Add(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //ProvinceOfBirth edite view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }


        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Countries.Update(country);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null)
                return NotFound();

            country.IsActive = !country.IsActive; // تغییر وضعیت
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
