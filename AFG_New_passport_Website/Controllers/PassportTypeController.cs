using Microsoft.AspNetCore.Mvc;
using AFG_New_passport_Website.Models.Domain;
using AFG_New_passport_Website.Data;

namespace AFG_New_passport_Website.Controllers
{
    public class PassportTypeController : Controller
    {
        private readonly WebDbContext _context;
        public PassportTypeController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var passportType = _context.PassportTypes.ToList();

            return View(passportType);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PassportType passportType)
        {
            if (ModelState.IsValid)
            {
                _context.PassportTypes.Add(passportType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passportType);
        }


        //Profession edite view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var passportType = await _context.PassportTypes.FindAsync(id);
            if (passportType == null)
            {
                return NotFound();
            }
            return View(passportType);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassportType passportType)
        {
            if (ModelState.IsValid)
            {
                _context.PassportTypes.Update(passportType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passportType);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var passportType = _context.PassportTypes.FirstOrDefault(p => p.Id == id);
            if (passportType == null)
            {
                return NotFound();
            }
            return View(passportType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var passportType = _context.PassportTypes.Find(id);
            if (passportType == null)
            {
                return NotFound();
            }
            _context.PassportTypes.Remove(passportType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

