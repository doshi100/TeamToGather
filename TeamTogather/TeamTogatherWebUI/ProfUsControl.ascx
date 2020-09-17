<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfUsControl.ascx.cs" Inherits="TeamTogatherWebUI.ProfUsControl" %>
<div class="ProfessionContainer">
    <div class ="ProfPhotoContainer" runat="server">
        <%--Profession image goes here--%>
        <img src="#" class="profImg" id="Profimg" runat="server" />
    </div>
    <span class="ProfName" id="nameContainer" runat="server">
        <%--Profession name goes here--%>
    </span>
</div>