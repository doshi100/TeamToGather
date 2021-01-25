<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TeamTogatherWebUI.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ProfileHeaderContainer">
    </div>
    <div class="ProfileContentContainer">
        <div class="updateAreaProfileContainer">
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
