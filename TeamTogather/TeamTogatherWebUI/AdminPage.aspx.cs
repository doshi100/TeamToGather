using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BL;

namespace TeamTogatherWebUI
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime month_ago = DateTime.Now.AddDays(-31);
            LoggedInNum.Text = GeneralMethods.ReturnLoggedUsers(month_ago).ToString();
            Profession prof = new Profession(GeneralMethods.ReturnMostRequestedProfession(month_ago));
            popularProf.Text = prof.ProfName;
            List<Project> listProj = Project.ReturnTopProjects(month_ago);
            GridView1.DataSource = listProj;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowIndex > -1)
            {
                Project p = (Project)e.Row.DataItem;
                ((Label)e.Row.FindControl("ProjectID")).Text = p.ProjectID.ToString();
                ((Label)e.Row.FindControl("AdminUsID")).Text = p.AdminUSID.ToString();
                ((Label)e.Row.FindControl("MinAge")).Text = p.MinAge.ToString();
                ((Label)e.Row.FindControl("ProjectStatus")).Text = p.ProjectStatus.ToString();
                ((Label)e.Row.FindControl("NumRateVoters")).Text = p.NumRateVoters.ToString();
                ((Label)e.Row.FindControl("ProjectRate")).Text = p.ProjectRate.ToString();
                ((Label)e.Row.FindControl("DateCreated")).Text = p.DateCreated.ToString();
                ((HyperLink)e.Row.FindControl("AdminProfile")).NavigateUrl = $"profile.aspx?userid={p.AdminUSID.ToString()}&section=0";
                ((HyperLink)e.Row.FindControl("AdminProfile")).Text = "enter Profile";
                ((HyperLink)e.Row.FindControl("AdminProfile")).Target = "_blank";
            }
        }

        protected void UsersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                if((bool)row["IsBanned"])
                {
                    e.Row.Cells[11].Text = "true";
                }
                else
                {
                    e.Row.Cells[11].Text = "false";
                }
            }
        }

        protected void UserGridBind(object sender, EventArgs e)
        {
            BanUserH.Visible = true;
            BanUser.Visible = true;
            string username = Username.Text;
            string email = Email.Text;
            string country = Country.Text;
            string languages = lang.Text;
            DataTable listUsers = UserInfo.RetrieveUserTableByCredentials(username, email, country, languages);
            UsersGridView.DataSource = listUsers;
            UsersGridView.DataBind();
        }

        protected void BanUserByID(object sender, EventArgs e)
        {
            try
            {
                int userID = int.Parse(UserBanNum.Text);
                GeneralMethods.BanUser(userID);
                Response.Redirect("adminPage.aspx", true);
            }
            catch
            {
                Response.Redirect("adminPage.aspx", true);
            }
        }

        protected void UnBanUserByID(object sender, EventArgs e)
        {
            try
            {
                int userID = int.Parse(UserBanNum.Text);
                GeneralMethods.UnBanUser(userID);
                Response.Redirect("adminPage.aspx", true);
            }
            catch
            {
                Response.Redirect("adminPage.aspx", true);
            }
        }
    }
}