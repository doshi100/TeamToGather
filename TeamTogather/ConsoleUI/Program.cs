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
            //DateTime dt = GeneralMethods.CreateDateTime(2001, 11, 19);
            //UserInfo.AddUser("jane", "living", "jane@gmail.com", dt, 61, 6, 1, 5, DateTime.Now);
            Project p = new Project(1);
            Console.ReadKey();

            /* things to remember :
               use 1 session that stores the user id after authentication and close it after 30 min.
               add a class named Project, that contains the project fields.
               add a list of projects to UserInfo ----> List<Projects>
               AppDomain.CurrentDomain.BaseDirectory gets me the base file which the program run on
               application domain is a mechanism like a process
             */
        }
    }
}
