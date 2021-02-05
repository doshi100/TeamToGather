﻿using System;
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
            if(Session["UserID"] == null)
            {
                Response.Redirect("HomePage.aspx", true);
            }
            if (!Page.IsPostBack)
            {
                Session["ShownProjects"] = null;
                Session["ShownUsers"] = null;
                if (Request.QueryString["section"] == "5")
                {
                    ProjectRequests_section.Visible = true;
                    UserInfo loggedinUser = new UserInfo(int.Parse(Request.QueryString["UserID"]), true);
                    Dictionary<Request, Project> projectsShowcase = loggedinUser.GetUserRequestsToProjects(DateTime.Now);
                    Session["ShownProjects"] = projectsShowcase;
                    ProjectRepeater.DataSource = projectsShowcase;
                    ProjectRepeater.DataBind();
                    try
                    {
                        Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("DateRequested");
                        if (LastProjID != null)
                        {
                            ShownProjectsIndex.Value = LastProjID.Text;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                else if (Request.QueryString["section"] == "4")
                {
                    UserRequests_section.Visible = true;
                    UserInfo loggedinUser = new UserInfo(int.Parse(Request.QueryString["UserID"]), true);
                    Dictionary<Request, string> UsersShowcase = loggedinUser.GetProjectsUserRequest(DateTime.Now);
                    UsersRepeater.DataSource = UsersShowcase;
                    UsersRepeater.DataBind(); try
                    {
                        Label LastProjID = (Label)UsersRepeater.Controls[UsersRepeater.Items.Count - 1].FindControl("DateRequested");
                        if (LastProjID != null)
                        {
                            ShownUserIndex.Value = LastProjID.Text;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                else if (Request.QueryString["section"] == "3")
                {
                    ProjectRequests_section.Visible = true;
                    UserInfo loggedinUser = new UserInfo(int.Parse(Request.QueryString["UserID"]), true);
                    List<Project> projectsShowcase = loggedinUser.GetUserDoneProjects(DateTime.Now);
                    Session["ShownProjects"] = projectsShowcase;
                    ProjectRepeater.DataSource = projectsShowcase;
                    ProjectRepeater.DataBind();
                    try
                    {
                        Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("DateRequested");
                        if (LastProjID != null)
                        {
                            ShownProjectsIndex.Value = LastProjID.Text;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                else if (Request.QueryString["section"] == "2")
                {
                    ProjectRequests_section.Visible = true;
                    UserInfo loggedinUser = new UserInfo(int.Parse(Request.QueryString["UserID"]), true);
                    List<Project> projectsShowcase = loggedinUser.ReturnUserProjects(DateTime.Now);
                    Session["ShownProjects"] = projectsShowcase;
                    ProjectRepeater.DataSource = projectsShowcase;
                    ProjectRepeater.DataBind();
                    try
                    {
                        Label LastProjID = (Label)ProjectRepeater.Controls[ProjectRepeater.Items.Count - 1].FindControl("DateCreated");
                        if (LastProjID != null)
                        {
                            ShownProjectsIndex.Value = LastProjID.Text;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }
            else
            {
                 ProjectRepeater.DataSource = Session["ShownProjects"];
                 UsersRepeater.DataSource = Session["ShownUsers"];
            }
        }


        protected void ProjectRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(Request.QueryString["section"] == "4")
                {
                    KeyValuePair<Request, Project> item = (KeyValuePair<Request, Project>)e.Item.DataItem;
                    Project project = item.Value;
                    Request req = item.Key;
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
                    ((Label)e.Item.FindControl("ProjectProfPos")).Text += "" + req.ReqProfession.ProfName + "";
                    ((Label)e.Item.FindControl("projectID")).Text += "" + project.ProjectID + "";
                    ((Label)e.Item.FindControl("DateRequested")).Text += "" + req.DateRequested + "";
                    ((Label)e.Item.FindControl("ProjectPos")).Text += "" + req.PositionID + "";
                    ((Label)e.Item.FindControl("RequestID")).Text += "" + req.RequestID + "";
                }

                else if (Request.QueryString["section"] == "3" || Request.QueryString["section"] == "2")
                {
                    Project project = (Project)e.Item.DataItem;
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
                    ((Label)e.Item.FindControl("DateCreated")).Text += "" + project.DateCreated + "";
                    ((Label)e.Item.FindControl("ProjectProfPos")).Visible = false;
                    ((Label)e.Item.FindControl("ProjectPos")).Visible = false;
                    ((HtmlGenericControl)e.Item.FindControl("ConfirmationBContainer")).Visible = false;
                }
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

        static public void AddMany(Dictionary<Request, string> dict, Dictionary<Request, string> toAdd)
        {
            foreach (KeyValuePair<Request, string> row in toAdd)
            {
                dict.Add(row.Key, row.Value);
            }
        }

        static public void AddMany(List<Project> list, List<Project> toAdd)
        {
            foreach (Project row in toAdd)
            {
                list.Add(row);
            }
        }


        protected void LoadMore_Click(object sender, EventArgs e)
        {
            UserInfo loggeduser = new UserInfo(int.Parse(Request.QueryString["UserID"]));
            if (Request.QueryString["section"] == "5")
            {
                Repeater updateReapeter = new Repeater();
                updateReapeter.ItemTemplate = ProjectRepeater.ItemTemplate;
                updateReapeter.ItemDataBound += new RepeaterItemEventHandler(ProjectRepeater_OnItemDataBound);
                try
                {
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
                    updateReapeter.DataBind();
                    if (projectsShowcase.Count != 0)
                    {
                        Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("ProjectID");
                        if (LastProjID != null)
                        {
                            ShownProjectsIndex.Value = LastProjID.Text;
                        }
                    }
                    if (projectsShowcase.Count < 10)
                    {
                        LoadMoreProjB.Visible = false;
                    }
                    MergeRepeaters(ProjectRepeater, updateReapeter);
                }
                catch
                {

                }
                //UpdateShownProjects.Update();
            }
            else if(Request.QueryString["section"] == "3" || Request.QueryString["section"] == "2")
            {
                Repeater updateReapeter = new Repeater();
                updateReapeter.ItemTemplate = ProjectRepeater.ItemTemplate;
                updateReapeter.ItemDataBound += new RepeaterItemEventHandler(ProjectRepeater_OnItemDataBound);
                try
                {
                    DateTime datetimevalue = Convert.ToDateTime(ShownProjectsIndex.Value);
                    List<Project> projectsShowcase = null ;
                    if (Request.QueryString["section"] == "3")
                    {
                        projectsShowcase = loggeduser.GetUserDoneProjects(datetimevalue);
                    }
                    else
                    {
                        projectsShowcase = loggeduser.ReturnUserProjects(datetimevalue);
                    }
                    List<Project> sessionProjects;
                    if (Session["ShownProjects"] != null)
                    {
                        sessionProjects = (List<Project>)Session["ShownProjects"];
                        AddMany(sessionProjects, projectsShowcase);
                        Session["ShownProjects"] = sessionProjects;
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
                        Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("DateCreated");
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
                catch
                {

                }
                //UpdateShownProjects.Update();
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
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                    ((ImageButton)e.Item.FindControl("ProfilePosPic")).AlternateText = PosUser.ID.ToString();
                }
                ((HtmlGenericControl)e.Item.FindControl("PosTitle")).InnerText = projectpos.profession.ProfName;
                HtmlGenericControl programsArea = ((HtmlGenericControl)e.Item.FindControl("PosPrograms"));
                if (projectpos.Programs != null)
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
                Label useridLabel = new Label();
                useridLabel.Text = projectpos.userID.ToString();
                useridLabel.CssClass = "userNumber";
                ((HtmlGenericControl)e.Item.FindControl("ReJoinButton")).Controls.Add(idLabel);
                ((HtmlGenericControl)e.Item.FindControl("ReJoinButton")).Controls.Add(useridLabel);
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
                if (PosUser.ID != 1 || !containsItem)
                {
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
                if (PosUser.ID != 1 && Request.QueryString["section"] == "2" && Request.QueryString["userid"] != (PosUser.ID).ToString() && Request.QueryString["userid"] == currentProject.AdminUSID.ToString())
                {
                    ((Button)e.Item.FindControl("RemoveUser")).Visible = true;
                }
                if (Request.QueryString["section"] == "2" && Request.QueryString["userid"] != (PosUser.ID).ToString() && Request.QueryString["userid"] == currentProject.AdminUSID.ToString())
                {
                    ((HtmlGenericControl)e.Item.FindControl("DeleteButton")).Visible = true;
                }
            }
        }




        // ********************** pop up methods from ProjectShowcase

        protected void OpenPopUp_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["section"] == "5")
            {
                PopUp.Visible = true;
                backDrop.Visible = true;
                Dictionary<Request ,Project> sessionProjects = (Dictionary<Request, Project>)Session["ShownProjects"];
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
            else if(Request.QueryString["section"] == "3" || Request.QueryString["section"] == "2")
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
        }

        protected void ClosePopUp_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["section"] == "5")
            {
                Dictionary<Request, Project> sessionProjects = (Dictionary<Request, Project>)Session["ShownProjects"];
                ProjectRepeater.DataSource = sessionProjects;
                ProjectRepeater.DataBind();
                PopUp.Visible = false;
                backDrop.Visible = false;
            }
            else if(Request.QueryString["section"] == "3" || Request.QueryString["section"] == "2")
            {
                List<Project> sessionProjects = (List<Project>)Session["ShownProjects"];
                ProjectRepeater.DataSource = sessionProjects;
                ProjectRepeater.DataBind();
                PopUp.Visible = false;
                backDrop.Visible = false;
            }
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
                bool execUpdateUsp = Project.updateUserRateAtProject(current_project.ProjectID, (int)Session["UserID"], userRate_Project);
                bool execUpdateStatus = Project.updateProjectRate(current_project.ProjectID, wholeRate, false);
            }

        }

        protected void RemoveUserFromPos_Click(object sender, EventArgs e)
        {
            try
            {
                string posID = PostPosID.Value;
                string userIDAtPos = PostUserID.Value; 
                if (posID != "" && userIDAtPos != "")
                {
                    Project.AddOrRemoveUserFromPos(1, int.Parse(posID));
                    Project.UpdateRequestStatusByPos(int.Parse(posID), int.Parse(userIDAtPos), 1);
                }
            }
            catch
            {
                Response.Redirect("HomePage.aspx", true);
            }
        }

        protected void ProfilePic_Click(object sender, EventArgs e)
        {
            ImageButton currentbutton = (ImageButton)sender;
            if (currentbutton.AlternateText != "1")
            {
                Response.Redirect($"Profile.aspx?UserID={currentbutton.AlternateText}", true);
            }
            else
            {
                ProjectRepeater.DataSource = Session["ShownProjects"];
                ProjectRepeater.DataBind();
            }
        }

        // *************************** end of popup methods ***************************


        protected void DeclineReq_Click(object sender, EventArgs e)
        {
            try
            {
                int reqID = int.Parse(PostRequestID.Value);
                bool succeed = Project.UpdateRequestStatus(3, reqID);
                Response.Redirect($"profile.aspx?userid={int.Parse(Request.QueryString["UserID"])}&section={int.Parse(Request.QueryString["section"])}");
            }
            catch
            {

            }
        }

        protected void AcceptReq_Click(object sender, EventArgs e)
        {
            try
            {
                int reqID = int.Parse(PostRequestID.Value);
                int posID = int.Parse(PostPosID.Value);
                Project.UpdateRequestStatus(2, reqID);
                Project.AddOrRemoveUserFromPos(int.Parse(Request.QueryString["UserID"]), posID);
                Response.Redirect($"profile.aspx?userid={int.Parse(Request.QueryString["UserID"])}&section={int.Parse(Request.QueryString["section"])}");
            }
            catch
            {

            }
        }

        protected void AcceptReq2_Click(object sender, EventArgs e)
        {
            try
            {
                int reqID = int.Parse(PostRequestID.Value);
                int posID = int.Parse(PostPosID.Value);
                int userID = int.Parse(ClickedUserID.Value);
                Project.UpdateRequestStatus(2, reqID);
                Project.AddOrRemoveUserFromPos(userID, posID);
                Response.Redirect($"profile.aspx?userid={int.Parse(Request.QueryString["UserID"])}&section={int.Parse(Request.QueryString["section"])}");
            }
            catch
            {

            }
        }



        // ********************************************************** userSectionMethods START *************************************************************

        protected void UsersRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Request, string> item = (KeyValuePair<Request, string>)e.Item.DataItem;
                Request req = item.Key;
                UserInfo user = new UserInfo(item.Key.UserID);
                if (user.ProfilePath != "" && user.ProfilePath != null)
                {
                    ((ImageButton)e.Item.FindControl("ProfilePicture")).ImageUrl = user.ProfilePath;
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("ProfilePicture")).ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                }
                ((Label)e.Item.FindControl("UserHeader")).Text = user.UserName;
                double rate = (double)user.UserRate / (double)user.NumRateVoters;
                if (double.IsNaN(rate))
                {
                    rate = 0;
                }
                var projectText = new HtmlDocument();
                projectText.LoadHtml(item.Value);
                string projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                ((Label)e.Item.FindControl("ProjectHeadertext")).Text += "" + projectName + "";
                ((Label)e.Item.FindControl("userProfession")).Text += "" + req.ReqProfession.ProfName + "";
                ((Label)e.Item.FindControl("UserID")).Text += "" + user.ID + "";
                ((Label)e.Item.FindControl("DateRequested")).Text += "" + req.DateRequested + "";
                ((Label)e.Item.FindControl("ProjectPos")).Text += "" + req.PositionID + "";
                ((Label)e.Item.FindControl("RequestID")).Text += "" + req.RequestID + "";
            }

        }


        protected void LoadMoreUsers_Click(object sender, EventArgs e)
        {
            UserInfo loggeduser = new UserInfo(int.Parse(Request.QueryString["UserID"]));
            Repeater updateReapeter = new Repeater();
            updateReapeter.ItemTemplate = UsersRepeater.ItemTemplate;
            updateReapeter.ItemDataBound += new RepeaterItemEventHandler(UsersRepeater_OnItemDataBound);
            DateTime datetimevalue = Convert.ToDateTime(ShownUserIndex.Value);
            Dictionary<Request, string> UsersShowcase = loggeduser.GetProjectsUserRequest(datetimevalue);
            Dictionary<Request, string> sessionUsers;
            if (Session["ShownUsers"] != null)
            {
                sessionUsers = (Dictionary<Request, string>)Session["ShownUsers"];
                AddMany(sessionUsers, UsersShowcase);
            }
            else
            {
                Session["ShownUsers"] = UsersShowcase;
            }
            sessionUsers = (Dictionary<Request, string>)Session["ShownUsers"];
            updateReapeter.DataSource = sessionUsers;
            updateReapeter.DataBind();
            if (UsersShowcase.Count != 0)
            {
                Label LastUserDate = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("DateRequested");
                if (LastUserDate != null)
                {
                    ShownUserIndex.Value = LastUserDate.Text;
                }
            }
            MergeRepeaters(UsersRepeater, updateReapeter);
            if (UsersShowcase.Count < 10)
            {
                LoadMoreUsers.Visible = false;
            }
            //UpdateShownUsers.Update();
        }


        // ********************************************************** userSectionMethods END *************************************************************

        // ********************************************************** Manage Projects START *************************************************************
        protected void DeletePosFromProject_Click(object sender, EventArgs e)
        {
            Project.DeletePos(int.Parse(PostPosID.Value));
            Project.neutralizePositionsRequests(int.Parse(PostPosID.Value));
        }


    }
}