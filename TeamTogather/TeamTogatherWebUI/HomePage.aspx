<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="TeamTogatherWebUI.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="backgroundsContainer" class="HomeSize">
        <div id="BlackBrightDiv">
            <div id="BrightDivElements" >
                <div id="imageContainer"><img src="DesignElements/logo/TeamTogatherLogo.png"></div>
                <span id="slogen">Perfect Ideas require perfect teams.</span>
                <a href="Registration.aspx">Sign Up</a>
            </div>
            <img src="DesignElements/Characters/WebSiteCharacterGreen.png" id="HPCharacter2"/>
        </div>
        <div id="BlackDiv">
            <img src="DesignElements/Characters/purpleCharacterHomePage.png" alt="Character" id="HPCharacter">
            <div id="loginContainer">
                <span class="Black">Log In</span>
                <ul id="loginList">
                    <li>
                        <asp:TextBox ID="UserNameBox" placeholder="Username" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <asp:TextBox ID="PassBox" placeholder="Password" runat="server"></asp:TextBox>
                    </li>
                </ul>
                        <asp:Button ID="LoginButton" CssClass="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" />
                        <asp:Label ID="LoginMessage" runat="server" CssClass="LoginMessage" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
        <div id="BoundingLine"></div>
        <div class="pattern HomeSize" ></div>
    </div>


</asp:Content>
