using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BL;
using HtmlAgilityPack;

namespace TeamTogatherWebUI
{
    public partial class ProjectShowcase : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("HomePage.aspx", true);
                }
                string dateS = "04/11/2018 10:10:01";
                DateTime datetime = Convert.ToDateTime(dateS);
                List<Project> projectsShowcase = Project.ShowProjects((int)Session["UserID"], 0, datetime, 0, int.Parse(ShownProjectsIndex.Value));
                Session["ShownProjects"] = projectsShowcase;
                ProjectRepeater.DataSource = projectsShowcase;
                ProjectRepeater.DataBind();
                try
                {
                    Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("ProjectID");
                    if (LastProjID != null)
                    {
                        ShownProjectsIndex.Value = LastProjID.Text;
                    }
                }
                catch
                {
                    
                }
            }
            DropDownAgeFilter.DataSource = Enumerable.Range(12, 19).ToList();

            if (!Page.IsPostBack)
            {
                DropDownAgeFilter.DataBind();
                DropDownAgeFilter.Items.Insert(0, new ListItem("none", "0"));
            }
        }

        protected void ProjectClick_Click(object sender, EventArgs e)
        {
            PopUp.Visible = true;
        }

        protected void ProjectRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Project project = (Project)e.Item.DataItem;
                var projectText = new HtmlDocument();
                projectText.LoadHtml(project.ProjectContent);
                string projectName;
                try
                {
                    projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                }
                catch
                {
                    projectName = "";
                }
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
                switch(project.ProjectStatus)
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

        protected void PosRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProjectPos projectpos = (ProjectPos)e.Item.DataItem;
                UserInfo PosUser = new UserInfo(projectpos.userID, false, false);
                if (PosUser.ProfilePath != null)
                {
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).ImageUrl = PosUser.ProfilePath;
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).AlternateText = PosUser.ID.ToString();
                    if (PosUser.ID == 1)
                    {
                        ((ImageButton)e.Item.FindControl("ProfilePosPic")).Enabled = false;
                    }
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).AlternateText = PosUser.ID.ToString();
                }
                ((HtmlGenericControl)e.Item.FindControl("PosTitle")).InnerText = projectpos.profession.ProfName;
                HtmlGenericControl programsArea = ((HtmlGenericControl)e.Item.FindControl("PosPrograms"));
                if(projectpos.Programs != null)
                {
                    foreach (Knowledge program in projectpos.Programs)
                    {
                        Image programImage = new Image();
                        programImage.ImageUrl = program.ProgPath;
                        programsArea.Controls.Add(programImage);
                    } 
                }
                Label idLabel = new Label();
                idLabel.Text = projectpos.id.ToString();
                idLabel.CssClass = "posNumber";
                ((HtmlGenericControl)e.Item.FindControl("ReJoinButton")).Controls.Add(idLabel);
                // add OR and check if user has already requested this Position
                List<int> loggedUser_professions = null;
                try
                {
                    loggedUser_professions = UserInfo.GetUserProfessions((int)Session["UserID"]);
                }
                catch
                {
                    Response.Redirect("ProjectShowcase.aspx", true);
                }
                bool containsItem = loggedUser_professions.Contains(projectpos.profession.ProfessionID);
                if (PosUser.ID != 1 || !containsItem) {
                    ((Button)e.Item.FindControl("SendJoinRe")).Visible = false;
                }
                if (projectpos.CheckUserAtProjectPos((int)Session["UserID"]))
                {
                    ((Button)e.Item.FindControl("SendJoinRe")).Attributes.Add("disabled", "true");
                }
                Project currentProject = new Project(int.Parse(PostProjID.Value), false);
                if (currentProject.AdminUSID == projectpos.userID)
                {
                    ((Label)e.Item.FindControl("ProjectManager")).Visible = true;
                }
            }

        }

        protected void SearchProject_Click(object sender, EventArgs e)
        {
            if(LoadMoreProjB.Visible == false)
            {
                LoadMoreProjB.Visible = true;
            }
            Session["ShownProjects"] = null;
            List<Project> projectsShowcase;
            ShownProjectsIndex.Value = "0";
            if (DropDownFiltered.SelectedValue == "1")
            {
                string dateS = "04/11/2018 10:10:01"; 
                DateTime datetime = Convert.ToDateTime(dateS); //30
                projectsShowcase = Project.ShowProjects((int)Session["UserID"], 0, datetime, 0, int.Parse(ShownProjectsIndex.Value));
            }
            else
            {
                int AgeFilter = int.Parse(DropDownAgeFilter.SelectedValue);
                int RateFilter = int.Parse(DropDownRateFilter.SelectedValue);
                DateTime DateFilter = new DateTime();
                switch (int.Parse(DropDownDateFilter.SelectedValue))
                {
                    case 0:
                        string dateS = "04/11/2018 10:10:01";
                        DateFilter = Convert.ToDateTime(dateS);
                        break;

                    case 1:
                        DateFilter = DateTime.Today;
                        break;
                    case 2:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS = new TimeSpan(7, 0, 0, 0);
                        DateFilter = DateFilter - TimeS;
                        break;
                    case 3:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS2 = new TimeSpan(30, 0, 0, 0);
                        DateFilter = DateFilter - TimeS2;
                        break;
                    case 4:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS3 = new TimeSpan(365, 0, 0, 0);
                        DateFilter = DateFilter - TimeS3;
                        break;
                }
                projectsShowcase = Project.ShowProjects((int)Session["UserID"], AgeFilter, DateFilter, RateFilter, int.Parse(ShownProjectsIndex.Value));
            }
            List<Project> sessionProjects;
            //if (session["shownprojects"] projectsShowcase != null)
            //{
            //    sessionProjects = (List<Project>)Session["ShownProjects"];
            //    sessionProjects.AddRange(projectsShowcase);
            //}
            //else
            //{
            Session["ShownProjects"] = projectsShowcase;
            //}
            sessionProjects = (List<Project>)Session["ShownProjects"];
            ProjectRepeater.DataSource = sessionProjects;
            ProjectRepeater.DataBind();
            if (projectsShowcase.Count != 0)
            {
                Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("ProjectID");
                if (LastProjID != null)
                {
                    ShownProjectsIndex.Value = LastProjID.Text;
                }
            }
            if (projectsShowcase.Count < 10)
            {
                LoadMoreProjB.Visible = false;
            }
        }

        static public void MergeRepeaters(Repeater a, Repeater b)
        {
            foreach(RepeaterItem item in b.Items)
            {
                a.Controls.Add(item);
            }
        }

        protected void LoadMore_Click(object sender, EventArgs e)
        {
            Repeater updateReapeter = new Repeater();
            updateReapeter.ItemTemplate = ProjectRepeater.ItemTemplate;
            updateReapeter.ItemDataBound += new RepeaterItemEventHandler(ProjectRepeater_OnItemDataBound);
            if (DropDownFiltered.SelectedValue == "1")
            {
                string dateS = "04/11/2018 10:10:01";
                DateTime datetime = Convert.ToDateTime(dateS);
                List<Project> projectsShowcase = Project.ShowProjects((int)Session["UserID"], 0, datetime, 0, int.Parse(ShownProjectsIndex.Value));
                List<Project> sessionProjects;
                if (Session["ShownProjects"] != null)
                {
                    sessionProjects = (List<Project>)Session["ShownProjects"];
                    sessionProjects.AddRange(projectsShowcase);
                }
                else
                {
                    Session["ShownProjects"] = projectsShowcase;
                }
                sessionProjects = (List<Project>)Session["ShownProjects"];
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
                
            }
            else
            {
                int AgeFilter = int.Parse(DropDownAgeFilter.SelectedValue);
                int RateFilter = int.Parse(DropDownRateFilter.SelectedValue);
                DateTime DateFilter = new DateTime();
                switch (int.Parse(DropDownDateFilter.SelectedValue))
                {
                    case 0:
                        string dateS = "04/11/2018 10:10:01";
                        DateFilter = Convert.ToDateTime(dateS);
                        break;

                    case 1:
                        DateFilter = DateTime.Today;
                        break;
                    case 2:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS = new TimeSpan(7, 0, 0, 0);
                        DateFilter = DateFilter - TimeS;
                        break;
                    case 3:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS2 = new TimeSpan(30, 0, 0, 0);
                        DateFilter = DateFilter - TimeS2;
                        break;
                    case 4:
                        DateFilter = DateTime.Now;
                        TimeSpan TimeS3 = new TimeSpan(365, 0, 0, 0);
                        DateFilter = DateFilter - TimeS3;
                        break;
                }
                List<Project> projectsShowcase = Project.ShowProjects((int)Session["UserID"], AgeFilter, DateFilter, RateFilter, int.Parse(ShownProjectsIndex.Value));
                List<Project> sessionProjects;
                if (Session["ShownProjects"] != null)
                {
                    sessionProjects = (List<Project>)Session["ShownProjects"];
                    sessionProjects.AddRange(projectsShowcase);
                }
                else
                {
                    Session["ShownProjects"] = projectsShowcase;
                }
                sessionProjects = (List<Project>)Session["ShownProjects"];
                updateReapeter.DataSource = (List<Project>)Session["ShownProjects"];
                updateReapeter.DataBind();
                if(projectsShowcase.Count < 10)
                {
                    LoadMoreProjB.Visible = false;
                }
                if(projectsShowcase.Count != 0)
                {
                    Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("ProjectID");
                    if (LastProjID != null)
                    {
                        ShownProjectsIndex.Value = LastProjID.Text;
                    }
                }
                MergeRepeaters(ProjectRepeater, updateReapeter);
            }
            //UpdateShownProjects.Update();
        }

        protected void OpenPopUp_Click(object sender, EventArgs e)
        {
            PopUp.Visible = true;
            backDrop.Visible = true;
            List<Project> sessionProjects = (List<Project>)Session["ShownProjects"];
            ProjectRepeater.DataSource = sessionProjects;
            ProjectRepeater.DataBind();
            backDrop.Attributes.Add("onclick", "ClosePopUp();");
            Project ChosenProject = new Project(int.Parse(PostProjID.Value), true);
            var projectText = new HtmlDocument();
            projectText.LoadHtml(ChosenProject.ProjectContent);
            string projectShortDesc = projectText.DocumentNode.SelectSingleNode("//div[@class='SubHeader_container']").OuterHtml;
            projectShortDesc = projectShortDesc.Replace("contenteditable=\"true\"", "contenteditable=\"false\"");
            string projectShortDesc_string = projectShortDesc;
            ShortSummaryDesc_wrap.InnerHtml = projectShortDesc_string;
            PositionsRepeater.DataSource = ChosenProject.ProjectPositions;
            PositionsRepeater.DataBind();
        }

        protected void ClosePopUp_Click(object sender, EventArgs e)
        {
            List<Project> sessionProjects = (List<Project>)Session["ShownProjects"];
            ProjectRepeater.DataSource = sessionProjects;
            ProjectRepeater.DataBind();
            PopUp.Visible = false;
            backDrop.Visible = false;
        }

        protected void SendJoinRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string posID = JoinRequestID.Value;
                if (posID != "")
                {
                    UserInfo loggedUser = new UserInfo((int)Session["UserID"]);
                    loggedUser.LisrProf = loggedUser.GetUserProfessionsList2();
                    ProjectPos position = new ProjectPos(int.Parse(posID));
                    bool containsItem = loggedUser.LisrProf.Any(item => item.ProfessionID == position.profession.ProfessionID);
                    if (containsItem)
                    {
                        Project.AddProjectRequest(int.Parse(posID), (int)Session["UserID"], 1, 1);
                    }
                }
            }
            catch
            {
                Response.Redirect("HomePage.aspx", true);
            }
        }

        protected void DirectProject_Click(object sender, EventArgs e)
        {
            try
            {
                int projID = int.Parse(PostProjID.Value);
                Response.Redirect($"ProjectDescription.aspx?ProjectID={projID}", true);
            }
            catch
            {

            }
        }

        protected void UpdateRate_Click(object sender, EventArgs e)
        {
            int userRate_Project = int.Parse(PostRate.Value);
            Project current_project = new Project(int.Parse(PostProjID.Value), false);
            int SumRate = current_project.ProjectRate;
            int dbRate = Project.ReturnUserProjectRate(current_project.ProjectID, (int)Session["UserID"]);
            if (dbRate == -1) // if there isn't such a record, create a new one and update other tables.
            {
                int wholeRate = userRate_Project + SumRate;
                bool execInsertStatus = Project.InsertProjectRate_Rec(current_project.ProjectID, (int)Session["UserID"], userRate_Project);
                bool execUpdateStatus = Project.updateProjectRate(current_project.ProjectID, wholeRate, true);
            }
            else
            {
                int wholeRate = (userRate_Project - dbRate) + SumRate;
                bool execUpdateUsp = Project.updateUserRateAtProject(current_project.ProjectID , (int)Session["UserID"], userRate_Project);
                bool execUpdateStatus = Project.updateProjectRate(current_project.ProjectID, wholeRate, false);
            }

        }

        protected void ProfilePic_Click(object sender, EventArgs e)
        {
            ImageButton currentbutton = (ImageButton)sender;
            if(currentbutton.AlternateText != "1")
            {
                Response.Redirect($"Profile.aspx?UserID={currentbutton.AlternateText}&section=0", true);
            }
            else
            {
                ProjectRepeater.DataSource = Session["ShownProjects"];
                ProjectRepeater.DataBind();
            }
        }
    }
}