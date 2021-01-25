using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BL
{
    public class Request
    {
        public int RequestID { get; set; }
        public int PositionID { get; set; }
        public int UserID { get; set; }
        public int RequestType { get; set; }
        public DateTime DateRequested { get; set; }
        public Profession ReqProfession { get; set; }

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

    }

}
