using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFG_New_passport_Website.Controllers
{
    public class GenderController : Controller
    {
        private readonly WebDbContext _context;

        public GenderController(WebDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult>Index()
        {
            var genders = await _context.Genders.ToListAsync();
            return View(genders);
        }


       
        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }

        // preview
        [HttpPost]
        public IActionResult Preview(Gender model)
        {
            return View(model); // نمایش صفحه Preview
        }


        [HttpPost]
        public IActionResult Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Genders.Add(gender);
                _context.SaveChanges();
                return RedirectToAction("Index", "Gender");
            }


            return View("Preview", gender);
        }


        // GET: Gender/Edit/5
        /*     [HttpGet]
             public async Task<IActionResult> Edit(int id)
             {
                 var gender = await _context.Genders.FindAsync(id);
                 if (gender == null)
                 {
                     return NotFound();
                 }
                 return View("Create", gender);
             }
     */
        public IActionResult PreviewEdit(Gender model)
        {
            // داده ها در model هستند
            return View(model);
        }
        // POST: Gender/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Genders.Update(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Gender");
            }

            return View(gender);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var gender = _context.Genders.FirstOrDefault(p => p.Id == id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var gender = _context.Genders.Find(id);
            if (gender == null)
            {
                return NotFound();
            }
            _context.Genders.Remove(gender);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}
