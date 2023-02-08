using AspNetCoreIdentityApp.Areas.Admin.Models;
using AspNetCoreIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task <IActionResult> Index()
        {
            var roleList = await _roleManager.Roles.ToListAsync();
   
            var roleViewModelList = roleList.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(roleViewModelList);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            AppRole appRole = new AppRole()
            {
                Name = request.Name,               
            };

            var identityResult = await _roleManager.CreateAsync(appRole);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError item in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    return View();
                }
                
            }
            return View();
           // return RedirectToAction(nameof(RoleController.Index));
        }
    }
}
