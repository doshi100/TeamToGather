using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BL
{
    public class UserContact
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string WebSiteName { get; set; }
        public string ContactLink { get; set; }
        public int WebsiteID { get; set; }
        public string WebSiteIDName { get; set; }
        public string SymbolPath { get; set; }

        /// <summary>
        /// no. 1 empty constructor
        /// </summary>
        public UserContact()
        {

        }

        /// <summary>
        /// no. 2 builds a user contact object by a DataRow
        /// </summary>
        public UserContact(DataRow UserContact)
        {
            this.ID = (int)UserContact["ID"];
            this.UserID = (int)UserContact["UserID"];
            this.WebSiteName = (string)UserContact["WebSiteName"];
            this.ContactLink = (string)UserContact["ContactLink"];
            this.WebsiteID = (int)UserContact["WebsiteID"];
            this.WebSiteIDName = (string)UserContact["WebsiteName"];
            this.SymbolPath = (string)UserContact["SymbolPath"];
        }
    }
}
