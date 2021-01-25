<%@ Page Title="" Language="C#" ClientIDMode="AutoID" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="ProjectShowcase.aspx.cs" Inherits="TeamTogatherWebUI.ProjectShowcase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ShowCasePageContainer">
        <div class="ProjShowcaseHeaderArea">
            <div class="ProjShowcaseHeader">
                <span class="ProjHeader">Search Projects</span>
                <span class="Projsub_header">Filter Project By:</span>
            </div>
            <div class="FilterBar">
                <span class="FilterTextOption">Filter:</span>
                <asp:DropDownList ID="DropDownFiltered" CssClass="dropDown" runat="server">
                    <asp:ListItem Value="1">No</asp:ListItem>
                    <asp:ListItem Value="2">Yes</asp:ListItem>
                </asp:DropDownList>
                <span class="FilterTextOption">Age Limit:</span>
                <asp:DropDownList ID="DropDownAgeFilter" class="dropDown_sml" EnableViewState="true" runat="server">
                </asp:DropDownList>
                <span class="FilterTextOption">Publish Date:</span>
                <asp:DropDownList ID="DropDownDateFilter" CssClass="dropDown" runat="server">
                    <asp:ListItem Value="0">all</asp:ListItem>
                    <asp:ListItem Value="1">Today</asp:ListItem>
                    <asp:ListItem Value="2">This Week</asp:ListItem>
                    <asp:ListItem Value="3">This Month</asp:ListItem>
                    <asp:ListItem Value="4">This Year</asp:ListItem>
                </asp:DropDownList>
                <span class="FilterTextOption">Rate:</span>
                <asp:DropDownList ID="DropDownRateFilter" CssClass="dropDown" runat="server">
                    <asp:ListItem Value="0">all</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="SearchProject" CssClass="ButtonBlue" runat="server" Text="Apply Filter" OnClick="SearchProject_Click" />
            </div>
        </div>
        <div class="ShowCaseProj_Container">
            <asp:HiddenField ID="PostRate" Value="" runat="server" />
            <asp:HiddenField ID="PostProjID" Value="" runat="server" />
            <asp:HiddenField ID="JoinRequestID" Value="" runat="server" />
            <asp:ScriptManager ID="ProjectShownScriptManager" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdateShownProjects" class="ProjectsShowAreaContainer" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="ShownProjectsIndex" Value="0" runat="server" />
                    <div class="ProjectsShowArea" id="ProjectsShowArea" runat="server">
                        <div class="ProjectsShowAreaInnerContainer">
                            <asp:Repeater ID="ProjectRepeater" OnItemDataBound="ProjectRepeater_OnItemDataBound" runat="server" EnableViewState="false">
                                <ItemTemplate>
                                    <div id="ProjBox" class="ProjBox" runat="server">
                                        <div class="ProjHeaderContainer">
                                            <asp:Label ID="ProjectHeader" CssClass="projBox_ProjectHeader" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="ProjRateContainer">
                                            <asp:Label ID="ProjectRate" CssClass="projBox_ProjectRate" runat="server" Text="Project Rate : "></asp:Label>
                                        </div>
                                        <div class="ProjStatusContainer">
                                            <asp:Label ID="ProjectStatus" CssClass="projBox_ProjectStatus" runat="server" Text="Project Status : "></asp:Label>
                                        </div>
                                        <asp:Label ID="ProjectID" CssClass="projBox_ProjectID" runat="server" Text=""></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <asp:Button ID="LoadMoreProjB" CssClass="ButtonPink LoadPinkB" runat="server" Text="Load More" OnClick="LoadMore_Click" />


                    <asp:Button ID="OpenPopUp" CssClass="OpenPopUp" runat="server" Text="" OnClick="OpenPopUp_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="ClosePopUp" CssClass="ClosePopUp" runat="server" Text="" OnClick="ClosePopUp_Click" UseSubmitBehavior="false" />
                    <div id="backDrop" class="backDrop" visible="false" runat="server">
                    </div>
                    <div id="PopUp" class="popUp" visible="false" runat="server">
                        <asp:LinkButton ID="DirectProject" CssClass="DirectProject" OnClick="DirectProject_Click" runat="server">
                            <div class="ContainerDirectionBox">
                            <div class="ProjectDirectionBox contrast">
                            </div>
                            <div class="DirectionText">Open </br> Description</div>
                            </div>
                        </asp:LinkButton>
                        <div class="positions_list">
                            <asp:Repeater ID="PositionsRepeater" OnItemDataBound="PosRepeater_OnItemDataBound" runat="server">
                                <ItemTemplate>
                                    <div class="ProjectPosContainer">
                                        <asp:Label ID="ProjectManager" CssClass="ProjectManager_text" runat="server" Text="Head of the Project" Visible="false"></asp:Label>
                                        <ul class="ProjPositionList">
                                            <li id="profilePosPic" class="profilePosPic UserCredentials_ProfileContainer">
                                                <asp:ImageButton ID="ProfilePosPic" CssClass="profilePosPic" OnClick="ProfilePic_Click" runat="server" />
                                            </li>
                                            <li id="PosTitle" class="PosTitle" runat="server"></li>
                                            <li class="PosProgramsAreali">
                                                <div id="PosPrograms" class="PosPrograms" runat="server"></div>
                                            </li>
                                            <li id="ReJoinButton" class="ReJoinButton" runat="server">
                                                <asp:Button ID="SendJoinRe" CssClass="ButtonBlue" runat="server" Text="Apply" UseSubmitBehavior="false" OnClientClick="RequstPos(this); return false;" />
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="PostRateContainer">
                            <asp:DropDownList ID="DropDownRateUpdate" CssClass="dropDown_sml UpdateRateDrop" runat="server">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="PublishRate_B" CssClass="ButtonBlue UpdateRateButton" runat="server" Text="Rate Project" UseSubmitBehavior="false" OnClientClick="PostUpdateRate(this); return false;" />
                        </div>
                        <div id="ShortSummaryDesc_wrap" class="ShortSummaryDesc_wrap" runat="server">
                        </div>
                    </div>
                    <asp:UpdateProgress ID="updateProgress" runat="server">
                        <ProgressTemplate>
                            <div class="loadingSymbol">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="DesignElements/elements/Loading2.gif" class="loadingSymbolImg" AlternateText="Loading..." />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdateButtons" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Button ID="SendJoinReq" CssClass="ButtonBlue SendRequestB" runat="server" OnClick="SendJoinRequest_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="UpdateRate" CssClass="ButtonBlue UpdateRate" runat="server" OnClick="UpdateRate_Click" UseSubmitBehavior="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
