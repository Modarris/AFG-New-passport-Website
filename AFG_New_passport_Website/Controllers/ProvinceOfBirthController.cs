using System.Threading.Tasks;
using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFG_New_passport_Website.Controllers
{
    public class ProvinceOfBirthController : Controller
    {
        private readonly WebDbContext _context;
        public ProvinceOfBirthController(WebDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var provinceOfBirth = await _context.ProvinceOfBirths.ToListAsync();
            return View(provinceOfBirth);
        }

        //ProvinceOfBirths form for add
        public IActionResult Create()
        {
            return View();
        }


        //ProvinceOfBirths save to database
        [HttpPost]
        public IActionResult Create(ProvinceOfBirth provinceOfBirth)
        {
            if (ModelState.IsValid)
            {
                _context.ProvinceOfBirths.Add(provinceOfBirth);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provinceOfBirth);
        }


        //ProvinceOfBirth edite view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var provinceOfBirth = await _context.ProvinceOfBirths.FindAsync(id);
            if (provinceOfBirth == null)
            {
                return NotFound();
            }
            return View(provinceOfBirth);
        }


        //ProvinceOfBirths edite save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProvinceOfBirth provinceOfBirth)
        {
            if (ModelState.IsValid)
            {
                _context.ProvinceOfBirths.Update(provinceOfBirth);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provinceOfBirth);
        }


        //ProvinceOfBirth delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var provinceOfBirth = _context.ProvinceOfBirths.Find(id);
            if (provinceOfBirth == null)
            {
                return NotFound();
            }
            _context.ProvinceOfBirths.Remove(provinceOfBirth);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
