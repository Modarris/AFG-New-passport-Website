using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AFG_New_passport_Website.Controllers
{
    public class AppoinmentTypeController : Controller
    {
        private readonly WebDbContext _context;
        public AppoinmentTypeController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var appoinmentType = _context.AppoinmentTypes.ToList();

            return View(appoinmentType);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAsync(AppoinmentType appoinmentType)
        {
            if (ModelState.IsValid)
            {
                _context.AppoinmentTypes.Add(appoinmentType);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(appoinmentType);
        }


        //Profession edite view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appoinmentType = await _context.AppoinmentTypes.FindAsync(id);
            if (appoinmentType == null)
            {
                return NotFound();
            }
            return View(appoinmentType);
        }


    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppoinmentType appoinmentType)
        {
            if (ModelState.IsValid)
            {
                _context.AppoinmentTypes.Update(appoinmentType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appoinmentType);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var appoinmentType = _context.AppoinmentTypes.FirstOrDefault(p => p.Id == id);
            if (appoinmentType == null)
            {
                return NotFound();
            }
            return View(appoinmentType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appoinmentType = _context.AppoinmentTypes.Find(id);
            if (appoinmentType == null)
            {
                return NotFound();
            }
            _context.AppoinmentTypes.Remove(appoinmentType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
