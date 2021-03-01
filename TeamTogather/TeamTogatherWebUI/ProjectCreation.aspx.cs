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
    public partial class ProjectCreation : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Session["UserID"] == null)
                {
                    Response.Redirect("HomePage.aspx", true);
                }
                Registration.BindProfessions(ProfContainer, Page);
                Registration.BindKnowledge(ProgContainer, Page);
                AgeDropDown.DataSource = Enumerable.Range(12, 19).ToList();
                AgeDropDown.DataBind();
                AgeDropDown.Items.Insert(0, new ListItem("none", "0"));
                UserInfo user = new UserInfo((int)Session["UserID"], true);
                PrimaryPositionDrop.DataTextField = "Value";
                PrimaryPositionDrop.DataValueField = "Key";
                PrimaryPositionDrop.DataSource = user.GetUserProfessions();
                PrimaryPositionDrop.DataBind();
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
                Project.AddProject((int)Session["UserID"], Age, ProjectStatusVal, ProjectContent,
                    ProjectAdminProf, positions);
                Response.Redirect("HomePage.aspx", true);
            }
        }

        public List<KeyValuePair<int, List<int>>> returnPositions(HtmlDocument html)
        {
            List<KeyValuePair<int, List<int>>> positionList = new List<KeyValuePair<int, List<int>>>();
            int counter = 0;
            while(counter < html.DocumentNode.ChildNodes.Count)
            {
                var node = html.DocumentNode.ChildNodes[counter];
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
                node = node.NextSibling;
                counter++;
            }
            return positionList;

        }
    }
}