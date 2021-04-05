using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class Project
    {
        /// <summary>
        /// start of project's Properties (the order is the same as in the db)
        /// </summary>
        public int ProjectID { get; set; }
        public int AdminUSID { get; set; }
        public int MinAge { get; set; }
        public int ProjectStatus { get; set; }
        public int NumRateVoters { get; set; }
        public int ProjectRate { get; set; }
        public string ProjectContent { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ProjectPos> ProjectPositions;
        // ********end of properties*****************

        /// <summary>
        /// constructor no 1. by manual fields
        /// </summary>
        public Project(int ProjectID, int AdminUSID, int MinAge, int ProjectStatus, int NumRateVoters, int ProjectRate, string ProjectContent, DateTime DateCreated)
        {
            this.ProjectID = ProjectID;
            this.AdminUSID = AdminUSID;
            this.MinAge = MinAge;
            this.ProjectStatus = ProjectStatus;
            this.NumRateVoters = NumRateVoters;
            this.ProjectRate = ProjectRate;
            this.ProjectContent = ProjectContent;
            this.DateCreated = DateCreated;
        }
        /// <summary>
        /// Empty Constructor no.2
        /// </summary>
        public Project()
        {

        }

        /// <summary>
        /// Constructor no.3 that builds a project object from an existing project in the db
        /// if LoadPositions = true => the project creates the Project positions aswell as the other fields.
        /// </summary>
        public Project(int ID, bool LoadPositions)
        {
            DataRow row = ProjectDB.ReturnProject(ID);
            this.ProjectID = (int)row["ProjectID"];
            this.AdminUSID = (int)row["AdminUSID"];
            this.MinAge = (int)row["MinAge"];
            this.ProjectStatus = (int)row["ProjectStatus"];
            this.NumRateVoters = (int)row["NumRateVoters"];
            this.ProjectRate = (int)row["ProjectRate"];
            this.ProjectContent = (string)row["ProjectContent"];
            this.DateCreated = (DateTime)row["DateCreated"];
            this.ProjectPositions = new List<ProjectPos>();
            DataTable dtPos = ProjectDB.ReturnProjectPositions(ProjectID);
            if (dtPos != null && LoadPositions)
            {
                foreach (DataRow posRow in dtPos.Rows)
                {
                    ProjectPos position = new ProjectPos((int)posRow["ID"], (int)posRow["UserID"], (int)posRow["Profession"]);
                    ProjectPositions.Add(position);
                }
            }
        }


        /// <summary>
        /// Constructor no.4 that builds a project object from an existing project in the db with a datarow that is given to it
        /// if LoadPositions = true => the project creates the Project positions aswell as the other fields.
        /// </summary>
        public Project(DataRow row, bool LoadPositions)
        {
            this.ProjectID = (int)row["ProjectID"];
            this.AdminUSID = (int)row["AdminUSID"];
            this.MinAge = (int)row["MinAge"];
            this.ProjectStatus = (int)row["ProjectStatus"];
            this.NumRateVoters = (int)row["NumRateVoters"];
            this.ProjectRate = (int)row["ProjectRate"];
            this.ProjectContent = (string)row["ProjectContent"];
            this.DateCreated = (DateTime)row["DateCreated"];
            this.ProjectPositions = new List<ProjectPos>();
            DataTable dtPos = ProjectDB.ReturnProjectPositions(ProjectID);
            if (dtPos != null && LoadPositions)
            {
                foreach (DataRow posRow in dtPos.Rows)
                {
                    ProjectPos position = new ProjectPos((int)posRow["ID"], (int)posRow["UserID"], (int)posRow["Profession"]);
                    ProjectPositions.Add(position);
                }
            }
        }

        /// <summary>
        /// Constructor no.5 that builds a project object from an existing project in the db with a datarow that is given to it
        /// </summary>
        public Project(DataRow row)
        {
            this.ProjectID = (int)row["ProjectID"];
            this.AdminUSID = (int)row["AdminUSID"];
            this.MinAge = (int)row["MinAge"];
            this.ProjectStatus = (int)row["ProjectStatus"];
            this.NumRateVoters = (int)row["NumRateVoters"];
            this.ProjectRate = (int)row["ProjectRate"];
            this.ProjectContent = (string)row["ProjectContent"];
            this.DateCreated = (DateTime)row["DateCreated"];
        }


        /// <summary>
        /// method that use the DataRow that is given by ProjectDB.ProjectByUserID(userID) and returns a list of projects the user has, that method will later be used +
        /// </summary>
        public static List<Project> ReturnUserProjects(int userID)
        {
            List<Project> projects = new List<Project>();
            DataTable userProject = ProjectDB.ProjectByUserID(userID);
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


        public static bool AddProject(int AdminUSID, int MinAge, int ProjectStatus, string ProjectContent,
            int AdminProfession, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            return ProjectDB.AddProject(AdminUSID, MinAge, ProjectStatus, ProjectContent, AdminProfession, ProfessionList);
        }


        public static List<Project> ShowProjects(int UserID, int AgeFilter, DateTime DateFilter, int RateFilter, int ProjectIndexShow)
        {
            List<Project> projects = new List<Project>();
            DataTable dt = ProjectDB.ShowProjects(UserID, AgeFilter, DateFilter, RateFilter, ProjectIndexShow);
            if(dt.Rows.Count != 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    Project p = new Project((int)row["ProjectID"], (int)row["AdminUSID"], (int)row["MinAge"],
                    (int)row["ProjectStatus"], (int)row["NumRateVoters"], (int)row["ProjectRate"], (string)row["ProjectContent"],
                    (DateTime)row["DateCreated"]);
                    projects.Add(p);
                }
            }
            return projects;
        }

        /// <summary>
        /// Add request to a position in a project, sepecify the user which requesting, state request status 1, if the Project Admin
        /// wants to add someone to his project, the RequestType should be 2, otherwise 1
        /// </summary>
        public static bool AddProjectRequest(int PositionID, int userID, int RequestStatus, int RequestType)
        {
            return ProjectDB.AddProjectReqeust(PositionID, userID, RequestStatus, RequestType);
        }


        public static bool CheckUserRate_Project(int projectID, int userID)
        {
            return ProjectDB.CheckUserRate_Project(projectID, userID);
        }


        /// <summary>
        /// returns the User rate to the project which is correlated to a specific ProjectID and userID
        /// </summary>
        public static int ReturnUserProjectRate(int projectID, int userID)
        {
            DataRow dtRow = ProjectDB.ReturnUserRateAtProject(projectID, userID);
            if(dtRow != null)
            {
                return (int)dtRow["ProjectRate"];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// inserts the user rate of a specific project
        /// </summary>
        public static bool InsertProjectRate_Rec(int projectID, int userID, int projectRate)
        {
            return ProjectDB.InsertProjectRate_Rec(projectID, userID, projectRate);
        }

        /// <summary>
        /// updates project's rate, rate should be SumRate + new_UserRate
        /// </summary>
        public static bool updateProjectRate(int ProjectID, int rate, bool newRate)
        {
            return ProjectDB.updateProjectRate(ProjectID, rate, newRate);
        }

        /// <summary>
        /// updates the user rate of a project
        /// </summary>
        public static bool updateUserRateAtProject(int ProjectID, int userID, int rate)
        {
            return ProjectDB.updateUserRateAtProject(ProjectID, userID, rate);
        }

        public static bool AddOrRemoveUserFromPos(int userID, int PosID)
        {
            return ProjectDB.AddOrRemoveUserFromPos(userID, PosID);
        }

        public static bool UpdateRequestStatus(int requestStatus, int requestID)
        {
            return ProjectDB.UpdateRequestStatus(requestStatus, requestID);
        }

        public static bool UpdateRequestStatusByPos(int positionID, int userID, int statusRe)
        {
            return ProjectDB.UpdateRequestStatusByPos(positionID, userID, statusRe);
        }

        public static bool UpdateProject(int minage, int projectStatus, string ProjectContent, int projectID, List<KeyValuePair<int, List<int>>> ProfessionList, int AdminID, int AdminProfession)
        {
            //if (ProjectDB.CheckProfessionPositionAtpos(projectID, AdminID, AdminProfession))
            //{
            //    int AdminCurrProf = returnProjAdminProf(projectID, AdminID);

            //}
            int Positionsuccess = 0;
            bool Programssuccess = true;
            if (ProfessionList.Count > 0)
            {
                Positionsuccess = ProjectDB.InsertProjPositions(projectID, ProfessionList);
                Programssuccess = ProjectDB.InsertPositionPrograms(projectID, ProfessionList, Positionsuccess);
            }
            bool updateproject = ProjectDB.UpdateProject(minage, projectStatus, ProjectContent, projectID);
            if(!ProjectDB.CheckProfessionPositionAtpos(projectID, AdminID, AdminProfession))
            {
                ProjectDB.UpdatePageAdminPos(AdminID, AdminProfession, projectID);
            }
            return Positionsuccess != -1 && Programssuccess && updateproject;
        }

        public static bool CheckProfessionPositionAtPos(int projectID, int AdminID, int AdminProfession)
        {
            return ProjectDB.CheckProfessionPositionAtpos(projectID, AdminID, AdminProfession);
        }

        public static bool DeletePos(int positionID)
        {
            return ProjectDB.DeletePos(positionID);
        }

        public static bool neutralizePositionsRequests(int positionID)
        {
            return ProjectDB.neutralizePositionsRequests(positionID);
        }

        public static List<int> returnProjAdminProf(int ProjectID, int AdminID)
        {
            return ProjectDB.returnProjAdminProf(ProjectID, AdminID);
        }

        public static Dictionary<int,string> returnProjectHeadLines(int userid)
        {
            Dictionary<int, string> Headers = new Dictionary<int, string>();
            DataTable dt = ProjectDB.returnProjectHeadLines(userid);
            foreach(DataRow row in dt.Rows)
            {
                Headers.Add((int)row["ProjectID"], (string)row["ProjectContent"]);
            }
            return Headers;
        }

        public static List<Project> ReturnTopProjects(DateTime date)
        {
            DataTable dt = ProjectDB.ReturnTopProjects(date);
            List<Project> list = new List<Project>();
            foreach (DataRow row in dt.Rows)
            {
                Project newProject = new Project(row);
                list.Add(newProject);
            }
            return list;
        }
    }
}
