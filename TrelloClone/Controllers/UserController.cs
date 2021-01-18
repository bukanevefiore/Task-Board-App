using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrelloClone.Infrastructure;
using TrelloClone.ViewModels;

namespace TrelloClone.Controllers
{
    public class UserController : Controller
    {
        // kullanıcılar için controller oluştuurlması
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // yeni kayıt aksiyon
        public IActionResult Register()
        {
            return View();
        }
        // kayıt işlmleri
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                // vverilerin eşlenmesi
                var user = new AppUser 
                { 
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager
                    .CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View(model);//sonucu diöndür
        } 
        
        public IActionResult Login()
        {
            return View();
        }
        // login için token işlmeleri
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction(nameof(Login));
        }
    }
}