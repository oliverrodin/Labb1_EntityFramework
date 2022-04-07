using ConsoleUI.Data.Enums;
using ConsoleUI.Data.Services;
using ConsoleUI.Models;

namespace ConsoleUI.Data.Handlers
{
    public class AppHandler : IAppHandler
    {
        private readonly ILeaveApplicationServices _services;
        public AppHandler(ILeaveApplicationServices services)
        {
            _services = services;
        }
        public void Run()
        {
            Welcome();
        }

        private void Welcome()
        {
            var run = "";

            while (run != "4")
            {
                Console.Clear();
                Console.WriteLine("Welcome To Vacation Planner! \n" +
                                  "What do you want to do?\n" +
                                  "\n" +
                                  "[1] - Create an Leave Application.\n" +
                                  "[2] - Get Leave Applications by Name.\n" +
                                  "[3] - Admin stuff.\n" +
                                  "[4] - Quit program.");
                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        CreateApplication();
                        break;
                    case ConsoleKey.D2:
                        ShowApplicationByName();
                        break;
                    case ConsoleKey.D3:
                        GetApplicationsFromSpecificMonth();
                        break;
                    case ConsoleKey.D4:
                        run = "4";
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("!!Invalid input!!\n" +
                                          "\n" +
                                          "Press Enter to go back to main menu.");
                        Console.ReadLine();
                        break;

                }
            }
            
        }

        private void CreateApplication()
        {
            var run = true;
            int nameId;
            DateTime startDate;
            DateTime endDate;
            int type;
            do
            {
                var employees = _services.GetAllEmployees();
                var count = 1;

                Console.Clear();
                Console.WriteLine("Which Employee are you? ");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.Id}. {employee.Name}");
                    count++;
                }
                nameId = ValidationHandler.ValidateEmployee(Console.ReadLine(), employees);
                if (nameId < 0)
                {
                    Console.WriteLine("Invalid input! Enter a valid number, Press enter and try again!");
                    Console.ReadLine();
                    
                } 
                else 
                    run = false;
            } while (run);

            run = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter start date (use this format: 2022-05-23)");
                var success= DateTime.TryParse(Console.ReadLine(), out startDate);

                if (!success)
                {
                    Console.WriteLine("Invalid input! Enter a valid Date, Press enter and try again!");
                    Console.ReadLine();
                }
                else
                    run = false;

            } while (run);

            run = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter end date (use this format: 2022-05-23)");
                var success= DateTime.TryParse(Console.ReadLine(), out endDate);

                if (!success)
                {
                    Console.WriteLine("Invalid input! Enter a valid Date, Press enter and try again!");
                    Console.ReadLine();
                }
                else
                    run = false;
            } while (run);

            run = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter why you're apply for leave:\n" +
                                  "1. Semester\n" +
                                  "2. VAB\n" +
                                  "3. Sjuk\n" +
                                  "4. Tjänstledighet\n" +
                                  "5. Annat");
                type = ValidationHandler.ValidateType(Console.ReadLine());
                if (type < 0)
                {
                    Console.WriteLine("Invalid input! Enter a valid number, Press enter and try again!");
                    Console.ReadLine();
                }
                else
                    run = false;
            } while (run);

            run = true;

            _services.CreateLeaveApplication(new LeaveApplication()
            {
                Created = DateTime.Now,
                EmployeeId = nameId,
                Type = (LeaveCategory) type,
                Startdate = startDate,
                EndDate = endDate


            });

            Console.Clear();
            Console.WriteLine("Your Application has been created and turned in for validation!\n" +
                              "Please press enter to go back to main menu.");
            Console.ReadLine();
        }

        private void ShowApplicationByName()
        {
            var created = "Created";
            var employee = "Employee";
            var span = "Timespan";
            var type = "Type";
            var run = true;
            var name = "";

            do
            {
                var employees = _services.GetAllEmployees();
                Console.Clear();

                Console.WriteLine("Enter the number of whos applications you want to see:");
                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.Id}. {e.Name}");
                }
                var nameId = ValidationHandler.ValidateEmployee(Console.ReadLine(), employees);
                if (nameId < 0)
                {
                    Console.WriteLine("Invalid input! Enter a valid number, Press enter and try again!");
                    Console.ReadLine();

                }
                else
                {
                    name = _services.GetAllEmployees().FirstOrDefault(x => x.Id == nameId).Name.ToString();
                    run = false;
                }
                    
            } while (run);

            var applications = _services.GetEmployeeByName(name);

            Console.Clear();
            Console.WriteLine($"Leave application for {name}:\n" +
                              $"");
            Console.WriteLine($"{created, -15}|{employee, -15}|{span, -31}|{type, -15} \n" +
                              $"----------------------------------------------------------------------");
            foreach (var app in applications)
            {
                Console.WriteLine($"{app.Created.ToString("yyyy MMMM dd"), -15}|" +
                                  $"{app.Employee.Name, -15}|" +
                                  $"{app.Startdate.ToString("yyyy MMMM dd"), -15}-" +
                                  $"{app.EndDate.ToString("yyyy MMMM dd"), 15}|" +
                                  $"{app.Type, -15}");
            }

            Console.WriteLine("\n" +
                              "Press any key to go back to main menu.");
            Console.ReadLine();
            
        }

        private void GetApplicationsFromSpecificMonth()
        {
            var run = true;
            var created = "Created";
            var employee = "Employee";
            var span = "Days off";
            int month;
            Array values = Enum.GetValues(typeof(Month));

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the number for the current month you want to se" +
                                  "Leave Applications for and press enter:\n" +
                                  "");
                foreach (Month val in values)
                {
                    Console.WriteLine($"{(int)val}. {Enum.GetName(typeof(Month), val)}");
                }

                month = ValidationHandler.ValidateMonth(Console.ReadLine());
                if (month < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input! Enter a valid number, Press enter and try again!");
                    Console.ReadLine();
                }
                else
                    run = false;
            } while (run);

            var filterDate = $"{DateTime.Now.Year:D4}-{month:D2}-{DateTime.Now.Day:D2}";
            var filteredApplications = _services.GetApplicationForSpecificMonth(DateTime.Parse(filterDate));

            Console.Clear();
            Console.WriteLine($"{created,-15}|{employee,-30}|{span, -10} \n" +
                              $"----------------------------------------------------------------------");
            if (filteredApplications.Count == 0)
            {
                Console.WriteLine("[There are no Leave Application created this month!] ");
            }
            else
            {
                foreach (var app in filteredApplications)
                {
                    var timeBetween = app.EndDate - app.Startdate;


                    Console.WriteLine($"{app.Created.ToString("yyyy MMMM dd"),-15}|" +
                                      $"{app.Employee.Name,-30}|" +
                                      $"{timeBetween.Days,-10}");


                }
            }
            

            Console.WriteLine("\n" +
                              "Press any key to go back to main menu.");
            Console.ReadLine();

            
        }

        
    }
}
