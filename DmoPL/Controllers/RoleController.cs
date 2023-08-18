using DmoPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DmoPL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index(string name) // action for main page of Employee
		{
				var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
				{
					Id = R.Id,
					RoleName = R.Name

				}).ToListAsync();
				return View(roles);
			

		}

	


		public IActionResult Create()
		{

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var role = new IdentityRole()
				{
					Name = model.RoleName
				};
				await _roleManager.CreateAsync(role);
				return RedirectToAction(nameof(Index));
			}
			return View(model);

		}

	}
}
