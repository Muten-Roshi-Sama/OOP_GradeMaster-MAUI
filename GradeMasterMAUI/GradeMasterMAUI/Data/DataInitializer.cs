using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Models;

namespace GradeMasterMAUI.Data
{
    class DataInitializer
    {
        public static void InitializeData()
        {
            Student Andy = new Student("Andy", "Myers");
            Student Sophie = new Student("Sophie", "Marcourt");
        }
        

    }
}
