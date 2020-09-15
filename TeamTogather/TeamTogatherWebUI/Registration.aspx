<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TeamTogatherWebUI.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- this block contains username, password+authintication, Email Address --%>
    <div id ="registrationP1" runat="server">
        <ul class="unstyledlist">
            <li>Username :<asp:TextBox ID="UserNameReg" runat="server"></asp:TextBox></li> <asp:RegularExpressionValidator ID="UsernameValid" runat="server" ErrorMessage="The user name is unvalid, please type again" ValidationExpression="^([A-Za-z0-9]){3,10}$" ControlToValidate="UserNameReg"></asp:RegularExpressionValidator>
            <li>- use only numbers and characters</li>
            <li>- type at least 3 characters and less than 11</li>
            <li>Password :<asp:TextBox ID="PassReg" Type="password" runat="server"></asp:TextBox></li> <asp:RegularExpressionValidator ID="PassValid" runat="server" ErrorMessage="The password is unvalid, please type again" ControlToValidate="PassReg" ValidationExpression="^([A-Za-z0-9#*]){6,20}$"></asp:RegularExpressionValidator>
            <li>- type at least 6 characters, you can use the special characters # and *</li>
            <li>confirm password :<asp:TextBox ID="ConfiPassReg" Type="password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordConfirm" runat="server" ErrorMessage="please type your password again" ControlToValidate="ConfiPassReg"></asp:RequiredFieldValidator>
            </li>
            <li>Email Address :<asp:TextBox ID="EmailAddressReg" runat="server"></asp:TextBox></li> <asp:RegularExpressionValidator ID="EmailValid" runat="server" ErrorMessage="The Email is unvalid, please type again" ControlToValidate="EmailAddressReg" ValidationExpression="^([a-zA-Z0-9.])+@\w{3,7}\.\w{2,3}(\.\w{2,3})?$"></asp:RegularExpressionValidator>
        </ul>
    </div>
    <%-- this  contains with user's native language, country origin--%> 
    <div id ="registrationP2" runat="server" visible="false"> 
        <ul class="unstyledlist">
            <li>
                Native language<asp:DropDownList ID="langDropDown" runat="server">
        </asp:DropDownList>
            </li>
        </ul>
    </div >
     <%-- this contains with the user profession, he must choose 1! --%>
    <div id ="registrationP3" runat="server" visible="false">
    </div>
     <%-- this contains with username skills at programs/programming --%>
    <div id ="registrationP4" runat="server" visible="false">
        registration4
    </div>
    <asp:Button ID="next" runat="server" Text="next" OnClick="next_Click" />
</asp:Content>
