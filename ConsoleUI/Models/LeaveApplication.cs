using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Data.Enums;

namespace ConsoleUI.Models
{
    public class LeaveApplication
    {
        public int Id { get; set; }
        public LeaveCategory Type { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
