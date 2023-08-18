using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Models
{
	public class Empployee
	{
		public int Id { get; set; }
		public long NationalId { get; set; }
        public string Name { get; set; }
		public DateTime DateofBirth { get; set; }
		public int Age { get; set; }
        public int AccountId { get; set; }
		public Account Account { get; set; }
		public string LineOfBusiness { get; set; }
        public string Language { get; set; }
        public string LanguageLevel { get; set; }


    }
}
