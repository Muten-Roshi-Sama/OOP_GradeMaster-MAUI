using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Models
{
    public class Professor : Person
    {
        private double salary; //good practice to use private, use a get function for reading the value. Now its protected from involuntary changes to salary.

        public Professor(string firstName, string lastName, double salary)
            : base(firstName, lastName)
        {
            this.salary = salary;
        }

        public string GetSalary()
        {
            return salary.ToString();
        }
    }

}
