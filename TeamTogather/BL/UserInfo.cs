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
        public List<Profession> LisrProf{ get; set; } // The professions of the user, such as: Animator, Write, Musician and etc', the ID's are in the table 'Professions'
        public int WeeklyFreeTime { get; set; } // the number of free time the user has to work on collabrative projects in a week.
        public int NumRateVoters { get; set; } // the number of users who voted if the user is a beginner or etc' used in average calculations later on.
        public int UserRate { get; set; } // the user's Rate, this Rate is incremented(by 1-5 according to the rater) every time someone rates the user, there is no table for Levels, this is written by code.
        public bool IsBanned { get; set; } // determines if the user is banned from the site by an administrator, if the user is banned he can't enter the site
        public int ProjectSum { get; set; } // determines the number of projects the user is working on(ACCORDING TO THE SITE, NOT THE USER)
        public DateTime RegistrationDate { get; set; } // the date in which the user signed up on.
        public DateTime LoginDate { get; set; } // saves the last date the user was logged in on, this should be changed in the table, only if the date has changed
        public int UserType { get; set; } // the user Type: 1. Admin 2. Regular, user types are available on the UserType Table in the database
        public string ProfilePath { get; set; }
        // usertype is ----> Type on the db
        public List<Project> UserProjects { get; set; } // saves the projects the user has.
        public List<UserKnowledge> UserPK { get; set; }// saves the skills the user have (example, the user knows how to program in c++ or know photoshop)
        // The end of the properties. **************************************************************************

        /// <summary>
        /// constructor no. 1, takes the properties from the user input and builds a User object
        /// </summary>
        public UserInfo(int ID, string UserName, string Password, string Email, DateTime Birthday, int NativeLang, int Country
                        , int WeeklyFreeTime, int NumRateVoters, int UserRate, bool IsBanned, int ProjectSum, DateTime RegistrationDate
                        ,DateTime LoginDate, int UserType)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Birthday = Birthday;
            this.NativeLang = NativeLang;
            this.Country = Country;
            this.WeeklyFreeTime = WeeklyFreeTime;
            this.NumRateVoters = NumRateVoters;
            this.UserRate = UserRate;
            this.IsBanned = IsBanned;
            this.ProjectSum = ProjectSum;
            this.RegistrationDate = RegistrationDate;
            this.LoginDate = LoginDate;
            this.UserType = UserType;
            this.UserProjects = Project.ReturnUserProjects(this.ID);
            this.UserPK = UserKnowledge.GetUserKnowledgeBL(this.ID);
        }


        /// <summary>
        /// constructor no. 2, takes an ID of a user and creats a user object
        /// </summary>
        public UserInfo(int userID)
        {
            try
            {
                DataRow userRow = UserDB.GetUserByID(userID);
                this.ID = (int)userRow["ID"];
                this.UserName = (string)userRow["UserName"];
                this.Password = (string)userRow["Pass"];
                this.Email = (string)userRow["Email"];
                this.Birthday = (DateTime)userRow["Birthday"]; 
                this.NativeLang = (int)userRow["NativeLang"];
                this.Country = (int)userRow["Country"];
                this.WeeklyFreeTime = (int)userRow["WeeklyFreeTime"];
                this.NumRateVoters = (int)userRow["NumRateVoters"];
                this.UserRate = (int)userRow["UserRate"];
                this.IsBanned = (bool)userRow["IsBanned"];
                this.ProjectSum = (int)userRow["ProjectsSum"];
                this.RegistrationDate = (DateTime)userRow["RegistrationDate"]; 
                this.LoginDate = (DateTime)userRow["LoginDate"]; 
                this.UserType = (int)userRow["Type"];
                this.ProfilePath = (string)userRow["ProfilePath"];
                this.UserProjects = Project.ReturnUserProjects(this.ID);
                this.UserPK = UserKnowledge.GetUserKnowledgeBL(this.ID);
            }
            catch (Exception)
            {
                // does nothing
            }

        }


        /// <summary>
        /// constructor no. 3, takes the properties from the user input and builds a User object with an option to build user's projects and user's Program Knowledge lists
        /// </summary>
        public UserInfo(int ID, string UserName, string Password, string Email, DateTime Birthday, int NativeLang, int Country
                        , int WeeklyFreeTime, int NumRateVoters, int UserRate, bool IsBanned, int ProjectSum, DateTime RegistrationDate
                        , DateTime LoginDate, int UserType, bool ListsFlag)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Birthday = Birthday;
            this.NativeLang = NativeLang;
            this.Country = Country;
            this.WeeklyFreeTime = WeeklyFreeTime;
            this.NumRateVoters = NumRateVoters;
            this.UserRate = UserRate;
            this.IsBanned = IsBanned;
            this.ProjectSum = ProjectSum;
            this.RegistrationDate = RegistrationDate;
            this.LoginDate = LoginDate;
            this.UserType = UserType;
            if (ListsFlag)
            {
                this.UserProjects = Project.ReturnUserProjects(ID);
                this.UserPK = UserKnowledge.GetUserKnowledgeBL(ID);
            }

        }

        /// <summary>
        /// checks if the given password and username match, if so, the function returns a user object which contains he's credentials
        /// </summary>
        public static UserInfo Authentication(string UsNa, string pass)
        {
            try
            {
                DataRow userRow = UserDB.UserAuthentication(UsNa, pass);
                if (userRow == null)
                {
                    return null;
                }
                // builds a user object by his ID;
                UserInfo authUser = new UserInfo((int)userRow["ID"]);
                return authUser;
            }
            catch(Exception)
            {
                return null;
            }

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
        public static int AddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                         , int WeeklyFreeTime, DateTime RegistrationDate
                         )
        {
            return UserDB.AddUser(UserName, Pass, Email, Birthday, NativeLang, Country
                         , WeeklyFreeTime, RegistrationDate
                         );
            
        }

        public static bool UserExist(string userN)
        {
            return UserDB.UserExist(userN);
        }



    }
}
