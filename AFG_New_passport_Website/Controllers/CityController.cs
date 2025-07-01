using Microsoft.AspNetCore.Mvc;
using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AFG_New_passport_Website.Models;

namespace AFG_New_passport_Website.Controllers
{
    public class CityController : Controller
    {
        private readonly WebDbContext _context;

        public CityController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cities = _context.Cities.Include(c => c.Country).ToList();
            return View(cities);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleBirthStatus(int id)
        {
            var city = _context.Cities.Find(id);
            if (city != null)
            {
                city.IsBirthPlace = !city.IsBirthPlace;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleProcessStatus(int id)
        {
            var city = _context.Cities.Find(id);
            if (city != null)
            {
                city.IsDocumentProcessPlace = !city.IsDocumentProcessPlace;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CityViewModel
            {
                Countries = _context.Countries.ToList()
            };
            return View(model);
        }



        [HttpPost]
        public IActionResult Create(CityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _context.Countries.ToList();
                return View(model);
            }

            if (model.CountryId == 0)
            {
                ModelState.AddModelError("CountryId", "لطفاً یک کشور انتخاب نمایید.");
                model.Countries = _context.Countries.ToList();
                return View(model);
            }

            var city = new City
            {
                Name = model.Name,
                NameLocal = model.NameLocal,
                CountryId = model.CountryId,
                IsBirthPlace = model.IsBirthPlace,
                IsDocumentProcessPlace = model.IsDocumentProcessPlace
            };

            _context.Cities.Add(city);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var city = _context.Cities.Include(c => c.Country).FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            var model = new CityViewModel
            {
                Name = city.Name,
                NameLocal = city.NameLocal,
                IsBirthPlace = city.IsBirthPlace,
                IsDocumentProcessPlace = city.IsDocumentProcessPlace,
                CountryId = city.CountryId,
                Countries = _context.Countries.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = _context.Cities.Find(model.Id);

                if (city != null)
                {
                    city.Name = model.Name;
                    city.CountryId = model.CountryId;

                    _context.Update(city);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            model.Countries = _context.Countries.ToList();
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var city = _context.Cities
                .Include(c => c.Country)
                .FirstOrDefault(l => l.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var city = _context.Cities.Find(id);

            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }

}

