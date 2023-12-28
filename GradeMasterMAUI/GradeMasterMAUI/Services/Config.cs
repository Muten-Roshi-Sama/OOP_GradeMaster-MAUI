using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GradeMasterMAUI.Services
{
    //From ChatGPT
    public class Config
    {
        public static string Dir =>
            //@"C:\Users\Vass\POO\My_exam_template\GradeMasterMAUI\GradeMasterMAUI\Data";
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".GradeMasterMAUI");
        //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        //Path.Combine(Directory.GetCurrentDirectory(), "Data");

        public static void EnsureDirectory()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }
    }
}
