<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TeamTogatherWebUI.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- this block contains username, password+authintication, Email Address --%>
    <div id="registrationP1" class="blackTeko registration" runat="server">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <ul class="unstyledlist">
                <li>Username <asp:TextBox ID="UserNameReg" runat="server"></asp:TextBox></li>
                <asp:RegularExpressionValidator ID="UsernameValid" runat="server" ErrorMessage="The user name is unvalid, please type again" ValidationExpression="^([A-Za-z0-9]){3,10}$" ControlToValidate="UserNameReg"></asp:RegularExpressionValidator>

                <asp:CustomValidator ID="userNameExistValid" runat="server" ControlToValidate="UserNameReg" ErrorMessage="this user name was taken already, please type another one" OnServerValidate="UserExist_ServerValidate"></asp:CustomValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserNameReg" ErrorMessage="enter a username"></asp:RequiredFieldValidator>

                <li>- use only numbers and characters</li>
                <li>- type at least 3 characters and less than 11</li>
                <li>Password <asp:TextBox ID="PassReg" Type="password" runat="server"></asp:TextBox></li>
                <asp:RegularExpressionValidator ID="PassValid" runat="server" ErrorMessage="The password is unvalid, please type again" ControlToValidate="PassReg" ValidationExpression="^([A-Za-z0-9#*]){6,20}$"></asp:RegularExpressionValidator>
                <li>- type at least 6 characters, you can use the special characters # and *</li>
                <li>confirm password <asp:TextBox ID="ConfiPassReg" Type="password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordConfirm" runat="server" ErrorMessage="please type your password again" ControlToValidate="ConfiPassReg"></asp:RequiredFieldValidator>
                </li>
                <li>Email Address <asp:TextBox ID="EmailAddressReg" runat="server"></asp:TextBox></li>
                <asp:RegularExpressionValidator ID="EmailValid" runat="server" ErrorMessage="The Email is unvalid, please type again" ControlToValidate="EmailAddressReg" ValidationExpression="^([a-zA-Z0-9.])+@\w{3,7}\.\w{2,3}(\.\w{2,3})?$"></asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EmailAddressReg" ErrorMessage="enter an email"></asp:RequiredFieldValidator>
            </ul>
            <ul class="ButtonsList">
            </ul>
            <asp:Button ID="next" runat="server" Text="next" OnClick="next_Click" />
        </div>
    </div>
    <%-- this  contains with user's native language, country origin--%>
    <div id="registrationP2" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <ul class="unstyledlist">
                <li>What is your native language?<asp:DropDownList ID="langDropDown" runat="server"></asp:DropDownList>
                </li>
                <li>Where are you from?<asp:DropDownList ID="CountryDropDown" runat="server"></asp:DropDownList>
                </li>
                <li>Birthday
                </li>
                <li>day:
                <asp:DropDownList ID="DropDownDay" runat="server"></asp:DropDownList>
                    month:
                <asp:DropDownList ID="DropDownMonth" runat="server"></asp:DropDownList>
                    year:
                <asp:DropDownList ID="DropDownYear" runat="server"></asp:DropDownList>
                </li>
                <li>How many hours a week can you work on a collabrative Project? (1 hour to 50)<asp:TextBox ID="WeekHours" runat="server"></asp:TextBox>
                </li>
            </ul>
            <asp:Button ID="Button1" runat="server" Text="next" OnClick="next_Click" />
        </div>
    </div>
    <%-- this div contains user professions --%>
    <div id="registrationP3" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <div id="CheckboxProf" class="radios" runat="server" visible="false">
            </div>
            <asp:Button ID="Button2" runat="server" Text="next" OnClick="next_Click" />
        </div>
    </div>
    <%-- this div contains username skills at programs/programming --%>
    <div id="registrationP4" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <div id="CheckboxProg" class="radios" runat="server" visible="false">
            </div>
            <asp:Button ID="Button3" runat="server" Text="next" OnClick="next_Click" />
        </div>
    </div>
    <div id="registrationP5" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <span id="RegistrationMessage" runat="server">Press Sign up in order to create your user ! </span>
        </div>
        <asp:Button ID="Button4" runat="server" Text="next" OnClick="next_Click" />
    </div>

</asp:Content>
