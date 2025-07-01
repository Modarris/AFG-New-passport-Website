using Microsoft.AspNetCore.Mvc;
using AFG_New_passport_Website.Models.Domain;
using AFG_New_passport_Website.Data;
using Microsoft.EntityFrameworkCore;


namespace AFG_New_passport_Website.Controllers
{
    public class LocationController : Controller
    {
        private readonly WebDbContext _context;

        public LocationController(WebDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var location = await _context.Locations.ToListAsync();
            return View(location);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameLocal, Name, IsActive, UsageType")] Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location); 
            }

            _context.Add(location);  
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var location = _context.Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            location.IsActive = !location.IsActive;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


       



    /*    [HttpGet]
        public IActionResult Edit(int id)
        {
            var location = _context.Locations
                .Include(l => l.Country) 
                .FirstOrDefault(l => l.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            var model = new LocationViewModel
            {
                Id = location.Id,
                NameLocal = location.NameLocal,
                Name = location.Name,
                CountryId = location.CountryId,
                Countries = _context.Countries.ToList()
            };

            return View(model);
        }




        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var location = _context.Locations.Find(model.Id);

                if (location != null)
                {
                    location.NameLocal = model.NameLocal;
                    location.Name = model.Name;
                    location.CountryId = model.CountryId;

                    _context.Update(location);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            model.Countries = _context.Countries.ToList();
            return View(model);
        }
*/



    /*    
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var location = _context.Locations
                .Include(l => l.Country) 
                .FirstOrDefault(l => l.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var location = _context.Locations.Find(id);

            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }*/

    }


    }

