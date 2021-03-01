using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using BL;

namespace TeamTogatherWebUI
{
    public partial class UpdateProject : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectID"] != "" && !Page.IsPostBack)
            {
                int projectID = int.Parse(Request.QueryString["ProjectID"]);
                Project current_project = new Project(projectID, false);
                UserAdmin.Value = current_project.AdminUSID.ToString();
                var projectText = new HtmlDocument();
                projectText.LoadHtml(current_project.ProjectContent);
                string projectName = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_header']").InnerText;
                string projectShortDesc = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_Summary']").InnerText;
                string projectContent = projectText.DocumentNode.SelectSingleNode("//div[@class='editor_Text Medium Medium-rich']").InnerHtml;
                editor_header.InnerHtml = projectName;
                editor_Summary.InnerHtml = projectShortDesc;
                editor_Text.InnerHtml = projectContent;
                if (Session["UserID"] == null)
                {
                    Response.Redirect("HomePage.aspx", true);
                }
                Registration.BindProfessions(ProfContainer, Page);
                Registration.BindKnowledge(ProgContainer, Page);
                AgeDropDown.DataSource = Enumerable.Range(12, 19).ToList();
                AgeDropDown.DataBind();
                AgeDropDown.Items.Insert(0, new ListItem("none", "0"));
                UserInfo user = new UserInfo(int.Parse(UserAdmin.Value), true);
                PrimaryPositionDrop.DataTextField = "Value";
                PrimaryPositionDrop.DataValueField = "Key";
                PrimaryPositionDrop.DataSource = user.GetUserProfessions();
                if (current_project.MinAge == 0)
                {
                    AgeDropDown.SelectedIndex = 0;
                }
                else
                {
                    AgeDropDown.SelectedIndex= AgeDropDown.Items.IndexOf(AgeDropDown.Items.FindByValue(current_project.MinAge.ToString()));
                }
                ProjectStatus.SelectedIndex = current_project.ProjectStatus - 1;
                PrimaryPositionDrop.DataBind();
                List<int> AdminCurrProf = Project.returnProjAdminProf(int.Parse(Request.QueryString["ProjectID"]), int.Parse(UserAdmin.Value));
                if (AdminCurrProf.Count > 0)
                {
                    foreach(int i in AdminCurrProf)
                    {
                        int index = PrimaryPositionDrop.Items.IndexOf(PrimaryPositionDrop.Items.FindByValue(i.ToString()));
                        if(index != -1)
                        {
                            ListItem adminProf = PrimaryPositionDrop.Items[index];
                            adminProf.Value = "0";
                            PrimaryPositionDrop.Items.RemoveAt(index);
                            PrimaryPositionDrop.Items.Add(adminProf);
                        }
                    }
                }
            }
            else
            {
                string PositionsListS = PositionsPostPassed.Value;
                HtmlDocument PositionsList = new HtmlDocument();
                PositionsList.LoadHtml(PositionsListS);
                List<KeyValuePair<int, List<int>>> positions = returnPositions(PositionsList);
                positions = positions.OrderBy(KeyValuePair => KeyValuePair.Key).ToList();
                string ProjectContent = EditorText.Text;
                ProjectContent = Regex.Replace(ProjectContent, "['’]", "&#39"); // santize the apostrophe(') for sql later on to reduce inconvenience
                int Age = int.Parse(AgeDropDown.SelectedValue);
                int ProjectStatusVal = int.Parse(ProjectStatus.SelectedValue);
                int ProjectAdminProf = int.Parse(PrimaryPositionDrop.SelectedValue);
                Project.UpdateProject(Age, ProjectStatusVal, ProjectContent, int.Parse(Request.QueryString["ProjectID"]), positions, int.Parse(UserAdmin.Value), ProjectAdminProf);
                Response.Redirect($"Profile.aspx?UserID={Session["UserID"]}&section=2", true);
            }
        }

        public List<KeyValuePair<int, List<int>>> returnPositions(HtmlDocument html)
        {
            List<KeyValuePair<int, List<int>>> positionList = new List<KeyValuePair<int, List<int>>>();
            int counter = 0;
            while (counter < html.DocumentNode.ChildNodes.Count)
            {
                var node = html.DocumentNode.ChildNodes[counter];
                if(int.Parse(node.FirstChild.InnerText) != 0)
                {
                    if (node.ChildNodes.Count == 1)
                    {
                        List<int> programList = new List<int>();
                        KeyValuePair<int, List<int>> position = new KeyValuePair<int, List<int>>(int.Parse(node.FirstChild.InnerText), programList);
                        positionList.Add(position);
                    }
                    else
                    {
                        List<int> programList = new List<int>();
                        foreach (var child in node.ChildNodes)
                        {
                            if (child != node.FirstChild)
                            {
                                programList.Add(int.Parse(child.InnerText));
                            }
                        }
                        KeyValuePair<int, List<int>> position = new KeyValuePair<int, List<int>>(int.Parse(node.FirstChild.InnerText), programList);
                        positionList.Add(position);
                    }
                }
                node = node.NextSibling;
                counter++;
            }
            return positionList;

        }
    }
}