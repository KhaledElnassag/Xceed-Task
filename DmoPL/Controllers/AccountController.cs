using DemoDAL.Models;
using DmoPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace DmoPL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager,
            RoleManager<IdentityRole> roleRepo)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _roleManager = roleRepo;
        }

		#region Register
		public IActionResult Rigster()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Rigster(RegisterViewModel model)
		{
			if(ModelState.IsValid)
			{
				var AUser = new ApplicationUser()
				{
					FName= model.FName,
					LName= model.LName,
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree=model.IsAgree,
				};
				var result=await _userManager.CreateAsync(AUser, model.Password);
				if (result.Succeeded)
				{
					if (_userManager.Users.Count() == 1)
					{
						if (_roleManager.Roles.Where(r => r.Name == "Admin").Count()==0)
						{
							var role = new IdentityRole()
							{
								Name = "Admin"
							};
						await _roleManager.CreateAsync(role);
						}
                        await _userManager.AddToRoleAsync(AUser,"Admin");
                        await _userManager.UpdateAsync(AUser);
                    }
					return RedirectToAction(nameof(Login));
				}
				foreach (var Err in result.Errors)
				{
					ModelState.AddModelError(string.Empty, Err.Description);
				}
			}
			return View(model);
		}
		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var AUser = await _userManager.FindByEmailAsync(model.Email);
				if(AUser is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(AUser, model.Password);
					if (Flag == true)
					{
						var result = await _signInManager.PasswordSignInAsync(AUser, model.Password, model.RememberMe, false);
						if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
					}
				}
				ModelState.AddModelError(string.Empty, "Email Or Password Is Invalid");
			}
			return View(model);
		}
		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
		 await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

	}
}
