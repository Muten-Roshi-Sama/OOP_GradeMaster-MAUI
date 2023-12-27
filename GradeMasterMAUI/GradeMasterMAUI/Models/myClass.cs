using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Models
{
    public class Person
    {

        public string personID;
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
    }

    public class Student : Person
    {
        //possède plusieurs eval
        //obtenir la moyenne des evals
        private List<Eval> evaluations = new List<Eval>();
        private static List<Student> studentList = new List<Student>(); //static: single list for all instances.

        // Dictionary<Eval> evalDict;
        public Student(string firstName, string lastName, List<Eval> evaluations)
            : base(firstName, lastName)
        {
            this.evaluations = evaluations;
            studentList.Add(this);
        }
        public static List<Student> GetStudentList()
        {
            return studentList;
        }
        public void Add(Eval eval)
        {
            evaluations.Add(eval);
        }
        public string PersonID
        {
            get { return personID; }
        }

        public double GetAverage()
        {
            int ECTSsum = 0;
            int pointsTotal = 0;
            int EvalCount = 0;
            foreach (Eval eval in evaluations)
            {
                EvalCount += 1;
                ECTSsum += eval.activity.ECTS;
                pointsTotal += eval.Note() * eval.activity.ECTS;
            }
            // Use double for the division to get a decimal result
            double activityAverage = (double)pointsTotal / EvalCount;
            return activityAverage / ECTSsum;
        }
    }

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

    public class Activity
    {
        //possede un unique enseignant
        public int ECTS;
        public string activityName;
        public Professor professor; //activity has-a professor (composition).

        public Activity(string activityName, Professor professor, int ECTS)
        {
            this.activityName = activityName;
            this.professor = professor;
            this.ECTS = ECTS;
        }
    }

  
    

    public class Eval 
    {
        public static List<Eval> allGrades = new List<Eval>();
        public int grade { get; private set; }
        //private static List<Appreciation> ApprList { get; set; } = new List<Appreciation>();
        public Student student { get; private set; }

        public Eval(int eval, Activity activity)
        {
            this.grade = eval;
            allGrades.Add(this);
        }

        public void Note(string appr)
        {//type 2 : N(=4), C(=8), B(=12), TB(=16), X(=20)
            this.grade = appr switch
            {
                "X" => 20,
                "TB" => 16,
                "B" => 12,
                "C" => 8,
                "N" => 4,
                _ => 0,
            };
        }

        public void UpdateGrade(int grade)
        {
            this.grade = grade;
        }
    }

}
