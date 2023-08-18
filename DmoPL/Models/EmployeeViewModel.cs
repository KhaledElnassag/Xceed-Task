using DemoDAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace DmoPL.Models
{
	public class EmployeeViewModel
	{

		public int Id { get; set; }

        [Required(ErrorMessage = "National ID is required")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = " National ID Must 14 number Only")]
        public long NationalId { get; set; }

		[Range(18, 60,ErrorMessage= "Age Must be More than 18")]
		public int Age { get; set; }		

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name Must Be Text Only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime DateofBirth { get; set; }
        [Required(ErrorMessage = "Account is required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Business Line is required")]
        public string LineOfBusiness { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Language Level is required")]
        public string[] LanguageLevel { get; set; }
    }
}
