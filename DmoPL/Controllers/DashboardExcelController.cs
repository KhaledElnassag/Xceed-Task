using DemoBll.Interfaces;
using DemoDAL.Models;
using DmoPL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DmoPL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DashboardExcelController : Controller
	{
		private readonly IGenericRepository<Empployee> _EmployeeRepo;
		public DashboardExcelController(IGenericRepository<Empployee> EmployeeRepo)
		{
			_EmployeeRepo = EmployeeRepo;
		}
		public IActionResult Index()
		{
			return View();
		}
        public async Task<IActionResult> Export()
        {
			
            var Employees = await _EmployeeRepo.GetAllWithSpecAsync(Emp => Emp.Include(e => e.Account));
            var stream = ExportExcel.ExportToExcel(Employees.ToList());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }

        public async Task<IActionResult> GetChartData()
        {
            var Employees = await _EmployeeRepo.GetAllWithSpecAsync(Emp => Emp.Include(e => e.Account));

            var pieChartLabels = Employees.Select(e => e.Account.AccountName).Distinct().ToList();
            var pieChartData = Employees.GroupBy(e => e.Account.AccountName).Select(g => g.Count()).ToList();

            var lineChartLabels = Employees.Select(e => e.Name).ToList();
            var lineChartData = Employees.Select(e => e.Age).ToList();

            var chartData = new
            {
                pieChartLabels,
                pieChartData,
                lineChartLabels,
                lineChartData
            };

            return Json(chartData);
        }
    }
}

