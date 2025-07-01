using AFG_New_passport_Website.Data;

using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFG_New_passport_Website.Controllers
{
    public class IdentityDocumentTypeController : Controller
    {
        private readonly WebDbContext _context;
        public IdentityDocumentTypeController(WebDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var identity = await _context.IdentityDocumentTypes.ToListAsync();
            return View(identity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityDocumentType model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "IdentityDocumentType");
            }
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var identity = _context.IdentityDocumentTypes.Find(id);
            if (identity == null)
            {
                return NotFound();
            }
            return View(identity);
        }

        [HttpPost]
        public IActionResult Edit(IdentityDocumentType identity)
        {
            if (ModelState.IsValid)
            {
                _context.Update(identity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(identity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var identity = _context.IdentityDocumentTypes.Find(id);
            if(identity == null)
            {
                return NotFound();
            }
            return View(identity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfermed(int id)
        {
            var identity = _context.IdentityDocumentTypes.Find(id);
            if(identity == null)
            {
                return NotFound();
            }

            _context.IdentityDocumentTypes.Remove(identity);
           _context.SaveChanges();
            return RedirectToAction("Index");
        }

  


    }
}
