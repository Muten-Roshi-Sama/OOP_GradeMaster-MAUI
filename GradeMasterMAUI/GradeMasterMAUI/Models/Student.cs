using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;

namespace GradeMasterMAUI.Models
{
    public class Student : Person
    {
        
        private List<Eval> studentEvals = [];
        private static List<Student> StudentList = []; //static: single list for all instances.

        public Student(string firstname, string lastname)
            : base(firstname, lastname)
        {
            GetFileName = $"{Path.GetRandomFileName()}.Student.txt";
            studentEvals = new List<Eval>();
            //StudentList.Add(this);  //not really needed since we recreate the list everytime.
        
        //this.StudentEvals = evaluations;
        }

        public void UpdateStudentEvalList()
        {
            studentEvals = [];
            
            IEnumerable<Eval> AllEval = Directory
                .EnumerateFiles(Config.Dir, "*.Eval.txt") //get a list of file names with extension *.student.txt
                .Select(filename => Eval.Unpack(Path.GetFileName(filename))) //deserialize each instance
                .OrderBy(eval => eval.GetEvalActivity);
            foreach (var eval in AllEval)
            {
                if(eval.Student.GetFileName == GetFileName)
                {
                    studentEvals.Add(eval);
                }
                
            }
        }

        //---Packing---
        public static Student Unpack(string filename)
        {
            
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            string content = FileAccessService.ReadFile(SaveFilename, errorOrigin: "Student-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            Student student = new Student(firstname: tokens[0], lastname: tokens[1]);
            student.GetFileName = filename;
            //Debug.WriteLine($"Unpacking student from file: {filename
            //Debug.WriteLine($"Full path to file: {SaveFilename
            //Debug.WriteLine($"Tokens extracted: {string.Join(", ", tokens
            //Debug.WriteLine($"Student created: {student.DisplayName} with ID {student.PersonID}");

            return student;
        }

        public static void UnpackAll()
        {
            StudentList = new List<Student>();
            Config.EnsureDirectory();
            IEnumerable<Student> Allstudents = Directory
                .EnumerateFiles(Config.Dir, "*.Student.txt") //get a list of file names with extension *.student.txt
                .Select(filename => Student.Unpack(Path.GetFileName(filename))) //deserialize each instance
                .OrderBy(student => student.DisplayName);
            foreach (var student in Allstudents)
            {
                StudentList.Add(student);
            }
        }

        public void Pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, GetFileName);
            string content = string.Format("{1}{0}{2}", Environment.NewLine, Firstname, Lastname);
            //FileAccessService.WriteFile(SaveFilename, data, errorOrigin:"Student-Pack");
            FileAccessService.WriteFile(SaveFilename, content, identifier: "Student", errorOrigin: "Student-Pack");
        }



        //---Getters------
        public static List<Student> GetStudentList()
        {
            return StudentList;
        }
        public List<Eval> GetStudentEvalList()
        {
            return studentEvals;
        }
        public void Add(Eval eval)
        {
            studentEvals.Add(eval);
        }
        
    }

}
