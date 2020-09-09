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
        public int TeamSize { get; set; }
        public string ProjectContent { get; set; }
        // ********end of properties*****************

        /// <summary>
        /// constructor no 1. by manual fields
        /// </summary>
        public Project(int ProjectID, int AdminUSID, int MinAge, int ProjectStatus, int NumRateVoters, int ProjectRate, int TeamSize, string ProjectContent)
        {
            this.ProjectID = ProjectID;
            this.AdminUSID = AdminUSID;
            this.MinAge = MinAge;
            this.ProjectStatus = ProjectStatus;
            this.NumRateVoters = NumRateVoters;
            this.ProjectRate = ProjectRate;
            this.TeamSize = TeamSize;
            this.ProjectContent = ProjectContent;
        }
        /// <summary>
        /// Empty Constructor no.2
        /// </summary>
        public Project()
        {

        }


        /// <summary>
        /// constructor no 1. by DataRow that is given by ProjectDB.ProjectByUserID(userID); method
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
                newpr.TeamSize = (int)row["TeamSize"];
                newpr.ProjectContent = (string)row["ProjectContent"];
                projects.Add(newpr);
            }
            return projects;
        }
        
    }
}
