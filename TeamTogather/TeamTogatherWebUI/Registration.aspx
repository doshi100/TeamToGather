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
            <ul class="unstyledlist regList">
                <li><label>Username</label>
                    <asp:TextBox ID="UserNameReg" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="UsernameValid" CssClass="gray" runat="server" ErrorMessage="The user name is unvalid, please type again" ValidationExpression="^([A-Za-z0-9]){3,10}$" ControlToValidate="UserNameReg"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="userNameExistValid" CssClass="gray" runat="server" ControlToValidate="UserNameReg" ErrorMessage="this user name was taken already, please type another one" OnServerValidate="UserExist_ServerValidate"></asp:CustomValidator>
                    <span class="floatLeft"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="gray" runat="server" ControlToValidate="UserNameReg" ErrorMessage="enter a username"></asp:RequiredFieldValidator></span>
                </li>


                <li class="graysml">- use only numbers and characters</li>
                <li class="graysml">- type at least 3 characters and less than 11</li>
                <li><label>Password </label>
                    <asp:TextBox ID="PassReg" Type="password" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="PassValid" CssClass="gray" runat="server" ErrorMessage="The password is unvalid, please type again" ControlToValidate="PassReg" ValidationExpression="^([A-Za-z0-9#*]){6,20}$"></asp:RegularExpressionValidator>

                </li>

                <li class="graysml">- type at least 6 characters, you can use the special characters # and *</li>
                <li><label>Confirm Password</label>
                    <asp:TextBox ID="ConfiPassReg" Type="password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordConfirm" CssClass="gray" runat="server" ErrorMessage="please type your password again" ControlToValidate="ConfiPassReg"></asp:RequiredFieldValidator>
                </li>
                <li><label>Email Address</label>
                     <asp:TextBox ID="EmailAddressReg" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="EmailValid" CssClass="gray" runat="server" ErrorMessage="The Email is unvalid, please type again" ControlToValidate="EmailAddressReg" ValidationExpression="^([a-zA-Z0-9.])+@\w{3,7}\.\w{2,3}(\.\w{2,3})?$"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  CssClass="gray" runat="server" ControlToValidate="EmailAddressReg" ErrorMessage="enter an email"></asp:RequiredFieldValidator>

                </li>

            </ul>
            <asp:Button ID="next" runat="server" Text="Next" OnClick="next_Click" />
        </div>
    </div>
    <%-- this  contains with user's native language, country origin--%>
    <div id="registrationP2" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <ul class="unstyledlist regList">
                <li>What is your native language?<asp:DropDownList ID="langDropDown" CssClass="dropDown" runat="server" ></asp:DropDownList>
                </li>
                <li>Where are you from?<asp:DropDownList ID="CountryDropDown" CssClass="dropDown" runat="server" ></asp:DropDownList>
                </li>
                <li>Birthday
                </li>
                <li class="graysml">
                    <label>day</label>
                    <label>month</label>
                    <label>year</label>
                <asp:DropDownList ID="DropDownDay"  CssClass="dropDown_sml" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="DropDownMonth" CssClass="dropDown_sml" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="DropDownYear" CssClass="dropDown_sml" runat="server"></asp:DropDownList>
                </li>
                <li>How many hours a week can you work on a collabrative Project? (1 hour to 50)</li>
                <li><asp:TextBox ID="WeekHours" runat="server"></asp:TextBox></li>
            </ul>
            <asp:Button ID="Button1" CssClass="Next_bt" runat="server" Text="Next" OnClick="next_Click" />
        </div>
    </div>
    <%-- this div contains user professions --%>
    <div id="registrationP3" class="blackTeko registration" runat="server" visible="false">
        <div class="grayDivRe">
            <div class="BoundingLBlue"></div>
            <img src="DesignElements/Characters/purpleCharacterHomePage.png">
        </div>
        <div class="WhiteDivRe">
            <span class="Header">Choose your Professions</span>
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
            <span class="Header">Choose The Programs/Programming Languages you know how to use</span>
            <div id="CheckboxProg" class="radios" runat="server" visible="false">
            </div>
            <asp:Button ID="Button3" runat="server" Text="next" OnClick="next_Click" />
        </div>
    </div>
    <div id="registrationP5" class="registrationFinal" runat="server" visible="false">
            <img src="DesignElements/Characters/happpypurpleCharacter.png" />
            <div class="BoundingLBlue2"></div>
            <span id="RegistrationMessage">Press Sign up And join the family !</span>
            <img src="DesignElements/logo/TeamTogatherLogo.png" alt="TeamTogatherlogo" class="logoImgS5" />
            <span class="smlText">Perfect ideas require Perfect teams.</span>
            <asp:Button ID="Button4" runat="server" Text="Sign up" OnClick="next_Click" />
    </div>

</asp:Content>
