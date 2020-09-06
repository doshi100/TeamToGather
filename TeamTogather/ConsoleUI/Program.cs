﻿using System;
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
            Console.WriteLine(AppDomain.CurrentDomain);
            bool check = UserInfo.Authentication("doshi", "123");
            Console.WriteLine(check);
            Console.ReadKey();

            /* things to remember :
               use 1 session that stores the user id after authentication and close it after 30 min.
               AppDomain.CurrentDomain.BaseDirectory gets me the base file which the program run on
               application domain is a mechanism like a process
             */
        }
    }
}
