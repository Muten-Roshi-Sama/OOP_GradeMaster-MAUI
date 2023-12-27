using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Models;

namespace GradeMasterMAUI
{
    internal class DataInitializer
    {
        public static void InitializeData()
        {
            Person Jeanne = new Person("Jeanne", "Delafleur");
            Professor Trelawney = new Professor("Sybille ", "Trelawney ", 4000);
            Professor Snape = new Professor("Severus", "Snape", 8000);
            Professor Chourave = new Professor("Pomona", "Chourave", 8000);

            Activity Divination = new Activity("Divination", Trelawney, 30);
            Activity Potions = new Activity("Potions", Snape, 20);
            Activity Botanique = new Activity("Botanique", Chourave, 10);

            List<Eval> eval_Sophie = new List<Eval>
            {
                new Cote(15, Divination),
                new Cote(12, Potions),
                new Appreciation("X", Botanique)
            };
            Student Sophie = new Student("Sophie", "Marcourt", eval_Sophie);

            List<Eval> eval_andy = new List<Eval>
            {
                new Cote(17, Divination),
                new Cote(9, Potions),
                new Appreciation("B", Botanique)
            };

            Student Andy = new Student("Andy", "Myers", eval_andy);
        }
    }
}
