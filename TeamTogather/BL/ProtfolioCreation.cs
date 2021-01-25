using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BL
{
    public class ProtfolioCreation
    {
        public int CreationID { get; set; }
        public string CreationPath { get; set; }
        public int UserID { get; set; }

        /// <summary>
        /// no. 1 empty constructor
        /// </summary>
        public ProtfolioCreation()
        {

        }

        /// <summary>
        /// no. 2 builds a user Protfolio Creation object by a DataRow
        /// </summary>
        public ProtfolioCreation(DataRow UserContact)
        {
            this.CreationID = (int)UserContact["CreationID "];
            this.CreationPath = (string)UserContact["CreationPath"];
            this.UserID = (int)UserContact["UserID"];
        }
    }
}
