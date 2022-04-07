using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Context;
using ConsoleUI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUI.Data.Services
{
    public class LeaveApplicationServices : ILeaveApplicationServices
    {
        private readonly LeaveApplicationContext _context;

        public LeaveApplicationServices(LeaveApplicationContext context)
        {
            _context = context;
        }

        public void CreateLeaveApplication(LeaveApplication leaveApplication)
        {
            _context.LeaveApplications.Add(leaveApplication);
            _context.SaveChanges();
        }

        public List<LeaveApplication> GetEmployeeByName(string name)
        {
            var employee = _context.Employees
                .FirstOrDefault(x => x.Name == name);

            return _context.LeaveApplications
                .Where(x => x.EmployeeId == employee.Id)
                .OrderBy(x => x.Created)
                .ToList();
        }

        public List<LeaveApplication> GetApplicationForSpecificMonth(DateTime date)
        {
            return _context.LeaveApplications
                .Where(x => x.Created.Month == date.Month)
                .Include(x => x.Employee)
                .OrderBy(x => x.Created)
                .ToList();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
    }
}
