<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="JobsPage.aspx.cs" Inherits="TeamTogatherWebUI.JobsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
                background-color: rgb(250,250,250);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="NumJobsOffer_container">
            <div class="NumOffObjects">
                <asp:Label ID="NumOffers" CssClass="NumOffers" runat="server"></asp:Label>
                <asp:Label ID="OffersSubTitle" CssClass="OffersSubTitle" runat="server">Offers Available</asp:Label>
            </div>
        </div>
        <asp:GridView ID="JobOffersGrid" CssClass="JobOffersGrid" AutoGenerateColumns="false" runat="server">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                <asp:BoundField DataField="Company" HeaderText="Company Name" />
                <asp:BoundField DataField="Position" HeaderText="Job Position" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
