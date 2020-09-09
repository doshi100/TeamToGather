using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class UserInfo
    {
        
        // the start of the properties *************************************************************************

        public int ID { get; set; } // ID of the user
        public string UserName { get; set; } // the User Name
        public string Password { get; set; } // the User Password(could be hashed, not sure at this time)
        public string Email { get; set; } // the User's Email
        public DateTime Birthday { get; set; } // The User Date of Birth
        public int NativeLang { get; set; } // the native language of the user, used later in filtering
        public int Country { get; set; } // the country the user lives in, used later in filtering
        public int Profession { get; set; } // The profession of the user, such as: Animator, Write, Musician and etc', the ID's are in the table 'Professions'
        public int WeeklyFreeTime { get; set; } // the number of free time the user has to work on collabrative projects in a week.
        public int NumRateVoters { get; set; } // the number of users who voted if the user is a beginner or etc' used in average calculations later on.
        public int UserRate { get; set; } // the user's Rate, this Rate is incremented(by 1-5 according to the rater) every time someone rates the user, there is no table for Levels, this is written by code.
        public bool IsBanned { get; set; } // determines if the user is banned from the site by an administrator, if the user is banned he can't enter the site
        public int ProjectSum { get; set; } // determines the number of projects the user is working on(ACCORDING TO THE SITE, NOT THE USER)
        public DateTime RegistrationDate { get; set; } // the date in which the user signed up on.
        public DateTime LoginDate { get; set; } // saves the last date the user was logged in on, this should be changed in the table, only if the date has changed
        public int UserType { get; set; } // the user Type: 1. Admin 2. Regular, user types are available on the UserType Table in the database
        // usertype is ----> Type on the db
        public List<Project> UserProjects { get; set; }
        // The end of the properties. **************************************************************************

        /// <summary>
        /// constructor no. 1, takes the properties from the user input and builds a User object
        /// </summary>
        public UserInfo(int ID, string UserName, string Password, string Email, DateTime Birthday, int NativeLang, int Country
                        ,int Profession, int WeeklyFreeTime, int NumRateVoters, int UserRate, bool IsBanned, int ProjectSum, DateTime RegistrationDate
                        ,DateTime LoginDate, int UserType)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Birthday = Birthday;
            this.NativeLang = NativeLang;
            this.Country = Country;
            this.Profession = Profession;
            this.WeeklyFreeTime = WeeklyFreeTime;
            this.NumRateVoters = NumRateVoters;
            this.UserRate = UserRate;
            this.IsBanned = IsBanned;
            this.ProjectSum = ProjectSum;
            this.RegistrationDate = RegistrationDate;
            this.LoginDate = LoginDate;
            this.UserType = UserType;
            this.UserProjects = Project.ReturnUserProjects(ID);
        }


        /// <summary>
        /// constructor no. 2, takes a datarow of a user and creats an user object
        /// </summary>
        public UserInfo(DataRow user)
        {
            this.ID = (int)user["ID"];
            this.UserName = (string)user["UserName"];
            this.Password = (string)user["Pass"];
            this.Email = (string)user["Email"];
            this.Birthday = (DateTime)user["Birthday"]; // I'm not sure it will work needs to be fixed, after I checked it. (DATETIME)
            this.NativeLang = (int)user["NativeLang"];
            this.Country = (int)user["Country"];
            this.Profession = (int)user["Profession"];
            this.WeeklyFreeTime = (int)user["WeeklyFreeTime"];
            this.NumRateVoters = (int)user["NumRateVoters"];
            this.UserRate = (int)user["UserRate"];
            this.IsBanned = (bool)user["IsBanned"];
            this.ProjectSum = (int)user["ProjectsSum"];
            this.RegistrationDate = (DateTime)user["RegistrationDate"]; // needs to be fixed (DATETIME)
            this.LoginDate = (DateTime)user["LoginDate"]; // needs to be fixed (DATETIME)
            this.UserType = (int)user["Type"];
            this.UserProjects = Project.ReturnUserProjects(this.ID);
        }

        /// <summary>
        /// checks if the given password and username match, if so, the function returns a user object which contains he's credentials
        /// </summary>
        public static UserInfo Authentication(string UsNa, string pass)
        {
            DataRow userRow = UserDB.UserAuthentication(UsNa, pass);
            if(userRow == null)
            {
                return null;
            }
            UserInfo authUser = new UserInfo(userRow);
            return authUser;
        }

        /// <summary>
        /// this method updates the db field and object's LoginDate, this method should be used after the user has authinticated using 'Authentication(string UsNa, string pass)'
        /// </summary>
        public void UpdateLoginDate()
        {
            UserDB.UpdateLoginDate(this.ID);
            this.LoginDate = DateTime.Now;
        }

        /// <summary>
        /// adds a user to the db
        /// </summary>
        /// <returns>true is user has been added false otherwise</returns>
        public static bool AddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                         , int Profession, int WeeklyFreeTime, DateTime RegistrationDate
                         )
        {
            return UserDB.AddUser(UserName, Pass, Email, Birthday, NativeLang, Country
                         , Profession, WeeklyFreeTime, RegistrationDate
                         );
        }



    }
}
