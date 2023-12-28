using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;

namespace GradeMasterMAUI.Models
{


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
