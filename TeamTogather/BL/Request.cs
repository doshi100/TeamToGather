using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class Request
    {
        public int RequestID { get; set; }
        public int PositionID { get; set; }
        public int UserID { get; set; }
        public int RequestType { get; set; }
        public int requestStatus { get; set; }
        public DateTime DateRequested { get; set; }
        public Profession ReqProfession { get; set; }

        /// <summary>
        /// builds an object by its Datarow, have the option not the initialize a profession object
        /// </summary>
        public Request(DataRow requestRow, bool prof)
        {
            this.RequestID = (int)requestRow["RequestID"];
            this.PositionID = (int)requestRow["PositionID"];
            this.UserID = (int)requestRow["UserID"];
            this.RequestType = (int)requestRow["RequestType"];
            this.DateRequested = (DateTime)requestRow["DateRequested"];
            if(prof)
            {
                this.ReqProfession = new Profession((int)requestRow["Profession"]);
            }
        }

        /// <summary>
        /// builds an object by its Datarow
        /// </summary>
        public Request(DataRow requestRow)
        {
            this.RequestID = (int)requestRow["RequestID"];
            this.PositionID = (int)requestRow["PositionID"];
            this.UserID = (int)requestRow["UserID"];
            this.RequestType = (int)requestRow["RequestType"];
            this.requestStatus = (int)requestRow["RequestStatus"];
            this.DateRequested = (DateTime)requestRow["DateRequested"];
            this.ReqProfession = new Profession((int)requestRow["Profession"]);
        }

        /// <summary>
        /// checks if an invitation was already sent by the user to a specific position in a project
        /// </summary>
        public static bool CheckRequestInvitation(int positionID, int userID)
        {
            return ProjectDB.CheckRequestInvitation(positionID, userID);
        }

    }

}
