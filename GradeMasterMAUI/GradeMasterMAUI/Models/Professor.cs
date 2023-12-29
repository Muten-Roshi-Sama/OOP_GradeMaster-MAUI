using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;

namespace GradeMasterMAUI.Models
{
    public class Professor : Person
    {
        private int salary; //good practice to use private, use a get function for reading the value. Now its protected from involuntary changes to salary.
        private static List<Professor> ProfessorList = new List<Professor>(); //static: single list for all instances.

        public Professor(string firstname, string lastname, int salary)
            : base(firstname, lastname)
        {
            this.salary = salary;
            FileName = $"{Path.GetRandomFileName()}.Professor.txt";
            //ProfessorList.Add(this);  //not really needed since we recreate the list everytime.
        }

        //---Packing---
        public static Professor Unpack(string filename)
        {
                    
            Config.EnsureDirectory();
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            string content = FileAccessService.ReadFile(SaveFilename, origin: "Professor-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            Professor professor = new Professor(firstname: tokens[0], lastname: tokens[1], salary: Convert.ToInt32(tokens[2]));
            professor.FileName = filename;

            //Debug.WriteLine($"Student created: {student.DisplayName} with ID {student.PersonID}");
            //Debug.WriteLine($"Unpacking Professor from file: {filename}");
            //Debug.WriteLine($"Full path to file: {SaveFilename
            //Debug.WriteLine($"Tokens extracted: {string.Join(", ", tokens
            return professor;
        }

        public static void UnpackAll()
        {
            ProfessorList = new List<Professor>();
            Config.EnsureDirectory();
            IEnumerable<Professor> Allprofessors = Directory
                .EnumerateFiles(Config.Dir, "*.Professor.txt") //get a list of file names with extension *.student.txt
                .Select(filename => Professor.Unpack(Path.GetFileName(filename))) //deserialize each instance
                .OrderBy(professor => professor.DisplayName);
            foreach (var prof in Allprofessors)
            {
                ProfessorList.Add(prof);
            }
        }
        //public void 

        public void Pack()
        {
            Config.EnsureDirectory();
            var SaveFilename = Path.Combine(Config.Dir, FileName);
            string data = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Firstname, Lastname, Salary);
            FileAccessService.WriteFile(SaveFilename, data, origin: "Professor-Pack");
        }




        //----Getters----

        public string Salary
        {
            get { return salary.ToString(); }
        }
        public static List<Professor> GetProfessorList()
        {
            return ProfessorList;
        }
    }
    
}
