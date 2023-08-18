using AutoMapper;
using DemoDAL.Models;
using DmoPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DmoPL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _RoleManager;
		private readonly IMapper _mapper;

		public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
			IMapper mapper, RoleManager<IdentityRole> releManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_RoleManager = releManager;
		}
		public async Task<IActionResult> Index() 
		{
			
				var users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					Role = _userManager.GetRolesAsync(U).Result.FirstOrDefault()
				}).ToListAsync();
				return View(users);
		}



		public async Task<IActionResult> Edit(string id) 
		{
			if (id is null)
			{
				return BadRequest();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
				return NotFound();
			var userVm = new UserViewModel()
			{
				Id = user.Id,
				FName = user.FName,
				LName = user.LName,
				Email = user.Email,
				Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
			};
			ViewData["UserRole"] = await _RoleManager.Roles.ToListAsync();
			return View(userVm);

		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel Updateduser) 
		{
			if (id != Updateduser.Id) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var mappedUser = await _userManager.FindByIdAsync(id);
					mappedUser.FName = Updateduser.FName;
					mappedUser.LName= Updateduser.LName;
					var roles = await _userManager.GetRolesAsync(mappedUser);
					if (roles?.Count==0)
					await _userManager.AddToRoleAsync(mappedUser,Updateduser.Role);
					var result= await _userManager.UpdateAsync(mappedUser);
					return RedirectToAction(nameof(Index));

				}
				catch (Exception Ex)
				{
					ModelState.AddModelError(string.Empty, Ex.Message);
				}
			}
			return View(Updateduser);

		}

	}
}
