<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="TeamTogatherWebUI.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin_container">
        <div class="SiteStatistics SearchUserSite">
            <span class="greenBg UlHeader">Search Users</span>
            <ul class="SearchUsersUl">
                <li>Username:
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox></li>
                <li>Email:
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox></li>
                <li>
                <li>Country:
                    <asp:TextBox ID="Country" runat="server"></asp:TextBox></li>
                <li>
                <li>Language:
                    <asp:TextBox ID="lang" runat="server"></asp:TextBox></li>
                <li>
                    <asp:Button ID="GetUsersB" CssClass="ButtonBlue" Text="Search Users" OnClick="UserGridBind" runat="server" /></li>
            </ul>
            <span id="BanUserH" class="greenBg UlHeader" runat="server" visible="false">Ban User By ID</span>
            <ul id="BanUser" visible="false" class="BanUserList" runat="server">
                <li>User ID:
                    <asp:TextBox ID="UserBanNum" runat="server"></asp:TextBox></li>
                <li>
                    <asp:Button ID="BanUserBu" CssClass="ButtonRed" Text="Ban User" OnClick="BanUserByID" runat="server" />
                </li>
                <li>
                    <asp:Button ID="UnBanUser" CssClass="ButtonBlue" Text="Release User Ban" OnClick="UnBanUserByID" runat="server" />
                </li>
            </ul>
            <asp:GridView ID="UsersGridView" CssClass="smltableText TopProjTable" OnRowDataBound="UsersGrid_RowDataBound" runat="server" AutoGenerateColumns="true">
                <Columns>
                    <asp:HyperLinkField
                        Text="enter Profile"
                        DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="Profile.aspx?userid={0}&section=0"
                        DataTextField="ID"
                        HeaderText="ProfileLink"
                        SortExpression="ID"
                        Target="_blank" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="PinkSepLine"></div>
        <div class="SiteStatistics">
            <ul>
                <li class="regular_textLI centerText"><span class="greenBg">Statistics</span></li>
                <li class="regular_textLI"><span>The number of people who logged in this month is: </span>
                    <asp:Label ID="LoggedInNum" CssClass="greenBg" runat="server"></asp:Label></li>
                <li class="regular_textLI"><span>The most popular Project Role Requested this month is: </span>
                    <asp:Label ID="popularProf" CssClass="greenBg" runat="server"></asp:Label></li>
                <li class="regular_textLI centerText">
                    <span class="greenBg">The Top rated Projects of this month</span>
                </li>
                <li>
                    <asp:GridView ID="GridView1" runat="server" CssClass="TopProjTable" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Project ID">
                                <ItemTemplate>
                                    <asp:Label ID="ProjectID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project's Admin ID">
                                <ItemTemplate>
                                    <asp:Label ID="AdminUsID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimum Age">
                                <ItemTemplate>
                                    <asp:Label ID="MinAge" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Status">
                                <ItemTemplate>
                                    <asp:Label ID="ProjectStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="number of raters">
                                <ItemTemplate>
                                    <asp:Label ID="NumRateVoters" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sum of rates">
                                <ItemTemplate>
                                    <asp:Label ID="ProjectRate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Creation Date">
                                <ItemTemplate>
                                    <asp:Label ID="DateCreated" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admin's Profile">
                                <ItemTemplate>
                                    <asp:HyperLink ID="AdminProfile" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </li>
            </ul>
        </div>
    </div>


</asp:Content>
