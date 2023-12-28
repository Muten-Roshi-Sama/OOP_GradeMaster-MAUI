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
        private List<Eval> StudentEvals = new List<Eval>();
        private static List<Student> StudentList = new List<Student>(); //static: single list for all instances.

        // Dictionary<Eval> evalDict;
        public Student(string firstname, string lastname)
            : base(firstname, lastname)
        {
            FileName = $"{Path.GetRandomFileName()}.Student.txt";
            StudentEvals = new List<Eval>();
            //this.StudentEvals = evaluations;
            //studentList.Add(this);
        }


        public static Student unpack(string filename)
        {
            Console.WriteLine($"Unpacking student from file: {filename}");

            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            Console.WriteLine($"Full path to file: {SaveFilename}");

            string content = File.ReadAllText(SaveFilename); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            Console.WriteLine($"Tokens extracted: {string.Join(", ", tokens)}");

            Student student = new Student(firstname: tokens[0], lastname: tokens[1]);
            student.FileName = filename;
            Console.WriteLine($"Student created: {student.DisplayName} with ID {student.PersonID}");

            return student;
        }

        public static void unpackAll()
        {
            StudentList = new List<Student>();
            Config.EnsureDirectory();
            IEnumerable<Student> students = Directory
                .EnumerateFiles(Config.Dir, "*.Student.txt") //get a list of file names with extension *.student.txt
                .Select(filename => Student.unpack(Path.GetFileName(filename))) //deserialize each instance
                .OrderBy(student => student.DisplayName);
            foreach (var student in students)
            {
                StudentList.Add(student);
            }
        }
        //public void 

        public void pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, FileName);
            string data = string.Format("{1}{0}{2}", Environment.NewLine, Firstname, Lastname);
            File.WriteAllText(SaveFilename, data);
        }



        //---Getters------
        public static List<Student> GetStudentList()
        {
            return StudentList;
        }
        public void Add(Eval eval)
        {
            StudentEvals.Add(eval);
        }
        public string PersonID
        {
            get { return personID; }
        }

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
