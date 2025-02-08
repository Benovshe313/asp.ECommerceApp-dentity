using ECommerceApp.UI.Entities;
using ECommerceApp.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.UI.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<CustomIdentityUser> _userManager;
		private readonly RoleManager<CustomIdentityRole> _roleManager;
		SignInManager<CustomIdentityUser> _signInManager;

		public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				CustomIdentityUser user = new CustomIdentityUser
				{
					UserName = model.Username,
					Email = model.Email,
				};
				IdentityResult result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					if (!(await _roleManager.RoleExistsAsync("Admin")))
					{
						CustomIdentityRole role = new CustomIdentityRole
						{
							Name = "Admin"
						};
						IdentityResult roleResult = await _roleManager.CreateAsync(role);
						if (!roleResult.Succeeded)
						{
							ModelState.AddModelError("RoleError", "We can not add the role");

						}
						
					}
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Login", "Account");

                }

			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password, model.RememberMe,false);
				if (result.Succeeded) {
					return RedirectToAction("Index", "Admin");
				}
				ModelState.AddModelError("", "Invalid login");
			}
			return View(model);
		}
        public IActionResult Login()
        {
            return View();
        }
    }

}

           
	
        
    
