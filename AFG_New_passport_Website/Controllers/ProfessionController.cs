using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFG_New_passport_Website.Controllers
{
    public class ProfessionController : Controller
    {

        private readonly WebDbContext _context;
        public ProfessionController(WebDbContext context)
        {
            _context = context;
        }


        //Profession list
        public async Task<IActionResult> Index()
        {
            var professions = await _context.Professions.ToListAsync();
            return View(professions);
        }


        //Profession form for add
        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var profession = _context.Professions.Find(id);
            if (profession == null)
                return NotFound();

            profession.IsActive = !profession.IsActive; 
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Profession save to database
        [HttpPost]
        public IActionResult Create(Profession profession)
        {
            var existingProfession = _context.Professions
       .FirstOrDefault(pr => pr.Id == profession.Id);

            if (existingProfession != null)
            {
              
                string message = $"آی‌دی <span style='color:red;'>\"{existingProfession.Id}\"</span> قبلاً برای " +
                                 $"<span style='color:red;'>\"{existingProfession.Name}</span> ، " +
                                 $"<span style='color:red;'>{existingProfession.NameLocal}\"</span> ثبت شده است.";
                ViewBag.DuplicateMessage = message;
                return View(profession);
            }

            if (ModelState.IsValid)
            {
                _context.Professions.Add(profession);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profession);
        }


        //Profession edite view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var profession =await _context.Professions.FindAsync(id);
            if(profession == null)
            {
                return NotFound();
            }
            return View(profession);
        }


        //Profession edit save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profession profession)
        {
          
            var duplicateName = await _context.Professions
                .AnyAsync(p => p.Id != profession.Id && 
                               (p.Name == profession.Name || p.NameLocal == profession.NameLocal)); 

          
            if (duplicateName)
            {
                string message = $"نام شغل <span style='color:red;'>\"{profession.Name}\"</span> یا <span style='color:red;'>\"{profession.NameLocal}\"</span> قبلاً برای رکورد دیگری ثبت شده است.";
                ViewBag.DuplicateMessage = message;
                return View(profession);
            }

         
            if (ModelState.IsValid)
            {
            
                _context.Professions.Update(profession);
                await _context.SaveChangesAsync();

             
                return RedirectToAction("Index");
            }

            return View(profession);
        }





        [HttpGet]
        public IActionResult Delete(int id)
        {
            var profession = _context.Professions.Find(id);
            if(profession == null)
            {
                return NotFound();
            }
            return View(profession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int  id)
        {
            var profession = _context.Professions.Find(id);
            if (profession == null)
            {
                return NotFound();
            }

            _context.Professions.Remove(profession);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
