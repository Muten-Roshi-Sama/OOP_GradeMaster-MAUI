using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GradeMasterMAUI.Services;

namespace GradeMasterMAUI.Services
{
    public class Config
    {
        public static string Dir =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".GradeMasterMAUI");
        //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        //Path.Combine(Directory.GetCurrentDirectory(), "Data");

        public static void EnsureDirAndAesKey()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            FileEncryptionService.InitializeEncryptionKey();
        }

        
    }
}
