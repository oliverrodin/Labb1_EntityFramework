using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI.Context;
using ConsoleUI.Data.Enums;
using ConsoleUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleUI.Handlers
{
    public class DataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LeaveApplicationContext(
                       serviceProvider.GetRequiredService<
                           DbContextOptions<LeaveApplicationContext>>()))
            {
                //Employees
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee(){ Name = "Oliver Rodin"},
                        new Employee(){ Name = "Fredrik Johansson"},
                        new Employee(){ Name = "Anna Jansson"},
                        new Employee(){ Name = "Emma Filipsson"},
                        new Employee(){ Name = "Rickard Bergman"},
                        new Employee(){ Name = "Sofie Hansen"}
                    });

                    context.SaveChanges();
                }
                //leaveApplications
                if (!context.LeaveApplications.Any())
                {
                    context.LeaveApplications.AddRange(new List<LeaveApplication>()
                    {
                        new LeaveApplication()
                        {
                            EmployeeId = 1,
                            Created = DateTime.Now.AddDays(-3),
                            Startdate = DateTime.Now.AddDays(+10),
                            EndDate = DateTime.Now.AddDays(+15),
                            Type = LeaveCategory.Annat

                        },
                        new LeaveApplication()
                        {
                            EmployeeId = 6,
                            Created = DateTime.Now.AddDays(-2),
                            Startdate = DateTime.Parse("2022-07-01"),
                            EndDate = DateTime.Parse("2022-08-01"),
                            Type = LeaveCategory.Semester

                        },
                        new LeaveApplication()
                        {
                            EmployeeId = 2,
                            Created = DateTime.Now,
                            Startdate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(+4),
                            Type = LeaveCategory.VAB

                        },
                        new LeaveApplication()
                        {
                        EmployeeId = 5,
                        Created = DateTime.Now.AddDays(-20),
                        Startdate = DateTime.Parse("2022-07-12"),
                        EndDate = DateTime.Parse("2022-08-30"),
                        Type = LeaveCategory.Semester

                        },
                        new LeaveApplication()
                        {
                            EmployeeId = 3,
                            Created = DateTime.Now,
                            Startdate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(6),
                            Type = LeaveCategory.Sjuk

                        },
                        new LeaveApplication()
                        {
                            EmployeeId = 4,
                            Created = DateTime.Now.AddDays(-20),
                            Startdate = DateTime.Parse("2022-05-12"),
                            EndDate = DateTime.Parse("2023-01-01"),
                            Type = LeaveCategory.Tjänstledigt

                        },

                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
