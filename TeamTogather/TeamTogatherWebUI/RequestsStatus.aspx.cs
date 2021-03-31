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
    public partial class RequestsStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (int)Session["UserID"] != int.Parse(Request.QueryString["UserID"]))
            {
                if (UserInfo.CheckAdmin((int)Session["UserID"]) == false)
                {
                    Response.Redirect("HomePage.aspx", true);
                }
            }
            DateTime date = DateTime.Now;
            DateTime WantedDate = date.AddDays(-60);
            if(Request.QueryString["section"] == "1")
            {
                requestsSection.Visible = true;
                InvitationLink1.NavigateUrl = $"RequestsStatus.aspx?UserID={Request.QueryString["userid"]}&section=2";
                ReqLink1.NavigateUrl = $"RequestsStatus.aspx?UserID={Request.QueryString["userid"]}&section=1";
                StatusReqRepeater.DataSource = UserInfo.returnSentUserReq(int.Parse(Request.QueryString["UserID"]), WantedDate);
                StatusReqRepeater.DataBind();
            }
            else if(Request.QueryString["section"] == "2")
            {
                invSections.Visible = true;
                InvitationLink.NavigateUrl = $"RequestsStatus.aspx?UserID={Request.QueryString["userid"]}&section=2";
                ReqLink.NavigateUrl = $"RequestsStatus.aspx?UserID={Request.QueryString["userid"]}&section=1";
                StatusInvRepeater.DataSource = UserInfo.returnSentUserInv(int.Parse(Request.QueryString["UserID"]), WantedDate);
                StatusInvRepeater.DataBind();
            }
        }

        protected void ReqBlock_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Project, Request> item = (KeyValuePair<Project, Request>)e.Item.DataItem;
                Image img = (Image)e.Item.FindControl("ProfileImage");
                img.AlternateText = item.Key.AdminUSID.ToString();
                string imagePath = UserInfo.ReturnUserProfilePath(item.Key.AdminUSID);
                if (imagePath != "")
                {
                    img.ImageUrl = imagePath;
                }
                else
                {
                    img.ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                }
                ((HyperLink)e.Item.FindControl("ProfileLink")).NavigateUrl = $"profile.aspx?UserID={item.Key.AdminUSID}&section=0";
                var projectText = new HtmlDocument();
                projectText.LoadHtml(item.Key.ProjectContent);
                string projectName;
                try
                {
                    projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                }
                catch
                {
                    projectName = "title";
                }
                ((Label)e.Item.FindControl("ReqProjHeader")).Text = $"\"{projectName}\"";
                ((HyperLink)e.Item.FindControl("redirectProjDesc")).NavigateUrl = $"ProjectDescription.aspx?ProjectID={item.Key.ProjectID}";
                Request req = item.Value;
                switch (req.requestStatus){
                    case 1:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Pending";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(175, 175, 175)");
                        break;
                    case 2:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Accepted";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(29, 237, 167)");
                        break;
                    case 3:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Rejected";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(226, 75, 75)");
                        break;
                    default:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Error";
                        break;
                }
                ((Label)e.Item.FindControl("ReqRole")).Text = req.ReqProfession.ProfName;
            }
        }


        protected void invBlock_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KeyValuePair<Project, Request> item = (KeyValuePair<Project, Request>)e.Item.DataItem;
                Image img = (Image)e.Item.FindControl("ProfileImage");
                img.AlternateText = item.Key.AdminUSID.ToString();
                string imagePath = UserInfo.ReturnUserProfilePath(item.Value.UserID);
                if (imagePath != "")
                {
                    img.ImageUrl = imagePath;
                }
                else
                {
                    img.ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                }
                ((HyperLink)e.Item.FindControl("ProfileLink")).NavigateUrl = $"profile.aspx?UserID={item.Value.UserID}&section=0";
                var projectText = new HtmlDocument();
                projectText.LoadHtml(item.Key.ProjectContent);
                string projectName;
                try
                {
                    projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                }
                catch
                {
                    projectName = "title";
                }
                ((Label)e.Item.FindControl("ReqProjHeader")).Text = $"\"{projectName}\"";
                ((HyperLink)e.Item.FindControl("redirectProjDesc")).NavigateUrl = $"ProjectDescription.aspx?ProjectID={item.Key.ProjectID}";
                Request req = item.Value;
                switch (req.requestStatus)
                {
                    case 1:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Pending";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(175, 175, 175)");
                        break;
                    case 2:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Accepted";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(29, 237, 167)");
                        break;
                    case 3:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Rejected";
                        ((Label)e.Item.FindControl("ReqStatus")).Style.Add("color", "rgb(226, 75, 75)");
                        break;
                    default:
                        ((Label)e.Item.FindControl("ReqStatus")).Text = "Error";
                        break;
                }
                ((Label)e.Item.FindControl("ReqRole")).Text = req.ReqProfession.ProfName;
            }
        }
    }
}