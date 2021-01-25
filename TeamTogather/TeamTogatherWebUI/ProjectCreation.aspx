<%@ Page Title="" Language="C#" MasterPageFile="~/TeamTogatherMaster.Master" AutoEventWireup="true" CodeBehind="ProjectCreation.aspx.cs" Inherits="TeamTogatherWebUI.ProjectCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="medium.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="editor_section_contain">
        <div>
            <div class="BlackSection BlackBackground">
            </div>
        </div>
        <div class="White_Section_editor">
            <div class="Section_editor">
                <div class="toolBox BlackBackground">
                    <span class="MakeBold">B</span><span class="MakeHighlight">H</span><span class="MakeUnderLine">U</span><span class="MakeItalic">/</span>
                </div>
                <asp:TextBox ID="EditorText" EnableViewState="true" CssClass="EditorText" Style="display: none" runat="server"></asp:TextBox>
                <div class="editor">
                    <div class="editor_header" contenteditable="true" placeholder="Project's Name">
                    </div>
                    <div class="Markimg_wrapper">
                        <div class="Markimg_wrapper2">
                            <img src="DesignElements/professions/Other.png" class="HeaderImage" alt="questionMark">
                        </div>
                    </div>
                    <div class="SubHeader_container">
                        <div class="subHeader"><span>Short Description</span><span id="CharCountContainer"><span id="CharCount">0</span>/114</span></div>
                        <div class="editor_Summary" contenteditable="true"></div>
                        <div class="PsepContainer">
                            <div class="PsepLine"></div>
                        </div>
                    </div>
                    <%--<div class="editor_ContentWrap">--%>
                    <div class="editor_Content">
                        <div class="editor_Text" contenteditable="true">
                        </div>
                    </div>
                    <%--</div>--%>
                </div>

            </div>
            <div class="ScrollDown_text">
                <span>Scroll Down
                <br />
                    And Continue</span>
                <img src="DesignElements/elements/Arrow.png" alt="arrow" />
            </div>
        </div>
    </div>
    <%--part 2 of the page--%>
    <asp:HiddenField ID="PositionsPostPassed" Value="" runat="server" />
    <%--stores the Html list of projectPosition and their programs(before postback) for traversal in behind (after postback)--%>
    <div class="ProjCreSection2">
        <div class="PinkSepLine"></div>
        <div class="GraySection">
            <span class="Header">Choose Your's Project Elements</span>
            <div class="elementsFatherContainer">
                <div class="elementsContainer">
                    <div id="ProfContainer" class="ProfContainer" runat="server">
                    </div>
                    <div id="ProgContainer" class="ProgContainer" runat="server">
                    </div>
                </div>
            </div>
            <div class="buttonContainer">
                <div id="backArrow">
                    <img src="DesignElements/elements/Arrow.png" alt="arrow"></div>
                <div id="PositionButton" class="DivButton">Add</div>
                <div id="ProgramButton" class="DivButton">Add</div>
            </div>
        </div>
        <div class="PositionsShowContainer">
            <span class="Header">Project's Positions:</span>
            <div class="PositionsShow">
            </div>
        </div>
        <div id="project_fields">
            <span class="Header">Minimum Age Restriction</span>

            <asp:DropDownList class="AgeDropDown dropDown_sml" ID="AgeDropDown" runat="server">
            </asp:DropDownList>
            <span class="Header header2">Project Status</span>
            <asp:DropDownList class="ProjectStatus dropDown" ID="ProjectStatus" runat="server">
                <asp:ListItem Value="1">Sketch</asp:ListItem>
                <asp:ListItem Value="2">In-Process</asp:ListItem>
            </asp:DropDownList>
            <span class="Header header3">Choose your primary Position<br>
                <span class="smallText">*Do NOT include it in the list on the left</span>
            </span>
            <asp:DropDownList CssClass="PrimaryPosition dropDown" ID="PrimaryPositionDrop" runat="server">
            </asp:DropDownList>
            <asp:Button ID="CreateProjectB" CssClass="ButtonBlue CreateProjectB" runat="server" Text="Publish Project" />
        </div>
    </div>
    <script src="librarys/rangy-core.js"></script>
    <script src="librarys/rangy-classapplier.js"></script>
    <script src="librarys/undo.js"></script>
    <script src="librarys/medium.min.js"></script>
</asp:Content>
