using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Services
{
    //From ChatGPT
    public static class Config
    {
        public static string RootDir => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ".GradeMasterMAUI");
    }
}
