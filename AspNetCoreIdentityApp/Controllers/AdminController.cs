using AspNetCoreIdentityApp.Models;
using AspNetCoreIdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roleList = await _roleManager.Roles.ToListAsync();

            var roleViewModelList = roleList.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(roleViewModelList);
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new UserViewAdminModel()
            {
                UserId = x.Id,
                UserEmail = x.Email,
                UserName = x.UserName
            }).ToList();

            return View(userViewModelList);
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

            return RedirectToAction(nameof(AdminController.Index));
        }

        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);

            if (roleToDelete == null)
            {
                TempData["NullMessage"] = "Silinecek rol bulunamadı";
            }

            var result = await _roleManager.DeleteAsync(roleToDelete!);

            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    return View();
                }
            }

            return RedirectToAction(nameof(AdminController.Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            ViewBag.userId = id;

            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var roleViewModelList = new List<AssignRoleToUserViewModel>();
          

            foreach (var role in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel()
                {
                    Id = role.Id,
                    Name = role.Name!
                };

                if(userRoles.Contains(role.Name!))
                {
                    assignRoleToUserViewModel.Exist = true;
                }

                roleViewModelList.Add(assignRoleToUserViewModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId,List<AssignRoleToUserViewModel> requestList)
        {

            var userToAssignRoles = await _userManager.FindByIdAsync(userId);

            foreach(var role in requestList)
            {
                if(role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name);
                }
            }

            return RedirectToAction(nameof(AdminController.UserList));
           
        }
    }
        
    
}
