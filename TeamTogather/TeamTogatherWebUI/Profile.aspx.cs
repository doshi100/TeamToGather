using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using HtmlAgilityPack;
using System.Web.UI.HtmlControls;


namespace TeamTogatherWebUI
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo loggedinUser = new UserInfo(7);
            Dictionary<Request, Project> projectsShowcase = loggedinUser.GetUserRequestsToProjects(DateTime.Now);
            Session["ShownProjects"] = projectsShowcase;
            ProjectRepeater.DataSource = projectsShowcase;
            ProjectRepeater.DataBind();
            //Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("ProjectID");
            //if (LastProjID != null)
            //{
            //    ShownProjectsIndex.Value = LastProjID.Text;
            //}
        }


        protected void ProjectRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Request, Project> item = (KeyValuePair<Request, Project>)e.Item.DataItem;
                Project project = item.Value;
                var projectText = new HtmlDocument();
                projectText.LoadHtml(project.ProjectContent);
                string projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                if (projectName != "")
                {
                    ((Label)e.Item.FindControl("ProjectHeader")).Text = projectName;
                }
                else
                {
                    ((Label)e.Item.FindControl("ProjectHeader")).Text = "title";
                }
                ((HtmlGenericControl)e.Item.FindControl("ProjBox")).Attributes.Add("onclick", "OpenPopUp(this);");
                double rate = (double)project.ProjectRate / (double)project.NumRateVoters;
                if (double.IsNaN(rate))
                {
                    rate = 0;
                }
                ((Label)e.Item.FindControl("ProjectRate")).Text += "" + Math.Round(rate) + "";
                switch (project.ProjectStatus)
                {
                    case 1:
                        ((Label)e.Item.FindControl("ProjectStatus")).Text += "Sketch";
                        break;
                    case 2:
                        ((Label)e.Item.FindControl("ProjectStatus")).Text += "In-Process";
                        break;
                    case 3:
                        ((Label)e.Item.FindControl("ProjectStatus")).Text += "Finished";
                        break;
                }
                ((Label)e.Item.FindControl("projectID")).Text += "" + project.ProjectID + "";
            }

        }


        static public void MergeRepeaters(Repeater a, Repeater b)
        {
            foreach (RepeaterItem item in b.Items)
            {
                a.Controls.Add(item);
            }
        }

        static public void AddMany(Dictionary<Request, Project> dict, Dictionary<Request, Project> toAdd)
        {
            foreach (KeyValuePair<Request, Project> row in toAdd)
            {
                dict.Add(row.Key, row.Value);
            }
        }

        protected void LoadMore_Click(object sender, EventArgs e)
        {
            UserInfo loggeduser = new UserInfo(7);
            Repeater updateReapeter = new Repeater();
            updateReapeter.ItemTemplate = ProjectRepeater.ItemTemplate;
            updateReapeter.ItemDataBound += new RepeaterItemEventHandler(ProjectRepeater_OnItemDataBound);
            DateTime datetimevalue = Convert.ToDateTime(ShownProjectsIndex.Value);
            Dictionary<Request, Project> projectsShowcase = loggeduser.GetUserRequestsToProjects(datetimevalue);
            Dictionary<Request, Project> sessionProjects;
                if (Session["ShownProjects"] != null)
                {
                    sessionProjects = (Dictionary<Request, Project>)Session["ShownProjects"];
                    AddMany(sessionProjects, projectsShowcase);
                }
                else
                {
                    Session["ShownProjects"] = projectsShowcase;
                }
                sessionProjects = (Dictionary<Request, Project>)Session["ShownProjects"];
                updateReapeter.DataSource = sessionProjects;
                updateReapeter.DataBind();
                if (projectsShowcase.Count != 0)
                {
                    Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("ProjectID");
                    if (LastProjID != null)
                    {
                        ShownProjectsIndex.Value = LastProjID.Text;
                    }
                }
                MergeRepeaters(ProjectRepeater, updateReapeter);
                if (projectsShowcase.Count < 10)
                {
                    LoadMoreProjB.Visible = false;
                }
            //UpdateShownProjects.Update();
        }

        

    }
}