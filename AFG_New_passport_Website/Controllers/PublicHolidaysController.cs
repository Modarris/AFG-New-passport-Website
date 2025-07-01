using System.Globalization;
using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace AFG_New_passport_Website.Controllers
{
    public class PublicHolidaysController : Controller
    {
        private readonly WebDbContext _context;

        public PublicHolidaysController(WebDbContext context)
        {
            _context = context;
        }
 
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var holidays = _context.PublicHolidays.OrderBy(h => h.HolidayDate).ToPagedList(page, pageSize);
            return View(holidays);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PublicHoliday model)
        {
            if (ModelState.IsValid)
            {
                _context.PublicHolidays.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "تعطیلی ثبت شد.";
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult ToggleActive(int id)
        {
            var holiday = _context.PublicHolidays.Find(id);
            if (holiday == null) return NotFound();

            holiday.IsActive = !holiday.IsActive;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var holiday = await _context.PublicHolidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }


        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublicHoliday holiday)
        {
            if (ModelState.IsValid)
            {
                _context.PublicHolidays.Update(holiday);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var holiday = _context.PublicHolidays.Find(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var holiday = _context.PublicHolidays.Find(id);
            if (holiday == null)
            {
                return NotFound();
            }

            _context.PublicHolidays.Remove(holiday);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var holiday = _context.Countries.Find(id);
            if (holiday == null)
                return NotFound();

            holiday.IsActive = !holiday.IsActive; // تغییر وضعیت
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
    }
