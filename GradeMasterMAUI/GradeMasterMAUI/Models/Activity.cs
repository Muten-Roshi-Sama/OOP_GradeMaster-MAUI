using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Models
{
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


}
