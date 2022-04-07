﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<LeaveApplication> LeaveApplications { get; set; }

    }
}
