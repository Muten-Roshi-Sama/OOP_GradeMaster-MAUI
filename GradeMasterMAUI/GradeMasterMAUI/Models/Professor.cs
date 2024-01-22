using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;
using System.IO.Enumeration;

namespace GradeMasterMAUI.Models
{
    public class Professor : Person
    {
        private int salary; //good practice to use private, use a get function for reading the value. Now its protected from involuntary changes to salary.
        private static HashSet<string> processedProfessorFiles = new HashSet<string>();

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
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            //Debug.WriteLine($"prof file name : {SaveFilename}");
            string content = FileAccessService.ReadFile(SaveFilename, errorOrigin: "Professor-Unpack"); //reads content of txt
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
            Config.EnsureDirectory();
            var professorFiles = Directory.EnumerateFiles(Config.Dir, "*.Professor.txt");

            foreach (var file in professorFiles)
            {
                string fileName = Path.GetFileName(file);
                if (!processedProfessorFiles.Contains(fileName))
                {
                    var professor = Professor.Unpack(fileName);
                    ProfessorList.Add(professor);
                    // Debug.WriteLine($"filename : {fileName}");
                    processedProfessorFiles.Add(fileName);
                }
            }
        }

        public void Pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, FileName);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Firstname, Lastname, Salary);
            FileAccessService.WriteFile(SaveFilename, content, identifier: "Professor", errorOrigin: "Professor-Pack");
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
