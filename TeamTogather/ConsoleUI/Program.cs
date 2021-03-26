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
            //GeneralMethods.SetDBPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../TeamTogatherDB.accdb"));
            ////DateTime dt = GeneralMethods.CreateDateTime(2001, 11, 19);
            ////UserInfo.AddUser("jane", "living", "jane@gmail.com", dt, 61, 6, 1, 5, DateTime.Now);
            //List<int> Programs1 = new List<int>();
            //Programs1.Add(10);
            //Programs1.Add(9);
            //List<int> Programs2 = new List<int>();
            //Programs2.Add(6);
            //List<int> Programs3 = new List<int>();
            //List<int> Programs4 = new List<int>();
            //Programs4.Add(22);
            //Programs4.Add(21);
            //Programs4.Add(20);
            //List<KeyValuePair<int, List<int>>> ProfessionList = new List<KeyValuePair<int, List<int>>>();
            //KeyValuePair<int, List<int>> k1 = new KeyValuePair<int, List<int>>(17, Programs1);
            //KeyValuePair<int, List<int>> k2 = new KeyValuePair<int, List<int>>(1, Programs2);
            //KeyValuePair<int, List<int>> k3 = new KeyValuePair<int, List<int>>(12, Programs3);
            //KeyValuePair<int, List<int>> k4 = new KeyValuePair<int, List<int>>(7, Programs4);
            //ProfessionList.Add(k1);
            //ProfessionList.Add(k2);
            //ProfessionList.Add(k3);
            //ProfessionList.Add(k4);
            //ProfessionList = ProfessionList.OrderBy(KeyValuePair => KeyValuePair.Key).ToList();
            //Console.WriteLine(Project.AddProject(29, 21, 1, "this is gonna be the third project opened programmatically", 15, ProfessionList));
            //Console.ReadKey();

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
