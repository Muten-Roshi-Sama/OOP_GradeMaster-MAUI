using GradeMasterMAUI.Services;
using System.Diagnostics;
using System.IO.Enumeration;
//using static Java.Util.Concurrent.Flow;
using GradeMasterMAUI.Models;

namespace GradeMasterMAUI.Models
{
    public class Professor : Person
    {
        private int salary; //good practice to use private, use a get function for reading the value. Now its protected from involuntary changes to salary.
        private static List<Professor> ProfessorList = []; //static: single list for all instances.

        public Professor(string firstname, string lastname, int salary)
            : base(firstname, lastname)
        {
            this.salary = salary;
            GetFileName = $"{Path.GetRandomFileName()}.Professor.txt";
            //ProfessorList.Add(this);  //not really needed since we recreate the list everytime.
        }

        //---Packing---
        public static Professor Unpack(string filename)
        {
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            //Debug.WriteLine($"prof file name : {SaveFilename}");
            string content = FileAccessService.ReadFile(SaveFilename, errorOrigin: "Professor-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            Professor professor = new(firstname: tokens[0], lastname: tokens[1], salary: Convert.ToInt32(tokens[2]))
            {
                GetFileName = filename
            };


            return professor;
        }
        public static void UnpackAll()
        {
            Config.EnsureDirectory();
            ProfessorList = new List<Professor>();
            IEnumerable<Professor> Allprofessors = Directory
                .EnumerateFiles(Config.Dir, "*.Professor.txt") //get a list of file names with extension *.student.txt
                .Select(filename => Professor.Unpack(Path.GetFileName(filename))) //deserialize each instance
                .OrderBy(professor => professor.DisplayName);
            foreach (var professor in Allprofessors)
            {
                ProfessorList.Add(professor);
            }
        }

        

        public void Pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, GetFileName);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Firstname, Lastname, GetSalary);
            FileAccessService.WriteFile(SaveFilename, content, identifier: "Professor", errorOrigin: "Professor-Pack");
        }




        //----Getters----

        public string GetSalary
        {
            get { return salary.ToString(); }
        }
        public static List<Professor> GetProfessorList()
        {
            return ProfessorList;
        }

        
    }
}
