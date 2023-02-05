using AspNetCoreIdentityApp.Models;
using AspNetCoreIdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreIdentityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)      
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = request.UserName,
                    PhoneNumber = request.Phone,
                    Email = request.Email
                };

                var identityResult = await _userManager.CreateAsync(user, request.Password);

                if (identityResult.Succeeded)
                {                  
                    TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir";
                    return RedirectToAction(nameof(HomeController.SignUp));
                }

                foreach (IdentityError item in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
                return View(request);
        }

        public IActionResult SıgnIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SıgnIn(SignInViewModel request)
        {
            
            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı adınız veya parolanız hatalı lütfen tekrar deneyiniz.";
                return View();
            }

            var SignInresult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);
            
            if(SignInresult.Succeeded)
            {
                return RedirectToAction("Index", "Member");
            }
            if(SignInresult.IsLockedOut)
            {
                TempData["LockedOutMessage"] = "3 dakika sonra tekrar deneyiniz";
                return View();
            }
                TempData["ErrorMessage"] = "Kullanıcı adınız veya parolanız hatalı lütfen tekrar deneyiniz.";
                return View(request);
            

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}