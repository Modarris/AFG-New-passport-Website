using AFG_New_passport_Website.Models.UserAccount;
using AFG_New_passport_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // پیدا کردن کاربر بر اساس ایمیل وارد شده
                var user = await userManager.FindByEmailAsync(model.Username!);
                if (user != null)
                {
                    // لاگین با username واقعی کاربر
                    var result = await signInManager.PasswordSignInAsync(
                        user.UserName!, model.Password!, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Dashboard", "AdminAccount");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };


                Console.WriteLine("⚙ در حال ساخت کاربر جدید...");
                var result = await userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Dashboard", "AdminAccount");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    Console.WriteLine("❌ Error: " + error.Description);
                }


            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }


    }


}
