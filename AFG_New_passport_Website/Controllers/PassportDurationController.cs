using Microsoft.AspNetCore.Mvc;
using AFG_New_passport_Website.Models.Domain;
using AFG_New_passport_Website.Data;


namespace AFG_New_passport_Website.Controllers
{
    public class PassportDurationController : Controller
    {


        private readonly WebDbContext _context;
        public PassportDurationController(WebDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var passportDuration = _context.PassportDurations.ToList();
            return View(passportDuration);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult CreateAsync(PassportDuration passportDuration)
        {
            if (ModelState.IsValid)
            {
                _context.PassportDurations.Add(passportDuration);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passportDuration);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var passportDuration = await _context.PassportDurations.FindAsync(id);
            if (passportDuration == null)
            {
                return NotFound();
            }
            return View(passportDuration);
        }

        [HttpPost]
        public IActionResult Edit(PassportDuration passportDuration)
        {
            if (ModelState.IsValid)
            {
                _context.PassportDurations.Update(passportDuration);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passportDuration);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var passportDuration = _context.PassportDurations.FirstOrDefault(p => p.Id == id);
            if (passportDuration == null)
            {
                return NotFound();
            }
            return View(passportDuration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var passportDuration = _context.PassportDurations.Find(id);
            if (passportDuration == null)
            {
                return NotFound();
            }
            _context.PassportDurations.Remove(passportDuration);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
