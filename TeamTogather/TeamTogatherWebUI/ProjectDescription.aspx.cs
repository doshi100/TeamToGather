using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using BL;

namespace TeamTogatherWebUI
{
    public partial class ProjectDescription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("HomePage.aspx", true);
            }
            if (Request.QueryString["ProjectID"] != "")
            {
                int projectID = int.Parse(Request.QueryString["ProjectID"]);
                Project current_project = new Project(projectID, false);
                ProjectContentContainer.InnerHtml = current_project.ProjectContent;
            }

        }
    }
}