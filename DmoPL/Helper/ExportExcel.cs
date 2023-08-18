using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using DemoDAL.Models;
using System.Collections.Generic;

namespace DmoPL.Helper
{
    public static class ExportExcel
    {
        public static MemoryStream ExportToExcel(IReadOnlyList<Empployee> Employees)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add headers
                worksheet.Cells[1, 1].Value = "National ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Date of Birth";
                worksheet.Cells[1, 4].Value = "Age";
                worksheet.Cells[1, 5].Value = "Account";
                worksheet.Cells[1, 6].Value = "Line of Business";
                worksheet.Cells[1, 7].Value = "Language";
                worksheet.Cells[1, 8].Value = "Language Level";

                // Add data
                for (int i = 0; i < Employees.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = Employees[i].NationalId;
                    worksheet.Cells[i + 2, 2].Value = Employees[i].Name;
                    worksheet.Cells[i + 2, 3].Value = Employees[i].DateofBirth.ToShortDateString();
                    worksheet.Cells[i + 2, 4].Value = Employees[i].Age;
                    worksheet.Cells[i + 2, 5].Value = Employees[i].Account.AccountName;
                    worksheet.Cells[i + 2, 6].Value = Employees[i].LineOfBusiness;
                    worksheet.Cells[i + 2, 7].Value = Employees[i].Language;
                    worksheet.Cells[i + 2, 8].Value = Employees[i].LanguageLevel;
                }

                // Generate the Excel file
                var stream = new MemoryStream(package.GetAsByteArray());
                
                return stream;

            }
        }
    }
}
