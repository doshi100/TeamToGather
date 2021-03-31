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
        private List<UserContact> userContacts; // user's contacts such as youtube, facebook, instagram and etc. NOT INITATED IN A *****CONSTRUCTOR*****
        private List<ProtfolioCreation> UserCreations; // user's creations that are saved in a local folder, the creations are photos. NOT INITATED IN A *****CONSTRUCTOR*****
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
                this.RegistrationDate = (DateTime)userRow["RegistrationDate"]; 
                this.LoginDate = (DateTime)userRow["LoginDate"]; 
                this.UserType = (int)userRow["UserType"];
                if(userRow["ProfilePath"] != DBNull.Value)
                {
                    this.ProfilePath = (string)userRow["ProfilePath"];
                }
                this.UserProjects = Project.ReturnUserProjects(this.ID);
                this.UserPK = UserKnowledge.GetUserKnowledgeBL(this.ID);
            }
            catch (Exception)
            {
                // does nothing
            }

        }


        /// <summary>
        /// constructor no. 2, takes an ID of a user and creats a user object without or with lists of knowledge and professions. 'thing' doesn't matter.
        /// </summary>
        public UserInfo(int userID, bool BuildLists, bool thing)
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
                this.RegistrationDate = (DateTime)userRow["RegistrationDate"];
                if(userRow["LoginDate"] != DBNull.Value)
                {
                    this.LoginDate = (DateTime)userRow["LoginDate"];
                }
                this.UserType = (int)userRow["UserType"];
                this.ProfilePath = (string)userRow["ProfilePath"];
                if(BuildLists)
                {
                    this.UserProjects = Project.ReturnUserProjects(this.ID);
                    this.UserPK = UserKnowledge.GetUserKnowledgeBL(this.ID);
                }
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
        /// Creates an Object of a user and with only his ID
        /// </summary>
        public UserInfo(int UserID, bool CreateObject)
        {
            if (CreateObject)
            {
                this.ID = UserID; 
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
        public static bool AddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                        , int WeeklyFreeTime, DateTime RegistrationDate, List<int> ProfessionList, List<int> ProgramsList
                        )
        {
            return UserDB.GeneralAddUser(UserName, Pass, Email, Birthday, NativeLang, Country
                         , WeeklyFreeTime, RegistrationDate, ProfessionList, ProgramsList
                         );
            
        }

        public static bool UserExist(string userN)
        {
            return UserDB.UserExist(userN);
        }


        public static bool CheckAdmin(int id)
        {
            return UserDB.CheckAdmin(id);
        }

        /// <summary>
        /// Gets the user Professions by his id And returns a dictonary that contains valus as  : <ProfessionID><professionName>
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetUserProfessions()
        {
            Dictionary<int, string> userProfessions = new Dictionary<int, string>();
            DataTable dtProf = UserDB.GetUserProfessions(this.ID);
            if (dtProf != null)
            {
                foreach (DataRow row in dtProf.Rows)
                {
                    userProfessions.Add((int)row["ProfID"], (string)row["ProfName"]);
                }
            }
            return userProfessions;
        }
        /// <summary>
        /// NON-STATIC : Gets the user Professions by his id And returns a list that contains valus as a list of : <ProfessionID>
        /// </summary>
        public List<int> GetUserProfessionsList()
        {
            List<int> userProfessions = new List<int>();
            DataTable dtProf = UserDB.GetUserProfessions(this.ID);
            if (dtProf != null)
            {
                foreach (DataRow row in dtProf.Rows)
                {
                    userProfessions.Add((int)row["ProfID"]);
                }
            }
            return userProfessions;
        }

        public List<Profession> GetUserProfessionsList2()
        {
            List<Profession> userProfessions = new List<Profession>();
            DataTable dtProf = UserDB.GetUserProfessions2(this.ID);
            if (dtProf != null)
            {
                foreach (DataRow row in dtProf.Rows)
                {
                    Profession prof = new Profession((int)row["ProfessionID"], (string)row["ProfName"], (string)row["ProfPath"]);
                    userProfessions.Add(prof);
                }
            }
            return userProfessions;
        }
        /// <summary>
        /// STATIC : Gets the user Professions by his id And returns a list that contains valus as a list of : <ProfessionID>
        /// </summary>
        public static List<int> GetUserProfessions(int id)
        {
            List<int> userProfessions = new List<int>();
            DataTable dtProf = UserDB.GetUserProfessions(id);
            if (dtProf != null)
            {
                foreach (DataRow row in dtProf.Rows)
                {
                    userProfessions.Add((int)row["ProfID"]);
                }
            }
            return userProfessions;
        }


        public static List<UserInfo> ShowUsers(int profID, DateTime age, int langID, int WeeklyFreeTime, int userRate,
                                          int IndexUserID)
        {
            List<UserInfo> users = new List<UserInfo>();
            DataTable dtUsers = UserDB.ShowUsers(profID, age, langID, WeeklyFreeTime, userRate, IndexUserID);
            if (dtUsers.Rows.Count != 0)
            {
                foreach (DataRow row in dtUsers.Rows)
                {
                    UserInfo p = new UserInfo((int)row["ID"], true);
                    p.UserName = (string)row["UserName"];
                    if(row["ProfilePath"] != DBNull.Value)
                    {
                        p.ProfilePath = (string)row["ProfilePath"];
                    }
                    else
                    {
                        p.ProfilePath = "";
                    }
                    users.Add(p);
                }
            }
            return users;
        }

        /// <summary>
        /// setter for user's contacts
        /// </summary>
        public void SetUserContacts()
        {
            List<UserContact> Lusercontacts = new List<UserContact>();
            DataTable contacts = UserDB.GetUserContacts(this.ID);
            if(contacts.Rows.Count > 0)
            {
                foreach(DataRow contact in contacts.Rows)
                {
                    UserContact contact_obj = new UserContact(contact);
                    Lusercontacts.Add(contact_obj);
                }
            }
            this.userContacts = Lusercontacts;
        }

        /// <summary>
        /// getter for user's contacts
        /// </summary>
        public List<UserContact> GetUserContacts()
        {
            List<UserContact> contacts = new List<UserContact>(this.userContacts);
            return contacts;
        }

        /// <summary>
        /// setter for user's creations
        /// </summary>
        public void SetUserCreations()
        {
            List<ProtfolioCreation> Lusercreations = new List<ProtfolioCreation>();
            DataTable creations = UserDB.GetUserCreations(this.ID);
            if (creations.Rows.Count > 0)
            {
                foreach (DataRow creation in creations.Rows)
                {
                    ProtfolioCreation creation_obj = new ProtfolioCreation(creation);
                    Lusercreations.Add(creation_obj);
                }
            }
            this.UserCreations = Lusercreations;
        }

        /// <summary>
        /// getter for user's contacts
        /// </summary>
        public List<ProtfolioCreation> GetUserCreations()
        {
            List<ProtfolioCreation> creations = new List<ProtfolioCreation>(this.UserCreations);
            return creations;
        }

        public static bool RateUser(int UserRatedID, int userID, int UserRate)
        {
            return UserDB.RateUser(UserRatedID, userID, UserRate);
        }


        public List<Project> GetUserDoneProjects(DateTime projectDate)
        {
            List<Project> doneProjects = new List<Project>();
            DataTable doneProjects_dt = UserDB.GetUserDoneProjects(this.ID , projectDate);
            if(doneProjects_dt.Rows.Count > 0)
            {
                foreach(DataRow row in doneProjects_dt.Rows)
                {
                    Project rowP = new Project(row, true);
                    doneProjects.Add(rowP);
                }
            }
            return doneProjects;
        }

        public Dictionary<Request, string> GetProjectsUserRequest(DateTime projectDate)
        {
            Dictionary<Request, string> requests = new Dictionary<Request, string>();
            DataTable dt = UserDB.GetProjectsUserRequest(this.ID, projectDate);
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    string projectContent = (string)row["ProjectContent"];
                    Request re = new Request(row, true);
                    requests.Add(re, projectContent);
                }
            }
            return requests;
        }

        public Dictionary<Request, Project> GetUserRequestsToProjects(DateTime projectDate)
        {
            Dictionary<Request, Project> requests = new Dictionary<Request, Project>();
            DataTable dt = UserDB.GetUserRequestsToProjects(this.ID, projectDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Request re = new Request(row, true);
                    Project proj = new Project(row, false);
                    requests.Add(re, proj);
                }
            }
            return requests;
        }


        public List<Project> ReturnUserProjects(DateTime dtIndex)
        {
            List<Project> projects = new List<Project>();
            DataTable userProject = ProjectDB.ProjectsByUserID(this.ID, dtIndex);
            foreach (DataRow row in userProject.Rows)
            {
                Project newpr = new Project();
                newpr.ProjectID = (int)row["ProjectID"];
                newpr.AdminUSID = (int)row["AdminUSID"];
                newpr.MinAge = (int)row["MinAge"];
                newpr.ProjectStatus = (int)row["ProjectStatus"];
                newpr.NumRateVoters = (int)row["NumRateVoters"];
                newpr.ProjectRate = (int)row["ProjectRate"];
                newpr.ProjectContent = (string)row["ProjectContent"];
                newpr.DateCreated = (DateTime)row["DateCreated"];
                projects.Add(newpr);
            }
            return projects;
        }


        public string ReturnLangByID()
        {
            return UserDB.ReturnLangByID(this.NativeLang);
        }

        public static void UpdateProfilePhoto(string path, int id)
        {
            UserDB.UpdateProfilePhoto(path, id);
        }

        public static string ReturnUserProfilePath(int userID)
        {
            return UserDB.ReturnUserProfilePath(userID);
        }


        public static int ReturnUserRateAtUserNum(int UserRatedID, int userID)
        {
            return UserDB.ReturnUserRateAtUserNum(UserRatedID, userID);
        }

        /// <summary>
        /// return a list of the requests the user sent to project's admins. (contains adminUSID and ProjectID on project object, and request's info on request.)
        /// </summary>
        public static List<KeyValuePair<Project, Request>> returnSentUserReq(int userID, DateTime ReqFromDate)
        {
            List<KeyValuePair<Project, Request>> reqList = new List<KeyValuePair<Project, Request>>();
            DataTable dt = UserDB.returnSentUserReq(userID, ReqFromDate);
            foreach(DataRow row in dt.Rows)
            {
                Request req = new Request(row);
                Project proj = new Project();
                proj.AdminUSID = (int)row["AdminUsID"];
                proj.ProjectID = (int)row["ProjectID"];
                proj.ProjectContent = (string)row["ProjectContent"];
                KeyValuePair<Project, Request> record = new KeyValuePair<Project, Request>(proj, req);
                reqList.Add(record);
            }
            return reqList;
        }

        /// <summary>
        /// return a list of the requests the user sent to project's admins. (contains adminUSID and ProjectID on project object, and request's info on request.)
        /// </summary>
        public static List<KeyValuePair<Project, Request>> returnSentUserInv(int userID, DateTime ReqFromDate)
        {
            List<KeyValuePair<Project, Request>> reqList = new List<KeyValuePair<Project, Request>>();
            DataTable dt = UserDB.returnSentUserInv(userID, ReqFromDate);
            foreach (DataRow row in dt.Rows)
            {
                Request req = new Request(row);
                Project proj = new Project();
                proj.AdminUSID = (int)row["AdminUsID"];
                proj.ProjectID = (int)row["ProjectID"];
                proj.ProjectContent = (string)row["ProjectContent"];
                KeyValuePair<Project, Request> record = new KeyValuePair<Project, Request>(proj, req);
                reqList.Add(record);
            }
            return reqList;
        }
    }
}
