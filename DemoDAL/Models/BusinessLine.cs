using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Models
{
    public class BusinessLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
