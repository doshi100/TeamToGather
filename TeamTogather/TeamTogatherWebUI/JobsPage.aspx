<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="JobsPage.aspx.cs" Inherits="TeamTogatherWebUI.JobsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: rgb(250,250,250);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ProxyManager" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="NumJobsOffer_container">
            <div class="NumOffObjects">
                <asp:Label ID="NumOffers" CssClass="NumOffers" runat="server"></asp:Label>
                <asp:Label ID="OffersSubTitle" CssClass="OffersSubTitle" runat="server">Job Offers Available</asp:Label>
            </div>
        </div>
        <asp:Label ID="UserFName" CssClass="FnameText" runat="server"></asp:Label><asp:Button ID="addJobOfferB" CssClass="ButtonBlue addOfferOpen" Text="Add Offer" OnClick="OpenPopUp" runat="server" />
        <asp:GridView ID="JobOffersGrid" CssClass="JobOffersGrid" AutoGenerateColumns="false" runat="server">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                <asp:BoundField DataField="Company" HeaderText="Company Name" />
                <asp:BoundField DataField="Position" HeaderText="Job Position" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:UpdatePanel ID="AddJob" runat="server">
        <ContentTemplate>
            <div id="JobPopUp" class="JobPopUp" runat="server" visible="false">
                <asp:LinkButton OnClick="ClosePopUp" runat="server" ID="exeIcon" on><img src="DesignElements/elements/ExeIcon.png" class="ExeIcon"/></asp:LinkButton>
                <span class="JobHeader2" id="UserInfoText" runat="server" visible="false"></span>
                <span ID="jobErrorMsg" class="jobErrorMsg" runat="server" visible="false">Login Failed ! try again</span>
                <span ID="jobErrorMsg2" class="jobErrorMsg" runat="server" visible="false">The Job has not been accepted, please try again.</span>
                <div id="JobPopSec1" class="JobPopSec1" runat="server">
                    <div>
                        <span class="JobHeader">Login</span>
                    </div>
                    <div>
                        Username:
                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        Password:
                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Button ID="LoginButton" Text="Login" class="ButtonBlue" OnClick="JobLogIn"  runat="server" />
                    </div>
                </div>
                <div id="JobPopSec2" class="JobPopSec1" runat="server" visible="false">
                    <div class="CreateJobOfferContainer">
                        <span class="JobHeader">Publish A new job Offer</span>
                        <div class="CreateJob">
                            <div>
                                Employer's Phone Number:
                            <asp:TextBox ID="PhoneNumberText" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                Employer's Company:
                            <asp:TextBox ID="CompanyText" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                Employee's Position:
                            <asp:TextBox ID="Position" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="publishJobB" CssClass="ButtonBlue" Text="Publish Job" OnClick="InsertJob" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
