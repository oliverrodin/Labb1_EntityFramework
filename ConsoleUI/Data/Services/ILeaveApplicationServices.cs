using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Models;

namespace ConsoleUI.Data.Services
{
    public interface ILeaveApplicationServices
    {
        void CreateLeaveApplication(LeaveApplication leaveApplication);
        List<LeaveApplication> GetEmployeeByName(string name);
        List<LeaveApplication> GetApplicationForSpecificMonth(DateTime month);
        List<Employee> GetAllEmployees();

    }
}
