using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class TeamTogatherMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] != null)
            {
                bool isAdmin = UserInfo.CheckAdmin((int)Session["UserID"]);
                navbar1.Visible = false;
                if(isAdmin)
                {
                    NavBarAdmin.Visible = true;
                    loginsmlMenuAdmin.Visible = true;
                }
                else
                {
                    navbar2.Visible = true;
                    LoginsmlMenuUser.Visible = true;
                }
            }
        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("HomePage.aspx");
        }
    }
}