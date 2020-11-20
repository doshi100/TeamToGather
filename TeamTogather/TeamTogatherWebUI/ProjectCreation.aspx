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
    <script src="librarys/rangy-core.js"></script>
    <script src="librarys/rangy-classapplier.js"></script>
    <script src="librarys/undo.js"></script>
    <script src="librarys/medium.min.js"></script>
</asp:Content>
