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

        public string personID;
        public string FileName;
        private string firstname;
        private string lastname;


        public Person(string firstname, string lastname, string personID = "0")
        {
            if (personID == "0")
            {
                personID = Guid.NewGuid().ToString();
            }
            this.firstname = firstname;
            this.lastname = lastname;
            this.personID = personID;
        }

        public string Firstname
        {
            get { return firstname; }
        }

        public string Lastname
        {
            get { return lastname; }
        }
        public string DisplayName
        {
            get { return $"{Firstname} {Lastname}"; }
        }
    }

}
