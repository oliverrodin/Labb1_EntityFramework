using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUI.Context
{
    public class LeaveApplicationContext : DbContext
    {
        public LeaveApplicationContext(DbContextOptions<LeaveApplicationContext> options)
        : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }


    }
}
