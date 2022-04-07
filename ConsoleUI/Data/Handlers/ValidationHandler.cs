using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ConsoleUI.Models;

namespace ConsoleUI.Data.Handlers
{
    public static class ValidationHandler
    {
        public static int ValidateEmployee(string input, List<Employee> list)
        {
            var success = int.TryParse(input, out var output);

            if (success)
            {
                if (output <= list.Count)
                {
                    return output;
                }

                return -1;
            }

            return -1;
        }

        public static int ValidateType(string input)
        {
            var success = int.TryParse(input, out var output);

            if (success)
            {
                if (output <= 5)
                {
                    return output;
                }

                return -1;
            }

            return -1;

        }

        public static int ValidateMonth(string input)
        {
            var success = int.TryParse(input, out var output);

            if (success)
            {
                if (output <= 12)
                {
                    return output;
                }

                return -1;
            }

            return -1;

        }
    }
}
