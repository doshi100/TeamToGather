<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="UserShowcase.aspx.cs" Inherits="TeamTogatherWebUI.UserShowcase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ShowCasePageContainer">
        <div class="ProjShowcaseHeaderArea">
            <div class="ProjShowcaseHeader">
                <span class="ProjHeader">Search Users</span>
                <span class="Projsub_header">Filter Users By:</span>
            </div>
            <div class="FilterBar FilterBar_users">
                <span class="FilterTextOption">Filter:</span>
                <asp:DropDownList ID="DropDownFiltered" CssClass="dropDown" runat="server">
                    <asp:ListItem Value="1">No</asp:ListItem>
                    <asp:ListItem Value="2">Yes</asp:ListItem>
                </asp:DropDownList>
                <span class="FilterTextOption">Profession:</span>
                <asp:DropDownList ID="DropDownProfFilter" CssClass="dropDown" EnableViewState="true" runat="server">
                </asp:DropDownList>
                <span class="FilterTextOption">Age:</span>
                <asp:DropDownList ID="DropDownAgeFilter" class="dropDown_sml" EnableViewState="true" runat="server">
                </asp:DropDownList>
                <span class="FilterTextOption">Rate</span>
                <asp:DropDownList ID="DropDownRateFilter" CssClass="dropDown" runat="server">
                    <asp:ListItem Value="0">all</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                </asp:DropDownList>
                <span class="FilterTextOption">Language</span>
                <asp:DropDownList ID="DropDownLanguageFilter" CssClass="dropDown" EnableViewState="true" runat="server">
                </asp:DropDownList>
                <span class="FilterTextOption">Free Weekly hours:</span>
                <asp:DropDownList ID="DropDownWeeklyHoursFilter" CssClass="dropDown" EnableViewState="true" runat="server">
                </asp:DropDownList>
                <asp:Button ID="SearchProject" CssClass="ButtonBlue" runat="server" Text="Apply Filter" OnClick="SearchProject_Click" />
            </div>
        </div>
        <div class="ShowCaseProj_Container">
            <asp:ScriptManager ID="ProjectShownScriptManager" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdateShownProjects" class="ProjectsShowAreaContainer" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="ShownUserIndex"  Value="0" runat="server" />
                    <asp:HiddenField ID="ClickedUserID" ClientIDMode="Static" Value="0" runat="server" />
                    <div class="ProjectsShowArea" id="UsersShowArea" runat="server">
                        <div class="ProjectsShowAreaInnerContainer">
                            <asp:Repeater ID="UsersRepeater" OnItemDataBound="UsersRepeater_OnItemDataBound" runat="server" EnableViewState="false">
                                <ItemTemplate>
                                    <div id="userBox" class="userBox" runat="server">
                                        <div class="userBox_UserCredentials">
                                            <div class="UserCredentials_ProfileContainer">
                                                <asp:ImageButton ID="ProfilePicture" OnClick="ProfilePic_Click" OnClientClick="UserProfileRedirection(this)" runat="server" UseSubmitBehavior="false"  class="profilePosPic" />
                                            </div>
                                            <div class="UserHeaderContainer">
                                                <asp:Label ID="UserHeader" CssClass="userBox_userHeader" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="ProjRateContainer">
                                            <asp:Label ID="userRate" CssClass="userBox_ProjectRate" runat="server" Text="User Rate : "></asp:Label>
                                        </div>
                                        <asp:Label ID="UserID" CssClass="UserBox_UserID" runat="server" Text=""></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <asp:Button ID="LoadMoreProjB" CssClass="ButtonPink LoadPinkB" runat="server" Text="Load More" OnClick="LoadMore_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
