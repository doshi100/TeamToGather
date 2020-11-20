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


    }
}
