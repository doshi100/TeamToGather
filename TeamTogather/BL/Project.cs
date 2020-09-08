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
        /// constructor no 1. by DataRow that is given by ProjectDB.ProjectByUserID(userID); method
        /// </summary>
        public Project(int userID)
        {
            DataRow userProject = ProjectDB.ProjectByUserID(userID);
            ProjectID = (int)userProject["ProjectID"];
            AdminUSID = (int)userProject["AdminUSID"];
            MinAge = (int)userProject["MinAge"];
            ProjectStatus = (int)userProject["ProjectStatus"];
            NumRateVoters = (int)userProject["NumRateVoters"];
            ProjectRate = (int)userProject["ProjectRate"];
            TeamSize = (int)userProject["TeamSize"];
            ProjectContent = (string)userProject["TeamSize"];
        }
        
    }
}
