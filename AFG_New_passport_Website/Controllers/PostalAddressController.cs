using AFG_New_passport_Website.Data;
using AFG_New_passport_Website.Models;
using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFG_New_passport_Website.Controllers
{
    public class PostalAddressController : Controller
    {

        private readonly WebDbContext _context;
        public PostalAddressController(WebDbContext context)
        {
            _context = context;
        }

        // List
        public async Task<IActionResult> Index()
        {
            var postAdd = _context.PostalAddresses.Include(c => c.City).ToList();
            return View(postAdd);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PostAddViewModel
            {
               
                Cities = _context.Cities
                     
                      .ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Create(PostAddViewModel model)
        {
            var postAdd = new PostalAddress
            {
                Name = model.Name,
                CityId = model.CityId
            };
            _context.PostalAddresses.Add(postAdd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var postAdd = _context.PostalAddresses.Find(id);
            if (postAdd == null)
                return NotFound();

            postAdd.IsActive = !postAdd.IsActive; 
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        


             [HttpGet]
             public IActionResult Edit(int  id)
             {

                 var postalAdd = _context.PostalAddresses
                    .Include(p => p.City) 
                    .FirstOrDefault(p => p.Id == id); 

                 if (postalAdd == null)
                 {
                     return NotFound();
                 }

                 var model = new PostAddViewModel
                 {
                     Id = postalAdd.Id,
                     Name = postalAdd.Name,
                     CityId = postalAdd.CityId,
                     Cities = _context.Cities.ToList() 
                 };
                 return View(model);
             }
             [HttpPost]
             [ValidateAntiForgeryToken]
             public IActionResult Edit(PostAddViewModel model)
             {
                 if (ModelState.IsValid)
                 {
                     var postalAdd = _context.PostalAddresses.Find(model.Id);

                     if (postalAdd != null)
                     {
                         postalAdd.Name = model.Name;
                         postalAdd.CityId = model.CityId;

                         _context.Update(postalAdd);
                         _context.SaveChanges();
                         return RedirectToAction("Index");
                     }
                     return NotFound();
                 }

                 model.Cities = _context.Cities.ToList();
                 return View(model);
             }


      
                [HttpGet]
                public IActionResult Delete(int id)
                {
                    var postalAdd = _context.PostalAddresses
                        .Include(p => p.City) 
                        .FirstOrDefault(l => l.Id == id);

                    if (postalAdd == null)
                    {
                        return NotFound();
                    }

                    return View(postalAdd);
                }

               
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public IActionResult DeleteConfirmed(int id)
                {
                    var postalAdd = _context.PostalAddresses.Find(id);

                    if (postalAdd != null)
                    {
                        _context.PostalAddresses.Remove(postalAdd);
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return NotFound();
                }
    }
}
