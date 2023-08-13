using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelToSQL.Models
{
    public class StaffInfoViewModel
    {
        public string StaffId { get; set;}
        public string url { get; set; }
        public string empId { get; set;}
        public string camId { get; set;}
        public string locId { get; set;}
        public string locNom { get; set; }
        public List<StaffInfoViewModel> StaffList { get; set;}
        public StaffInfoViewModel()
        {
            StaffList = new List<StaffInfoViewModel>();
        }
    }
}
