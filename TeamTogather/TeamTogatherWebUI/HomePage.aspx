<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="TeamTogatherWebUI.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <span>Username: </span>
        <asp:TextBox ID="UserNameBox" runat="server"></asp:TextBox>
    </p>
    <span>Password: </span>
    <asp:TextBox ID="PassBox" runat="server"></asp:TextBox>
    <br/>
    <asp:Button ID="LoginButton" runat="server" Text="login" OnClick="LoginButton_Click" />
    <br />
    <asp:Label ID="LoginMessage" runat="server" Text="Label"></asp:Label>

</asp:Content>
