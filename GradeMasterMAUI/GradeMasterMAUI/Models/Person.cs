using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;

namespace GradeMasterMAUI.Models
{
    public class Person
    {
        private string fileName;
        private string firstname;
        private string lastname;


        public Person(string firstname, string lastname)
        {
            
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname
        {
            get { return firstname; }
        }

        public string Lastname
        {
            get { return lastname; }
        }
        public string GetFileName
        {
            get { return fileName; }
            set {  fileName = value; }
        }
        public string DisplayName
        {
            get { return $"{Firstname} {Lastname}"; }
        }
    }

}
