using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTogatherWebUI
{
    public partial class JobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobsWS.JobsWS proxy = new JobsWS.JobsWS();
            NumOffers.Text = proxy.countOffers().ToString();
            if(Session["JobUser"] != null)
            {
                UserFName.Text = $"Hello {proxy.getUserFName((string)Session["JobUser"])},";
            }
            JobOffersGrid.DataSource = proxy.ReturnJobOffers();
            JobOffersGrid.DataBind();
        }

        protected void JobLogIn(object sender, EventArgs e)
        {
            JobsWS.JobsWS proxy = new JobsWS.JobsWS();
            bool logged = proxy.LogIn(UserName.Text, Password.Text);
            if(logged)
            {
                JobsWS.UserWS loggedUser = proxy.GetUser(UserName.Text);
                UserInfoText.InnerText = $"Username: {loggedUser.username} | First Name: {loggedUser.FirstName}";
                UserInfoText.Visible = true;
                JobPopSec1.Visible = false;
                JobPopSec2.Visible = true;
                jobErrorMsg.Visible = false;
                Session["JobUser"] = UserName.Text;
                Session["JobPass"] = Password.Text;
            }
            else
            {
                jobErrorMsg.Visible = true;
            }
        }

        protected void InsertJob(object sender, EventArgs e)
        {
            JobsWS.JobsWS proxy = new JobsWS.JobsWS();
            JobsWS.JobOffer offer = new JobsWS.JobOffer();
            offer.Phone = PhoneNumberText.Text;
            offer.Company = CompanyText.Text;
            offer.Position = Position.Text;
            bool inserted = proxy.AddJobOffer((string)Session["JobUser"], (string)Session["JobPass"], offer);
            if (inserted)
            {
                Response.Redirect("JobsPage.aspx");
            }
            else
            {
                jobErrorMsg2.Visible = true;
            }
        }

        protected void ClosePopUp(object sender, EventArgs e)
        {
            JobPopUp.Visible = false;
            Response.Redirect("JobsPage.aspx");
        }

        protected void OpenPopUp(object sender, EventArgs e)
        {
            JobPopUp.Visible = true;
            if (Session["JobUser"] != null)
            {
                JobsWS.JobsWS proxy = new JobsWS.JobsWS();
                JobsWS.UserWS loggedUser = proxy.GetUser((string)Session["JobUser"]);
                UserInfoText.InnerText = $"Username: {loggedUser.username} | First Name: {loggedUser.FirstName}";
                UserInfoText.Visible = true;
                JobPopSec1.Visible = false;
                JobPopSec2.Visible = true;
                jobErrorMsg.Visible = false;
            }
        }
    }
}