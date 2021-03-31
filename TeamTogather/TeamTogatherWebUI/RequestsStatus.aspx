<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="RequestsStatus.aspx.cs" Inherits="TeamTogatherWebUI.RequestsStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: rgb(250,250,250);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="requestsSection" visible="false">
        <div class="RequestBlocksPageContainer">
            <div class="headerReqBlocks_container">
                <asp:Label runat="server" class="header">Requests Statuses</asp:Label>
                <asp:Label runat="server" class="subTextReq">Take a look at all of the positions requests you have sent to projects in the last 2 months. <br>
                * you can click on a project to review it again, and see if you have been accepted by the Project's admin. 
                </asp:Label>
                <div class="reqNavBar">
                    <asp:HyperLink runat="server" CssClass="reqSideBorder" ID="InvitationLink1"><div>See Invitations Statuses</div></asp:HyperLink>
                    <asp:HyperLink runat="server" ID="ReqLink1"><div>See Applied Requests Statuses</div></asp:HyperLink>
                </div>
            </div>
            <div class="ReqBlocks_container">
                <div class="RequestBlocknoRep RequestBlockRep">
                    <div class="ReqBlockGeneral">
                        <span>Project's admin</span>
                    </div>
                    <div class="ReqBlockGeneral">
                        <span>Project's Name</span>
                    </div>
                    <div class="ReqBlockGeneral">
                        <span>Status</span>
                    </div>
                    <div class="ReqBlockGeneral posAreaRequst">
                        <span>Position</span>
                    </div>
                </div>
                <asp:Repeater ID="StatusReqRepeater" OnItemDataBound="ReqBlock_OnItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="RequestBlockRep">
                            <div class="profileImg_container ReqBlockGeneral">
                                <asp:HyperLink ID="ProfileLink" Target="_blank" runat="server">
                                    <asp:Image ID="ProfileImage" class="profileImg" runat="server" />
                                </asp:HyperLink>
                            </div>
                            <asp:HyperLink ID="redirectProjDesc" CssClass="redirectProjDesc" Target="_blank" runat="server">
                                <div class="reqBlock_Proj ReqBlockGeneral">
                                    <asp:Label ID="ReqProjHeader" class="ReqProjHeader" runat="server"></asp:Label>
                                </div>
                            </asp:HyperLink>
                            <div class="reqStatus_container ReqBlockGeneral">
                                <asp:Label ID="ReqStatus" class="reqStatus" runat="server"></asp:Label>
                            </div>
                            <div class="reqRole_container ReqBlockGeneral">
                                <asp:Label ID="ReqRole" class="reqRole" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div runat="server" id="invSections" visible="false">
        <div class="RequestBlocksPageContainer">
            <div class="headerReqBlocks_container">
                <asp:Label runat="server" class="header">Invitations Statuses</asp:Label>
                <asp:Label runat="server" class="subTextReq">Take a look at all of the Invitations you have sent to users in the last 2 months. <br>
                * you can click on a project to review it again, and see if you have been accepted by the Project's admin. 
                </asp:Label>
                <div class="reqNavBar">
                    <asp:HyperLink runat="server" CssClass="reqSideBorder" ID="InvitationLink"><div>See Invitations Statuses</div></asp:HyperLink>
                    <asp:HyperLink runat="server" ID="ReqLink"><div>See Applied Requests Statuses</div></asp:HyperLink>
                </div>
            </div>
            <div class="ReqBlocks_container">
                <div class="RequestBlocknoRep RequestBlockRep">
                    <div class="ReqBlockGeneral">
                        <span>user's Profile</span>
                    </div>
                    <div class="ReqBlockGeneral">
                        <span>Project's Name</span>
                    </div>
                    <div class="ReqBlockGeneral">
                        <span>Status</span>
                    </div>
                    <div class="ReqBlockGeneral posAreaRequst">
                        <span>Position</span>
                    </div>
                </div>
                <asp:Repeater ID="StatusInvRepeater" OnItemDataBound="invBlock_OnItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="RequestBlockRep">
                            <div class="profileImg_container ReqBlockGeneral">
                                <asp:HyperLink ID="ProfileLink" Target="_blank" runat="server">
                                    <asp:Image ID="ProfileImage" class="profileImg" runat="server" />
                                </asp:HyperLink>
                            </div>
                            <asp:HyperLink ID="redirectProjDesc" CssClass="redirectProjDesc" Target="_blank" runat="server">
                                <div class="reqBlock_Proj ReqBlockGeneral">
                                    <asp:Label ID="ReqProjHeader" class="ReqProjHeader" runat="server"></asp:Label>
                                </div>
                            </asp:HyperLink>
                            <div class="reqStatus_container ReqBlockGeneral">
                                <asp:Label ID="ReqStatus" class="reqStatus" runat="server"></asp:Label>
                            </div>
                            <div class="reqRole_container ReqBlockGeneral">
                                <asp:Label ID="ReqRole" class="reqRole" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
