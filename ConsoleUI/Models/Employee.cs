namespace ConsoleUI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<LeaveApplication> LeaveApplications { get; set; }

    }
}
