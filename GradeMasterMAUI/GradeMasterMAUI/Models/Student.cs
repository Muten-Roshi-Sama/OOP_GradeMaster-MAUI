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
        //possède plusieurs eval
        //obtenir la moyenne des evals
        private List<Eval> studentEvals = [];
        private static List<Student> StudentList = []; //static: single list for all instances.
        //private static readonly object _lockObj = new object();

        // Dictionary<Eval> evalDict;
        public Student(string firstname, string lastname)
            : base(firstname, lastname)
        {
            FileName = $"{Path.GetRandomFileName()}.Student.txt";
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
                if(eval.Student.FileName == FileName)
                {
                    studentEvals.Add(eval);
                }
                
            }
        }





        //---Packing---
        public static Student Unpack(string filename)
        {
            
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            string content = FileAccessService.ReadFile(SaveFilename, origin:"Student-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            Student student = new Student(firstname: tokens[0], lastname: tokens[1]);
            student.FileName = filename;
            //Debug.WriteLine($"Unpacking student from file: {filename
            //Debug.WriteLine($"Full path to file: {SaveFilename
            //Debug.WriteLine($"Tokens extracted: {string.Join(", ", tokens
            //Debug.WriteLine($"Student created: {student.DisplayName} with ID {student.PersonID}");

            return student;
        }

        public static void UnpackAll()
        {
            StudentList = new List<Student>();
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
            var SaveFilename = Path.Combine(Config.Dir, FileName);
            string data = string.Format("{1}{0}{2}", Environment.NewLine, Firstname, Lastname);
            FileAccessService.WriteFileAsync(SaveFilename, data, origin:"Student-Pack");
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
        //public string PersonID
        //{
        //    get { return personID; }
        //}

        //public double GetAverage()
        //{
        //    int ECTSsum = 0;
        //    int pointsTotal = 0;
        //    int EvalCount = 0;
        //    foreach (Eval eval in StudentEvals)
        //    {
        //        EvalCount += 1;
        //        ECTSsum += eval.Activity.ECTS;
        //        pointsTotal += eval.Note() * eval.activity.ECTS;
        //    }
        //    // Use double for the division to get a decimal result
        //    double activityAverage = (double)pointsTotal / EvalCount;
        //    return activityAverage / ECTSsum;
        //}
    }

}
