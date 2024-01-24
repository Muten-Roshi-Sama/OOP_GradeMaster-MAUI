using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Models
{
    public interface IPackable
    {
        void Pack();
        void Unpack(string filename);

        //string GetFileName();
    }

}
