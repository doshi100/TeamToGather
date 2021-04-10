<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TeamTogatherWebUI.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: rgb(250,250,250);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="PostRate" ClientIDMode="Static" Value="" runat="server" />
    <asp:HiddenField ID="PostProjID" ClientIDMode="Static" Value="" runat="server" />
    <asp:HiddenField ID="JoinRequestID" ClientIDMode="Static" Value="" runat="server" />
    <asp:HiddenField ID="PostRequestID" ClientIDMode="Static" Value="0" runat="server" />
    <asp:HiddenField ID="PostPosID" ClientIDMode="Static" Value="0" runat="server" />
    <asp:HiddenField ID="PostUserID" ClientIDMode="Static" Value="0" runat="server" />
    <asp:HiddenField ID="GeneralPost" ClientIDMode="Static" Value="0" runat="server" />
    <asp:HiddenField ID="General" ClientIDMode="Static" Value="0" runat="server" />
    <asp:ScriptManager ID="ProjectShownScriptManager" runat="server"></asp:ScriptManager>
    <div class="ProfileHeaderContainer">


        <div id="profileImg_wrapper" class="profileImg_wrapper">
            <div class="profileImg_container">
                <asp:Image ID="ProfileImg" CssClass="profileImg" runat="server" />
            </div>
            <asp:Label ID="ProfileUsername" runat="server"></asp:Label>
            <span runat="server" visible="false" class="ChangeProfile_txt" id="ChangeProfile_txt">change profile photo</span>
        </div>
        <div class="profile_navbar">
            <ul>
                <li>
                    <asp:HyperLink ID="Protfolio_section" runat="server">Protfolio</asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="About_section" runat="server">About</asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="FinishedProj_section" runat="server">Finished Projects</asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="myProj_section" runat="server">Current Projects</asp:HyperLink></li>
                <li id="JoinReqLi_section" runat="server" visible="false">
                    <asp:HyperLink ID="JoinReq_section" runat="server">User Join Requests</asp:HyperLink></li>
                <li id="ProjInvLi_section" runat="server" visible="false">
                    <asp:HyperLink ID="ProjInv_section" runat="server">Project Invitation</asp:HyperLink></li>
                <li id="RequestsLi" runat="server" visible="false">
                    <asp:HyperLink ID="Requests" runat="server">user's Requests</asp:HyperLink></li>
            </ul>
        </div>
        <%-------------------------------------------------------------  START OF Invite User to project for a specific Role in the project SECTION------------------------------------------------%>
        <div runat="server" id="UserInfo_Section" class="UserRequests_section" visible="false">
            <div class="updateAreaProfileContainer">
                <asp:UpdatePanel ID="InvProjPositions" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="SendInvitation_container">
                            <div class="inviteUserDropContainer">
                                <input type="button" value="Invite User" id="sendProjectInv" onclick="addevent();" class="ButtonBlue openInvPanel" />
                                <asp:DropDownList ID="ProjectHeaders" CssClass="ProjectHeaders dropDown" AutoPostBack="true" OnSelectedIndexChanged="SetPositions" EnableViewState="true" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="profileProjectPositions_Container">
                                <div class="positions_list" id="positions_list" runat="server">
                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <div class="loadingSymbol_Child">
                                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="DesignElements/elements/Loading2.gif" class="loadingSymbolImg" AlternateText="Loading..." />
                                            </div>
                                            <div class="blurdrop" id="blurdrop" runat="server"></div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Repeater ID="ProjectInvRepeater" OnItemDataBound="ProjectInvRepeater_OnItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="PositionInputID" ClientIDMode="Static" runat="server" />
                                            <label id="InvPosItem" class="ProjectPosContainer" for="" runat="server">
                                                <asp:Label ID="posNumber" CssClass="" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="userNumber" CssClass="" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="ProjectManager" CssClass="ProjectManager_text" runat="server" Text="Head of the Project" Visible="false"></asp:Label>
                                                <ul class="ProjPositionList">
                                                    <li id="DeleteBContainer" class="DeleteBContainer">
                                                        <div id="DeleteButton" runat="server" class="DeletePosButton" onclick="DeletePos(this); return false;" visible="false">
                                                        </div>
                                                    </li>
                                                    <li id="profilePosPic" class="profilePosPic UserCredentials_ProfileContainer">
                                                        <asp:Image ID="ProfilePosPic" CssClass="profilePosPic" runat="server" />
                                                    </li>
                                                    <li id="PosTitle" class="PosTitle" runat="server"></li>
                                                    <li class="PosProgramsAreali">
                                                        <div id="PosPrograms" class="PosPrograms" runat="server"></div>
                                                    </li>
                                                </ul>
                                            </label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="PositionInvmessage" runat="server" CssClass="PositionInvmessage" Visible="false">There are no positions available for invitation in this project</asp:Label>
                                    <asp:Label ID="PositionAlreadySentMsg" runat="server" CssClass="PositionInvmessage" Visible="false">This invitation was already sent to the user</asp:Label>
                                    <asp:Button ID="SendAdminInvitation" OnClientClick="RequstPos2(this)" OnClick="AdminToUserRequest" runat="server" CssClass="ButtonBlue SendAdminInv" Text="Send" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <input type="button" class="popUpOpen ButtonBlue" id="popUpOpen" value="Upload Creation" runat="server" visible="false" />
        <input type="button" class="popUpRemoveCreation ButtonRed hiddenElm" id="popUpRemoveCreation" value="Remove Creation" runat="server" visible="false" />
        <%-------------------------------------------------------------  END OF Invite User to project for a specific Role in the project SECTION------------------------------------------------%>
    </div>

    <div id="ChangeProfile_popUp" class="confirmationPopUp2" runat="server" visible="false">
        <p><span class="Header">Are you sure You want to change this profile photo?</span></p>
        <div class="confirmationButtons_container">
            <input type="button" class="CancelCreationB ButtonRed" value="No" />
            <asp:Button ID="ChangeProfileB" runat="server" CssClass="ChangeProfileB ButtonBlue" Text="yes" OnClick="ChangeProfile_Click" />
            <asp:FileUpload ID="profileUploader" CssClass="profileUploader" runat="server" />
        </div>
    </div>

    <div id="confirmationPopUp" class="confirmationPopUp" runat="server" visible="false">
        <p><span class="Header">Are you sure you want to add this protfolio creation?</span></p>
        <div class="confirmationButtons_container">
            <input type="button" class="CancelCreationB ButtonRed" value="don't add the creation" />
            <asp:Button ID="AddCreationB" runat="server" CssClass="ButtonBlue" Text="Add the creation" OnClick="UplaodFile_Click" />
        </div>
    </div>

    <div id="DeleteCreationConfirmation" class="confirmationPopUpRemove" runat="server" visible="false">
        <p><span class="Header">Are you sure you want to remove this protfolio creation?</span></p>
        <div class="confirmationButtons_container">
            <input type="button" class="CancelCreationB ButtonRed" value="Don't Remove the Creation" />
            <asp:Button ID="Button1" runat="server" CssClass="ButtonBlue" Text="Remove the creation" OnClick="RemoveCreation" />
        </div>
    </div>

    <div class="viewPhoto_container">
        <img id="ViewPhoto" src="#" alt="" />
    </div>
    <div class="overlay" onclick="hideImg()"></div>
    <div class="ProfileContentContainer">
        <%-------------------------------------------------------------  START OF addProtfolioCreations Section ------------------------------------------------%>
        <div runat="server" id="ProtfolioSection" class="ProtfolioSection_section" visible="false">
            <div class="addCreation_popup">
                <asp:FileUpload ID="CreationUploader" CssClass="CreationUploader" runat="server" />
                <%--<input type="button" class="popUpOpen ButtonBlue" value="Upload" />--%>
                <%--</div>
            <div class="flexi">
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            <div class="creation_container"></div>
            </div>--%>
                <div class="flexi">
                    <asp:Repeater ID="CreationsRepeater" OnItemDataBound="CreationRepeater_OnItemDataBound" runat="server" EnableViewState="false">
                        <ItemTemplate>
                            <div id="creation_container" class="creation_container" onclick="viewPhoto(this)" runat="server">
                                <asp:Label ID="CreationID" CssClass="CreationID hiddenElm" runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <%-------------------------------------------------------------  END OF addProtfolioCreations Section  ------------------------------------------------%>
        <%--  search Project Requests Section  --%>
        <div runat="server" id="ProjectRequests_section" visible="false">
            <div class="updateAreaProfileContainer">
                <asp:UpdatePanel ID="UpdateShownProjects" class="ProjectsShowAreaContainer" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="ShownProjectsIndex" ClientIDMode="Static" Value="0" runat="server" />
                        <div class="ProjectsShowArea" id="ProjectsShowArea" runat="server">
                            <div class="ProjectsShowAreaInnerContainer">
                                <asp:Repeater ID="ProjectRepeater" OnItemDataBound="ProjectRepeater_OnItemDataBound" runat="server" EnableViewState="false">
                                    <ItemTemplate>
                                        <div id="ProjBox" class="ProjBox" runat="server">
                                            <div class="ProjHeaderContainer">
                                                <asp:Label ID="ProjectHeader" CssClass="projBox_ProjectHeader" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="ProjRateContainer">
                                                <asp:Label ID="ProjectRate" CssClass="projBox_ProjectRate" runat="server" Text="Project Rate : "></asp:Label>
                                            </div>
                                            <div class="ProjStatusContainer">
                                                <asp:Label ID="ProjectStatus" CssClass="projBox_ProjectStatus" runat="server" Text="Project Status : "></asp:Label>
                                            </div>
                                            <div class="ProjProfContainer">
                                                <asp:Label ID="ProjectProfPos" CssClass="projBox_ProjectStatus" runat="server" Text="Profession: "></asp:Label>
                                            </div>
                                            <asp:Label ID="ProjectPos" CssClass="projBox_ProjectPosID" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="RequestID" CssClass="projBox_RequestID" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="ProjectID" CssClass="projBox_ProjectID" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="DateRequested" CssClass="projBox_DateRequested" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="DateCreated" CssClass="projBox_DateCreated" runat="server" Text=""></asp:Label>
                                            <div class="ConfirmationBContainer" id="ConfirmationBContainer" runat="server">
                                                <asp:Button ID="ProjectDeclineButton" CssClass="ButtonRed" runat="server" OnClientClick="DeclineInvitation_Button(this);" Text="Decline" />
                                                <asp:Button ID="ProjectAcceptButton" CssClass="ButtonBlue" runat="server" OnClientClick="AcceptInvitation_Button(this);" Text="Accept" />
                                            </div>
                                            <%-- OnClientClick="GetProjectID_Button(this); event.cancelBubble=true;"--%>
                                            <%--OnClientClick="GetProjectID_Button(this); event.cancelBubble=true;"--%>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <asp:Button ID="LoadMoreProjB" CssClass="ButtonPink LoadPinkB" runat="server" Text="Load More" OnClick="LoadMore_Click" />


                        <asp:Button ID="OpenPopUp" CssClass="OpenPopUp" runat="server" Text="" type="button" OnClick="OpenPopUp_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="ClosePopUp" CssClass="ClosePopUp" runat="server" Text="" OnClick="ClosePopUp_Click" type="button" UseSubmitBehavior="false" />
                        <asp:Button ID="Accept" CssClass="AcceptInvitation" runat="server" Text="" OnClick="AcceptReq_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="Decline" CssClass="DeclineInvitation" runat="server" Text="" OnClick="DeclineReq_Click" UseSubmitBehavior="false" />
                        <div id="backDrop" class="backDrop" visible="false" runat="server">
                        </div>
                        <div id="PopUp" class="popUp" visible="false" runat="server">
                            <asp:LinkButton ID="DirectProject" CssClass="DirectProject" OnClick="DirectProject_Click" runat="server">
                                <div class="ContainerDirectionBox">
                                    <div class="ProjectDirectionBox contrast">
                                    </div>
                                    <div id="DirectionText" class="DirectionText" runat="server">
                                        Open
                                        <br>
                                        Description
                                    </div>
                                </div>
                            </asp:LinkButton>
                            <div class="positions_list">
                                <asp:Repeater ID="PositionsRepeater" OnItemDataBound="PosRepeater_OnItemDataBound" runat="server">
                                    <ItemTemplate>
                                        <div class="ProjectPosContainer">
                                            <asp:Label ID="ProjectManager" CssClass="ProjectManager_text" runat="server" Text="Head of the Project" Visible="false"></asp:Label>
                                            <ul class="ProjPositionList">
                                                <li id="DeleteBContainer" class="DeleteBContainer">
                                                    <div id="DeleteButton" runat="server" class="DeletePosButton" onclick="DeletePos(this); return false;" visible="false">
                                                    </div>
                                                </li>
                                                <li id="profilePosPic" class="profilePosPic UserCredentials_ProfileContainer">
                                                    <asp:ImageButton ID="ProfilePosPic" CssClass="profilePosPic" OnClick="ProfilePic_Click" runat="server" />
                                                </li>
                                                <li id="PosTitle" class="PosTitle" runat="server"></li>
                                                <li class="PosProgramsAreali">
                                                    <div id="PosPrograms" class="PosPrograms" runat="server"></div>
                                                </li>
                                                <li id="ReJoinButton" class="ReJoinButton" runat="server">
                                                    <asp:Button ID="SendJoinRe" CssClass="ButtonBlue" runat="server" Text="Apply" UseSubmitBehavior="false" OnClientClick="RequstPos(this); return false;" />
                                                    <asp:Button ID="RemoveUser" CssClass="ButtonRed" runat="server" Text="Remove user" UseSubmitBehavior="false" OnClientClick="RemoveUserFromPos(this); return false;" Visible="false" />
                                                </li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="PostRateContainer">
                                <asp:DropDownList ID="DropDownRateUpdate" CssClass="dropDown_sml UpdateRateDrop" runat="server">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="PublishRate_B" CssClass="ButtonBlue UpdateRateButton" runat="server" Text="Rate Project" UseSubmitBehavior="false" OnClientClick="PostUpdateRate(this); return false;" />
                            </div>
                            <div id="ShortSummaryDesc_wrap" class="ShortSummaryDesc_wrap" runat="server">
                            </div>
                        </div>
                        <asp:UpdateProgress ID="updateProgress" runat="server">
                            <ProgressTemplate>
                                <div class="loadingSymbol">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="DesignElements/elements/Loading2.gif" class="loadingSymbolImg" AlternateText="Loading..." />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdateButtons" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="SendJoinReq" CssClass="ButtonBlue SendRequestB" runat="server" OnClick="SendJoinRequest_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="UpdateRate" CssClass="ButtonBlue UpdateRate" runat="server" OnClick="UpdateRate_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="RemoveUser" CssClass="ButtonBlue RemoveUser" runat="server" OnClick="RemoveUserFromPos_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="RemovePosition" CssClass="ButtonBlue RemovePos" runat="server" OnClick="DeletePosFromProject_Click" UseSubmitBehavior="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--div here--%>

        <%--  END OF search Project Requests Section  --%>

        <%--  search User Requests Section  --%>
        <div runat="server" id="UserRequests_section" class="UserRequests_section" visible="false">
            <div class="updateAreaProfileContainer">
                <div class="ShowCaseProj_Container">
                    <asp:UpdatePanel ID="UpdatePanel1" class="ProjectsShowAreaContainer" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="ShownUserIndex" Value="0" runat="server" />
                            <asp:HiddenField ID="ClickedUserID" ClientIDMode="Static" Value="0" runat="server" />
                            <div class="ProjectsShowArea" id="UsersShowArea" runat="server">
                                <div class="ProjectsShowAreaInnerContainer">
                                    <asp:Repeater ID="UsersRepeater" OnItemDataBound="UsersRepeater_OnItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <div id="userBox" class="userBox" runat="server">
                                                <div class="userBox_UserCredentials">
                                                    <div class="UserCredentials_ProfileContainer">
                                                        <asp:ImageButton ID="ProfilePicture" OnClick="ProfilePic_Click" OnClientClick="UserProfileRedirection(this)" runat="server" UseSubmitBehavior="false" class="profilePosPic" />
                                                    </div>
                                                    <div class="UserHeaderContainer">
                                                        <asp:Label ID="UserHeader" CssClass="userBox_userHeader" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="UserProfessionContainer">
                                                    <asp:Label ID="userProfession" CssClass="userBox_ProjectRate" runat="server" Text="Position : "></asp:Label>
                                                </div>
                                                <div class="ProjectHeaderContainer">
                                                    <asp:Label ID="ProjectHeadertext" CssClass="userBox_ProjectRate" runat="server" Text="Project Title : "></asp:Label>
                                                </div>
                                                <div class="ConfirmationBContainer_Users">
                                                    <asp:Button ID="ProjectDeclineButton" CssClass="ButtonRed" OnClientClick="GetProjectID_Button(this)" OnClick="DeclineReq_Click" runat="server" Text="Decline" />
                                                    <asp:Button ID="ProjectAcceptButton" CssClass="ButtonBlue" OnClientClick="GetProjectID2_Button(this)" runat="server" OnClick="AcceptReq2_Click" Text="Accept" />
                                                </div>
                                                <asp:Label ID="UserID" CssClass="UserBox_UserID" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="ProjectPos" CssClass="projBox_ProjectPosID" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="RequestID" CssClass="projBox_RequestID" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="DateRequested" CssClass="projBox_DateRequested" runat="server" Text=""></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <asp:Button ID="LoadMoreUsers" CssClass="ButtonPink LoadPinkB" runat="server" Text="Load More" OnClick="LoadMoreUsers_Click" />
                            <asp:UpdateProgress ID="updateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="loadingSymbol">
                                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="DesignElements/elements/Loading2.gif" class="loadingSymbolImg" AlternateText="Loading..." />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%-------------------------------------------------------------  END OF search User Requests Section  ------------------------------------------------%>

        <%-------------------------------------------------------------  START OF addcontacts and show information Section  ------------------------------------------------%>
        <div runat="server" id="Contacts_section" class="Contacts_section" visible="false">
            <asp:HiddenField ID="CurrentRate" ClientIDMode="Static" Value="" runat="server" />
            <div class="rateSection" id="rateSection" runat="server" visible="true">
                <span class="header_rateuser">Rate User</span>
                <div class="rateBt_container">
                    <span class="btText">Beginner</span>
                    <asp:Button ID="Rate1" CssClass="userRateBu" OnClick="RateUser" runat="server" />
                    <asp:Button ID="Rate2" CssClass="userRateBu" OnClick="RateUser" runat="server" />
                    <asp:Button ID="Rate3" CssClass="userRateBu" OnClick="RateUser" runat="server" />
                    <asp:Button ID="Rate4" CssClass="userRateBu" OnClick="RateUser" runat="server" />
                    <asp:Button ID="Rate5" CssClass="userRateBu" OnClick="RateUser" runat="server" />
                    <span class="btText">Professional</span>
                </div>
            </div>
            <div class="data_container">
                <div class="generalInfo">
                    <div class="headercontainer"><span class="Header">About</span></div>
                    <div class="userinfo">
                        <asp:Label runat="server" ID="info_username" Text="Username: "></asp:Label>
                        <asp:Label runat="server" ID="info_age" Text="Age: "></asp:Label>
                        <asp:Label runat="server" ID="info_freehours" Text="Free hours in a week: "></asp:Label>
                        <asp:Label runat="server" ID="info_nativelang" Text="Native Language: "></asp:Label>
                        <asp:Label runat="server" ID="info_userrate" Text="User Rate: "></asp:Label>
                    </div>
                </div>
                <div class="professionInfo">
                    <div class="headercontainer"><span class="Header">User's Professions</span></div>
                    <div class="infoBox">
                        <asp:Repeater ID="professionRepeater" OnItemDataBound="ProfessionInfo_OnItemDataBound" runat="server">
                            <ItemTemplate>
                                <div class="infoElmContainer">
                                    <div class="infoImgContainer">
                                        <asp:Image ImageUrl="#" ID="infoImg" AlternateText="" runat="server" />
                                    </div>
                                    <asp:Label ID="InfoElmName" runat="server"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="contactInfo">
                    <div class="headercontainer">
                        <span class="Header">User's Contacts</span>
                        <div class="editimg_container" onclick="editContacts(this)" id="editimg_container" runat="server" visible="false">
                            <img src="DesignElements/elements/Pencil.png" alt="pencil">
                        </div>
                        <div class="addContact_popup">
                            <span class="Header2">Add Account's Website...</span><asp:DropDownList ID="WebsitesDropDown" CssClass="dropDownBlack dropDown" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="Contactname" placeholder="Account username" class="slimTextArea" runat="server"></asp:TextBox>
                            <asp:TextBox ID="ContactLink" placeholder="Account Link" class="slimTextArea" runat="server"></asp:TextBox>
                            <asp:Button ID="AddContactButton" CssClass="ButtonBlue" runat="server" Text="Add" OnClick="AddContact" />
                        </div>
                    </div>
                    <div class="infoBox">
                        <asp:Repeater ID="ContactRepeater" OnItemDataBound="ContactInfo_OnItemDataBound" runat="server">
                            <ItemTemplate>
                                <div class="infoElmContainer">
                                    <div class="infoImgContainer">
                                        <asp:Image ImageUrl="#" ID="infoImg" AlternateText="" runat="server" />
                                    </div>
                                    <a href="#" id="ContactInfoLink" target="_blank" runat="server">
                                        <asp:Label ID="InfoElmName" runat="server"></asp:Label></a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="programInfo">
                    <div class="headercontainer"><span class="Header">User's Knowledge</span></div>
                    <div class="infoBox">
                        <asp:Repeater ID="ProgramRepeater" OnItemDataBound="ProgramInfo_OnItemDataBound" runat="server">
                            <ItemTemplate>
                                <div class="infoElmContainer">
                                    <div class="infoImgContainer">
                                        <asp:Image ImageUrl="#" ID="infoImg" AlternateText="" runat="server" />
                                    </div>
                                    <asp:Label ID="InfoElmName" runat="server"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <%-------------------------------------------------------------  END addcontacts and show information Section  ------------------------------------------------%>
    </div>

</asp:Content>
