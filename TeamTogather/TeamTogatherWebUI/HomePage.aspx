<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="TeamTogatherWebUI.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="backgroundsContainer" class="HomeSize">
        <div id="BlackBrightDiv">
            <div id="BrightDivElements">
                <div id="imageContainer">
                    <img src="DesignElements/logo/TeamTogatherLogo.png"></div>
                <span id="slogen">Perfect Ideas require perfect teams.</span>
                <a href="Registration.aspx" runat="server" id="SignUpButton">Sign Up</a>
            </div>
            <img src="DesignElements/Characters/WebSiteCharacterGreen.png" class="animatefloat" id="HPCharacter2" />
        </div>
        <div id="BlackDiv">
            <img src="DesignElements/Characters/purpleCharacterHomePage.png" class="animatefloat" alt="Character" id="HPCharacter">
            <div class="loginWrapper" id="loginWrapper" runat="server">
                <div id="loginContainer">
                    <span class="Black">Log In</span>
                    <ul id="loginList">
                        <li>
                            <asp:TextBox ID="UserNameBox" placeholder="Username" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox ID="PassBox" placeholder="Password" Type="password" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                    <asp:Button ID="LoginButton" CssClass="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" />
                    <asp:Label ID="LoginMessage" runat="server" CssClass="LoginMessage" Text="Label" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="UserSuggestions" id="UserSuggestions" runat="server" visible="false">
                <ul>
                    <li><a href="ProjectCreation.aspx"><span class="PinkUnderline">Create new project</span></a></li>
                    <li><asp:HyperLink runat="server" ID="UserStatusSugg"><span class="PinkUnderline">See your requests Statuses</span></asp:HyperLink></li>
                    <li><asp:HyperLink runat="server" ID="ManageProjectsSugg"><span class="PinkUnderline">Manage your Projects.</span></asp:HyperLink></li>
                </ul>
            </div>
        </div>
        <div id="BoundingLine"></div>
        <div class="pattern HomeSize"></div>
    </div>


</asp:Content>
