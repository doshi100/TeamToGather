using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class UserShowcase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("HomePage.aspx", true);
            }
            if (!IsPostBack)
            {
                Session["ShownUsers"] = null;
            }
            List<Profession> profList = Profession.GetProfessionList();
            DropDownProfFilter.DataTextField = "ProfName";
            DropDownProfFilter.DataValueField = "ProfessionID";
            DropDownProfFilter.DataSource = profList;
            int this_year = DateTime.Now.Year;
            DropDownAgeFilter.DataSource = Enumerable.Range(1, this_year-1940).ToList();
            DropDownLanguageFilter.DataSource = GeneralMethods.GetLang();
            DropDownLanguageFilter.DataTextField = "Value";
            DropDownLanguageFilter.DataValueField = "Key";
            DropDownWeeklyHoursFilter.DataSource = Enumerable.Range(1, 50).ToList();
            List<UserInfo> users = ReturnUsersDefaultList(0);
            UsersRepeater.DataSource = users;
            UsersRepeater.DataBind();
            if (!Page.IsPostBack)
            {
                if(UsersRepeater.HasControls())
                {
                    Label LastUserID = (Label)UsersRepeater.Controls[UsersRepeater.Items.Count - 1].FindControl("UserID");
                    if (LastUserID != null)
                    {
                        ShownUserIndex.Value = LastUserID.Text;
                    }
                }
                DropDownProfFilter.DataBind();
                DropDownProfFilter.Items.Insert(0, new ListItem("none", "-1"));
                DropDownAgeFilter.DataBind();
                DropDownAgeFilter.Items.Insert(0, new ListItem("none", "-1"));
                DropDownLanguageFilter.DataBind();
                DropDownLanguageFilter.Items.Insert(0, new ListItem("none", "-1"));
                DropDownWeeklyHoursFilter.DataBind();
                DropDownWeeklyHoursFilter.Items.Insert(0, new ListItem("none", "0"));

            }

        }

        protected void UsersRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                UserInfo user = (UserInfo)e.Item.DataItem;
                if(user.ProfilePath != "")
                {
                    ((Image)e.Item.FindControl("ProfilePicture")).ImageUrl = user.ProfilePath;
                }
                else
                {
                    ((Image)e.Item.FindControl("ProfilePicture")).ImageUrl = "DesignElements/elements/ProfilePicEmpty.png";
                }
                ((HyperLink)e.Item.FindControl("ProfileRedirecting")).NavigateUrl = $"profile.aspx?userid={user.ID}&section=0";
                ((Label)e.Item.FindControl("UserHeader")).Text = user.UserName;
                double rate = (double)user.UserRate / (double)user.NumRateVoters;
                if (double.IsNaN(rate))
                {
                    rate = 0;
                }

                ((Label)e.Item.FindControl("userRate")).Text += "" + Math.Round(rate) + "";
                ((Label)e.Item.FindControl("UserID")).Text += "" + user.ID + "";
            }

        }

        protected void ProfilePic_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Profile.aspx?UserID={ClickedUserID.Value}&section=0", true);
        }



        protected List<UserInfo> ReturnUsersDefaultList(int IndexUserID)
        {
            DateTime age = DateTime.Now;
            List<UserInfo> users = UserInfo.ShowUsers(-1, age, -1, 0, 0, IndexUserID);
            return users;
        }


        protected void SearchProject_Click(object sender, EventArgs e)
        {
            if (LoadMoreProjB.Visible == false)
            {
                LoadMoreProjB.Visible = true;
            }
            Session["ShownUsers"] = null;
            List<UserInfo> UsersShowcase;
            ShownUserIndex.Value = "0";
            if (DropDownFiltered.SelectedValue == "1")
            {
                UsersShowcase = ReturnUsersDefaultList(0);
            }
            else
            {
                int langFilter = int.Parse(DropDownLanguageFilter.SelectedValue);
                int profFilter = int.Parse(DropDownProfFilter.SelectedValue);
                int AgeFilter = int.Parse(DropDownAgeFilter.SelectedValue);
                int RateFilter = int.Parse(DropDownRateFilter.SelectedValue);
                int weeklyHours = int.Parse(DropDownWeeklyHoursFilter.SelectedValue);
                DateTime userChosen;
                if(AgeFilter == -1)
                {
                    userChosen = DateTime.Now;
                }
                else
                {
                    int years = 365 * AgeFilter;
                    TimeSpan t = new TimeSpan(years, 0, 0, 0);
                    userChosen = DateTime.Now - t;
                }
                UsersShowcase = null; // remove later ************ !!!!!!!!!! and replace with the one below
                UsersShowcase = UserInfo.ShowUsers(profFilter, userChosen, langFilter, weeklyHours, RateFilter, int.Parse(ShownUserIndex.Value));
            }
            List<UserInfo> sessionUsers;
            //if (session["ShownUsers"] UsersShowcase != null)
            //{
            //    sessionUsers = (List<UserInfo>)Session["ShownUsers"];
            //    sessionUsers.AddRange(UsersShowcase);
            //}
            //else
            //{
            Session["ShownUsers"] = UsersShowcase;
            //}
            sessionUsers = (List<UserInfo>)Session["ShownUsers"];
            UsersRepeater.DataSource = sessionUsers;
            UsersRepeater.DataBind();
            if (UsersShowcase.Count != 0)
            {
                Label LastProjID = (Label)UsersRepeater.Controls[UsersRepeater.Items.Count - 1].FindControl("UserID");
                if (LastProjID != null)
                {
                    ShownUserIndex.Value = LastProjID.Text;
                }
            }
            if (UsersShowcase.Count < 3)
            {
                LoadMoreProjB.Visible = false;
            }
        }

        static public void MergeRepeaters(Repeater a, Repeater b)
        {
            foreach (RepeaterItem item in b.Items)
            {
                a.Controls.Add(item);
            }
        }

        protected void LoadMore_Click(object sender, EventArgs e)
        {
            Repeater updateReapeter = new Repeater();
            updateReapeter.ItemTemplate = UsersRepeater.ItemTemplate;
            updateReapeter.ItemDataBound += new RepeaterItemEventHandler(UsersRepeater_OnItemDataBound);
            if (DropDownFiltered.SelectedValue == "1")
            {
                DateTime datetime = DateTime.Now;
                List<UserInfo> UsersShowcase = ReturnUsersDefaultList(int.Parse(ShownUserIndex.Value));
                List<UserInfo> sessionUsers;
                if (Session["ShownUsers"] != null)
                {
                    sessionUsers = (List<UserInfo>)Session["ShownUsers"];
                    sessionUsers.AddRange(UsersShowcase);
                }
                else
                {
                    Session["ShownUsers"] = UsersShowcase;
                }
                sessionUsers = (List<UserInfo>)Session["ShownUsers"];
                updateReapeter.DataSource = sessionUsers;
                updateReapeter.DataBind();
                if (UsersShowcase.Count != 0)
                {
                    Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("UserID");
                    if (LastProjID != null)
                    {
                        ShownUserIndex.Value = LastProjID.Text;
                    }
                }
                MergeRepeaters(UsersRepeater, updateReapeter);
                if (UsersShowcase.Count < 3)
                {
                    LoadMoreProjB.Visible = false;
                }

            }
            else
            {
                int langFilter = int.Parse(DropDownLanguageFilter.SelectedValue);
                int profFilter = int.Parse(DropDownProfFilter.SelectedValue);
                int AgeFilter = int.Parse(DropDownAgeFilter.SelectedValue);
                int RateFilter = int.Parse(DropDownRateFilter.SelectedValue);
                int weeklyHours = int.Parse(DropDownWeeklyHoursFilter.SelectedValue);
                DateTime userChosen;
                if (AgeFilter == -1)
                {
                    userChosen = DateTime.Now;
                }
                else
                {
                    int years = 365 * AgeFilter;
                    TimeSpan t = new TimeSpan(years, 0, 0, 0);
                    userChosen = DateTime.Now - t;
                }
                List<UserInfo> UsersShowcase = UserInfo.ShowUsers(profFilter, userChosen, langFilter, weeklyHours, RateFilter, int.Parse(ShownUserIndex.Value));
                List<UserInfo> sessionUsers;
                if (Session["ShownUsers"] != null)
                {
                    sessionUsers = (List<UserInfo>)Session["ShownUsers"];
                    sessionUsers.AddRange(UsersShowcase);
                }
                else
                {
                    Session["ShownUsers"] = UsersShowcase;
                }
                sessionUsers = (List<UserInfo>)Session["ShownUsers"];
                updateReapeter.DataSource = (List<UserInfo>)Session["ShownUsers"];
                updateReapeter.DataBind();
                if (UsersShowcase.Count < 3)
                {
                    LoadMoreProjB.Visible = false;
                }
                if (UsersShowcase.Count != 0)
                {
                    Label LastProjID = (Label)updateReapeter.Controls[updateReapeter.Items.Count - 1].FindControl("UserID");
                    if (LastProjID != null)
                    {
                        ShownUserIndex.Value = LastProjID.Text;
                    }
                }
                UsersRepeater.DataSource = null;
                UsersRepeater.DataBind();
                MergeRepeaters(UsersRepeater, updateReapeter);
            }
        }
    }
}