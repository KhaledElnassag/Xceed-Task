using AutoMapper;
using DemoBll.Interfaces;
using DemoBll.Repositories;
using DemoDAL.Models;
using DmoPL.Helper;
using DmoPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoPl.Controllers
{
	[Authorize(Roles = "Admin,User")]
	public class EmployeeController : Controller
	{
        private readonly IGenericRepository<Empployee> _EmployeeRepo;
        private readonly IGenericRepository<Account> _AccountRepo;
        private readonly IGenericRepository<BusinessLine> _BusinessRepo;
		private readonly IMapper _Mapper;

        public EmployeeController(IGenericRepository<Empployee> EmployeeRepo,IMapper mapper,
			IGenericRepository<Account> AccountRepo, IGenericRepository<BusinessLine> BusinessRepo) 
		{
			
            _EmployeeRepo = EmployeeRepo;
            _AccountRepo = AccountRepo;
            _BusinessRepo = BusinessRepo;
            _Mapper = mapper;
        }
		public async Task<IActionResult> Index() 
		{
            var role = User.FindFirstValue(ClaimTypes.Role);
            ViewData["User"]=role;
            var Employees = await _EmployeeRepo.GetAllWithSpecAsync(Emp=>Emp.Include(e=>e.Account));
            return View(Employees);
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Create() 
		{
            ViewData["Language"] = GetLnaguageAndLevel.Language;
            ViewData["LanguageLevel"] = GetLnaguageAndLevel.LanguageLevel;
            ViewData["Account"] =await _AccountRepo.GetAllAsync();
            ViewData["BusinessLine"] = new List<BusinessLine>();
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM) 
		{
			if (ModelState.IsValid)
			{

               var Employee = _Mapper.Map<Empployee>(EmployeeVM);
               await _EmployeeRepo.Add(Employee);
                var result = await _EmployeeRepo.Complete();
                if(result>0)
				    return RedirectToAction(nameof(Index));
			}
			return View(EmployeeVM);
		}
		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id) 
		{
            if (id is null)
              return BadRequest();
            
            var Employee =await _EmployeeRepo.GetByIdAsync(id.Value);
            if (Employee is null)
                return NotFound();

            var EmployeeViewMode = _Mapper.Map<EmployeeViewModel>(Employee);
			ViewData["Language"] = GetLnaguageAndLevel.Language;
			ViewData["LanguageLevel"] = GetLnaguageAndLevel.LanguageLevel;
			ViewData["Account"] = await _AccountRepo.GetAllAsync();
			ViewData["BusinessLine"] = await _BusinessRepo.GetAllWithSpecAsync((line) =>
            line.Where(L => L.AccountId == Employee.AccountId));

			return View(EmployeeViewMode);

		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
		{
            if (id != EmployeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Employee = _Mapper.Map<Empployee>(EmployeeVM);
                    _EmployeeRepo.Update(Employee);
                    var res=await _EmployeeRepo.Complete();
                    if(res>0)
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
            }
            return View(EmployeeVM);

        }
		[HttpGet]

		public async Task<IActionResult> Delete([FromRoute] int? id) 
		{
			if (id is null)
				return BadRequest();
				try
				{
					var Employee =await _EmployeeRepo.GetByIdAsync(id.Value);
					_EmployeeRepo.Delete(Employee);
					var res = await _EmployeeRepo.Complete();
					if (res > 0)
						return RedirectToAction(nameof(Index));

				}
				catch (Exception Ex)
				{
					ModelState.AddModelError(string.Empty, Ex.Message);
				}
		 	return RedirectToAction(nameof(Index));

		}

	

        [HttpGet]
        public async Task<IActionResult> GetBusinessLines([FromRoute] int? id)
        {
            if(id is not null)
            {
                var Lines = await _BusinessRepo.GetAllWithSpecAsync((line)=>line.Where(L=>L.AccountId==id));
                return Json(Lines);
            }
            return BadRequest("Id Not Valid");
        }
    }
}

