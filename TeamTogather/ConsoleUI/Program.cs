using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralMethods.SetDBPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../TeamTogatherDB.accdb"));

            UserInfo.Authintication("doshi", "123");


        }
    }
}
