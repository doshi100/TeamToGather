
let smallNav = document.querySelector(".loginsmlMenu");
let ProfilePhoto = document.querySelector(".profilePhoto");
let Placeholder = document.querySelector(".editor_header");
let scrollContainer = document.querySelector('.editor_Content'),
    scrollContent = document.querySelector('.editor_Text'),
    editor = document.querySelector('.editor'),
    CharCount = document.querySelector('#CharCount'),
    SummaryForm = document.querySelector('.editor_Summary'),
    contentPosition = 0,
    scrollerBeingDragged = false,
    scroller,
    topPosition,
    scrollerHeight;

let bold = document.querySelector(".MakeBold"),
    highlightB = document.querySelector(".MakeHighlight"),
    underLine = document.querySelector(".MakeUnderLine"),
    italic = document.querySelector(".MakeItalic"),
    Isbold = false,
    isHightlight = false,
    isUnderLine = false;

let elementsContainer = document.querySelector(".ProfContainer"),
    ProgContainer = document.querySelector('.ProgContainer'),
    PositionsShow = document.querySelector(".PositionsShow"),
    addPos_button = document.querySelector('#PositionButton'),
    addProg_button = document.querySelector('#ProgramButton'),
    arrowB = document.querySelector('#backArrow'),
    PositionsPassed = "",
    PositionsPostPassed = document.querySelector('#ContentPlaceHolder1_PositionsPostPassed'),
    CreateProjectB = document.querySelector('.CreateProjectB'),
    EditorText = document.querySelector('.EditorText'),
    PrimaryPosition = document.querySelector('.PrimaryPosition'),
    checkedElement;

let PopUpButton = document.querySelector(".OpenPopUp"),
    popUp = document.querySelector(".popUp"),
    hiddenPostID = document.getElementById('PostProjID'),
    BackDrop = document.querySelector(".backDrop"),
    closePopUpButton = document.querySelector(".ClosePopUp"),
    sendRequestB = document.querySelector(".SendRequestB"),
    UpdateRate = document.querySelector(".UpdateRate"),
    hiddenPostRate = document.querySelector("#PostRate"),
    hiddenPostPosID = document.querySelector("#PostPosID"),
    hiddenPostReqID = document.querySelector("#PostRequestID"),
    hiddenPostRequest = document.querySelector("#JoinRequestID"),
    hiddenPostUserID = document.querySelector("#PostUserID"),
    RemovePos = document.querySelector(".RemovePos"),
    RemoveUser = document.querySelector(".RemoveUser");

let Bu_openInvPanel = document.querySelector(".openInvPanel"),
    popUpOpen = document.querySelector(".popUpOpen");

let profile_pic = document.querySelector(".profileImg_container"),
    chgProfileTxt = document.querySelector(".ChangeProfile_txt"),
    RateButtons = document.querySelectorAll(".userRateBu");

if (RateButtons.length != 0) {
    RateButtons.forEach(item => { item.addEventListener("mouseover", () => mouseoverRate(item, RateButtons)) })
    let currentRate = document.getElementById("CurrentRate").value;
    for (let i = 0; i < currentRate; i++) {
        RateButtons[i].classList.add("userRateBuHover");
    }
}

if (chgProfileTxt) {
    chgProfileTxt.addEventListener('mouseover', () => { profile_pic.classList.add("profileImgHover_container") });
    chgProfileTxt.addEventListener('mouseout', () => { profile_pic.classList.remove("profileImgHover_container") });
    let confirmationPopUp = document.querySelector(".confirmationPopUp2"),
        CancelCreationB = confirmationPopUp.querySelector(".CancelCreationB"),
        CreationUploaderB = document.querySelector(".profileUploader");
    chgProfileTxt.addEventListener('click', () => clickButton(CreationUploaderB));
    CreationUploaderB.addEventListener('change', function () { if (CreationUploaderB.files[0]) { OpeningpopupMenuMechanism(confirmationPopUp, "block") } });
    CancelCreationB.addEventListener('click', () => ClosingpopupMenuMechanism(confirmationPopUp));
    //confirmationPopUpRemove.addEventListener('click', () => clickButton(CreationRemoverB2));
}

if (Bu_openInvPanel) {
    let ProjectHeaders = document.querySelector(".ProjectHeaders"),
        positionpanel = document.querySelector(".profileProjectPositions_Container"),
        inviteUserDropContainer = document.querySelector(".inviteUserDropContainer");
   popupMenuMechanism(positionpanel, "flex", ProjectHeaders, "rgb(45,45,45)", inviteUserDropContainer);
}



if (popUpOpen) {
    let confirmationPopUp = document.querySelector(".confirmationPopUp"),
        CancelCreationB = confirmationPopUp.querySelector(".CancelCreationB"),
        CreationUploaderB = document.querySelector(".CreationUploader"),
        confirmationPopUpRemove = document.querySelector(".confirmationPopUpRemove"),
        CancelCreationB2 = confirmationPopUpRemove.querySelector(".CancelCreationB"),
        CreationRemoverB2 = document.querySelector(".popUpRemoveCreation");
    popUpOpen.addEventListener('click', () => clickButton(CreationUploaderB));
    CreationUploaderB.addEventListener('change', function () { if (CreationUploaderB.files[0]) { OpeningpopupMenuMechanism(confirmationPopUp, "block") }});
    CancelCreationB.addEventListener('click', () => ClosingpopupMenuMechanism(confirmationPopUp));

    //confirmationPopUpRemove.addEventListener('click', () => clickButton(CreationRemoverB2));
    CreationRemoverB2.addEventListener('click', function () { OpeningpopupMenuMechanism(confirmationPopUpRemove, "block") });
    CancelCreationB2.addEventListener('click', () => ClosingpopupMenuMechanism(confirmationPopUpRemove));
}



if (scrollContainer != null) {
    createScroller();
    scrollContent.addEventListener('paste', detectPaste);
    scrollContent.addEventListener('keydown', ChangeScrollerHeight);
    scrollContent.addEventListener('scroll', moveScroller);
    SummaryForm.addEventListener('keydown', CheckCharLimit);
    SummaryForm.addEventListener('paste', detectPaste_Limit);
    var path = window.location.pathname;
    var page = path.split("/").pop();
    if (page == "ProjectCreation.aspx" || page == "Profile.aspx" || page == "UpdateProject.aspx") {
        addPos_button.addEventListener('click', CreatePositions)
        addProg_button.addEventListener('click', () => AddPrograms(checkedElement));
        arrowB.addEventListener('click', () => BackToPos());
        CreateProjectB.addEventListener('click', sendParameters);
        bold.addEventListener('mousedown', () => format4('b', "font-family: tekoSemiBold;"));
        highlightB.addEventListener('mousedown', () => format4('strong', "background-color: rgb(108, 241, 186); font-weight: 400"));
        underLine.addEventListener('mousedown', () => format4('u', ""));
        italic.addEventListener('mousedown', () => format4('i', ""));
        var medium = new Medium({
            element: scrollContent,
            mode: Medium.richMode,
            tags: null,
            attributes: null
        })

        scrollContent.highlight = function () {
            if (document.activeElement !== scrollContent) {
                medium.select();
            }
        };

        function format4(formatTag, elm_style) {
            scrollContent.highlight();
            medium.invokeElement(formatTag, {
                style: elm_style
            });
            return false;
        }
    }
    else if (page == "ProjectDescription.aspx") {
        let scroller2 = document.getElementsByClassName("scroller")[0];
        scroller2.parentNode.removeChild(scroller2);
        SummaryForm.setAttribute("contenteditable", "false");
        scrollContent.setAttribute("contenteditable", "false");
        Placeholder.setAttribute("contenteditable", "false");

    }
}


if (Placeholder != null && window.location.pathname.split("/").pop() == "ProjectCreation.aspx") {
    window.addEventListener("load", function () {
        Placeholder.innerHTML = "";
        Placeholder.focus();
    });
    Placeholder.addEventListener('keydown', CheckCharLimitH);
    Placeholder.addEventListener('paste', detectPaste_LimitH);
}
if (smallNav != null && ProfilePhoto != null) {
    window.addEventListener("load", function () {
        smallNav.style.display = "none";
    })


    ProfilePhoto.addEventListener("click", function () {
        if (smallNav.style.display == "none") {
            smallNav.style.display = "flex"
        }
        else {
            smallNav.style.display = "none";
        }
    })
}

function mouseoverRate(e, array) {
    removeMarks(array);
    let element = e;
    while (element.tagName == "INPUT") {
        element.classList.add("userRateBuHover");
        element = element.previousElementSibling;
    }
}

function removeMarks(array) {
    array.forEach(item => { item.classList.remove("userRateBuHover") });
}



function ListContains(list, valueToAdd) {
    for (let item of list.children) {
        if (item.innerText === valueToAdd && item !== list.firstElementChild) {
            return true;
        }
    }
    return false;
}

function AddPrograms(PositionElement) {
    let GetCheckBoxes = ProgContainer.getElementsByTagName("input");
    for (let element of GetCheckBoxes) {
        if (element.checked === true) {
            let ProgVal = element.value;
            AddProgram(element.nextSibling.getElementsByTagName('img')[0], ProgVal, PositionElement)
            element.checked = false;
        }
    }
    ProgContainer.style.display = 'none';
    elementsContainer.style.display = 'block';
    addPos_button.style.display = 'block';
    addProg_button.style.display = 'none';
    arrowB.style.display = 'none';
}

function BackToPos() {
    ProgContainer.style.display = 'none';
    elementsContainer.style.display = 'block';
    addPos_button.style.display = 'block';
    addProg_button.style.display = 'none';
    arrowB.style.display = 'none';
}

function ChangeCheckedElement(PositionElement) {
    ProgContainer.style.display = 'block';
    elementsContainer.style.display = 'none';
    addPos_button.style.display = 'none';
    addProg_button.style.display = 'block';
    arrowB.style.display = 'block';
    checkedElement = PositionElement;
}

function AddProgram(positionPhoto, progVal, positionElement) {
    if (!(ListContains(positionElement.getElementsByTagName('ul')[0], progVal))) {
        let listItem = document.createElement('li');
        listItem.innerText = progVal;
        positionElement.getElementsByTagName('ul')[0].append(listItem);
        positionPhoto = positionPhoto.cloneNode(true);
        positionElement.querySelector('.ProgramsContainer').append(positionPhoto);
    }
}

function CreatePositions() {
    let GetCheckBoxes = elementsContainer.getElementsByTagName("input");
    for (let element of GetCheckBoxes) {
        if (element.checked === true) {
            checkedElement = CreatePosition(element, element.nextSibling)
            element.checked = false;
        }
    }
    elementsContainer.style.display = 'none';
    ProgContainer.style.display = 'block';
    addPos_button.style.display = 'none';
    addProg_button.style.display = 'block';
    arrowB.style.display = 'block';
}

function CreatePosition(elementInput, elementLabel) {
    let PositionElement = document.createElement('div');
    PositionElement.className = 'PositionElement';
    let ProfPosition = document.createElement('div');
    ProfPosition.className = 'ProfPosition';
    let PositionList = document.createElement('ul'); // a list that saves positions and programs 
    PositionList.style.display = 'none';
    let ProfIDLItem = document.createElement('li');
    ProfIDLItem.innerText = elementInput.value;
    PositionList.appendChild(ProfIDLItem);
    PositionElement.appendChild(ProfPosition);
    PositionElement.appendChild(PositionList);
    elementInput = elementInput.cloneNode(true)
    elementLabel = elementLabel.cloneNode(true)
    elementInput.id = elementInput.id + RandomString();
    elementLabel.setAttribute('for', elementInput.id);
    elementInput.type = 'radio';
    elementInput.name = 'Professions';
    let ProgramsFaContainer = document.createElement('div');
    ProgramsFaContainer.className = 'ProgramsFatherContainer';
    let ProgramsContainer = document.createElement('div');
    ProgramsContainer.className = 'ProgramsContainer';
    ProgramsFaContainer.append(ProgramsContainer);
    PositionElement.append(ProgramsFaContainer);
    ProfPosition.appendChild(elementInput);
    ProfPosition.appendChild(elementLabel);
    PositionsShow.appendChild(PositionElement);
    let DeleteB = document.createElement('div');
    DeleteB.className = 'DeletePosB';
    PositionElement.append(DeleteB);
    DeleteB.addEventListener('click', () => deletePosElement(PositionElement));
    PositionElement.querySelector('label').addEventListener('click', () => ChangeCheckedElement(PositionElement));
    return PositionElement;
}

function deletePosElement(PositionElement) {
    PositionElement.parentNode.removeChild(PositionElement);
    ProgContainer.style.display = 'none';
    elementsContainer.style.display = 'block';
    addPos_button.style.display = 'block';
    addProg_button.style.display = 'none';
    arrowB.style.display = 'none';
}

function RandomString() {
    let chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    // Pick characers randomly
    let str = '';
    length = 8;
    for (let i = 0; i < length; i++) {
        str += chars.charAt(Math.floor(Math.random() * chars.length));
    }

    return str;
}

function CheckCharLimit(evt) {
    let key = evt.key;
    if ((SummaryForm.textContent.length >= 114 || (key === "Enter")) && (key !== "Backspace" && key !== "ArrowDown" && key !== "ArrowUp" && key !== "ArrowLeft" && key !== "ArrowRight")) {
        evt.preventDefault();
    }
    else {
        SummaryForm.addEventListener('keyup', CountChars);
    }
}

function CountChars(evt) {
    CharCount.innerHTML = SummaryForm.textContent.length;
}

function detectPaste_Limit(evt) {
    evt.preventDefault()
    let text = evt.clipboardData.getData('text/plain')
    document.execCommand('insertText', true, text);
    text = SummaryForm.innerHTML;
    if (text.length >= 114) {
        text = text.slice(0, 114);
        text = text.replace(/(<br>)|(<\/br>)/g, '');
        SummaryForm.textContent = text;
    }
}

function detectPaste_LimitH(evt) {
    evt.preventDefault()
    let text = evt.clipboardData.getData('text/plain')
    document.execCommand('insertText', true, text);
    text = Placeholder.innerHTML;
    if (text.length >= 30) {
        text = text.slice(0, 30);
        text = text.replace(/(<br>)|(<\/br>)/g, '');
        Placeholder.textContent = text;
    }
}

function CheckCharLimitH(evt) {
    let key = evt.key;
    if ((Placeholder.textContent.length >= 30 || (key === "Enter")) && (key !== "Backspace" && key !== "ArrowDown" && key !== "ArrowUp" && key !== "ArrowLeft" && key !== "ArrowRight")) {
        evt.preventDefault();
    }
    else {
        Placeholder.addEventListener('keyup', CountChars);
    }
}

function sendParameters() {
    let editorContentText = editor.outerHTML;
    EditorText.value = editorContentText;
    let positionList = document.createElement('ul'); // a list that saves positions and programs 
    positionList.style.display = 'none';
    let profidlitem = document.createElement('li');
    let selectedPrimaryPos = PrimaryPosition.value;
    profidlitem.innerText = selectedPrimaryPos;
    positionList.append(profidlitem);
    PositionsPassed = positionList.outerHTML;
    inserPos_List();
}

function inserPos_List() {
    let uls = PositionsShow.getElementsByTagName('ul');
    for (let i = 0; i < uls.length; i++) {
        PositionsPassed += uls[i].outerHTML;
    }
    PositionsPostPassed.value += PositionsPassed;
}

function detectPaste(evt) {
    evt.preventDefault()
    setTimeout(ChangeScrollerHeight(evt, false), 1);
}


function calculateScrollerHeight() {
    // *Calculation of how tall scroller should be
    var visibleRatio = scrollContainer.offsetHeight / scrollContent.scrollHeight;
    return visibleRatio * scrollContainer.offsetHeight;
}

function ChangeScrollerHeight(evt) {
    scrollerHeight = calculateScrollerHeight();
    scroller.style.height = scrollerHeight + 'px';
}

function moveScroller(evt) {
    // Move Scroll bar to top offset
    if (evt != null) {
        var scrollPercentage = evt.target.scrollTop / scrollContent.scrollHeight;
        topPosition = scrollPercentage * (scrollContainer.offsetHeight - 10); // 5px arbitrary offset so scroll bar doesn't move too far beyond content wrapper bounding box
        scroller.style.top = topPosition + 'px';
    }

}

function startDrag(evt) {
    normalizedPosition = evt.pageY;
    contentPosition = scrollContent.scrollTop;
    scrollerBeingDragged = true;
}

function stopDrag(evt) {
    scrollerBeingDragged = false;
}

function scrollBarScroll(evt) {
    if (scrollerBeingDragged === true) {
        var mouseDifferential = evt.pageY - normalizedPosition;
        var scrollEquivalent = mouseDifferential * (scrollContent.scrollHeight / scrollContainer.offsetHeight);
        scrollContent.scrollTop = contentPosition + scrollEquivalent;
    }
}

function createScroller() {
    // *Creates scroller element and appends to '.scrollable' div
    // create scroller element
    scroller = document.createElement("div");
    scroller.className = 'scroller';

    // determine how big scroller should be based on content
    scrollerHeight = calculateScrollerHeight();

    // *If there is a need to have scroll bar based on content size
    scroller.style.height = scrollerHeight + 'px';

    // append scroller to scrollContainer div
    editor.appendChild(scroller);

    // show scroll path divot
    editor.className += ' showScroll';

    // attach related draggable listeners
    scroller.addEventListener('mousedown', startDrag);
    window.addEventListener('mouseup', stopDrag);
    window.addEventListener('mousemove', scrollBarScroll)
}


function OpenPopUp(e) {
    let id = e.querySelector(".projBox_ProjectID").innerText;
    hiddenPostID.value = id;
    PopUpButton.click();
}

function OpenPopUp_date(e) {
    let id = e.querySelector(".projBox_DateRequested").innerText;
    hiddenPostID.value = id;
    PopUpButton.click();
}

function ClosePopUp(e) {
    closePopUpButton.click();
}

function RequstPos(e) {
    let posIDNode = e.parentNode.querySelector(".posNumber");
    let posID = posIDNode.innerText;
    hiddenPostRequest.value = posID;
    sendRequestB.click();
    e.setAttribute("disabled", "disabled")
    e.classList.remove("ButtonBlue")
}

function RemoveUserFromPos(e) {
    let posIDNode = e.parentNode.querySelector(".posNumber");
    let posID = posIDNode.innerText;
    let UserIDNode = e.parentNode.querySelector(".userNumber");
    let userID = UserIDNode.innerText;
    hiddenPostPosID.value = posID;
    hiddenPostUserID.value = userID;
    RemoveUser.click();
    e.setAttribute("disabled", "disabled")
    e.classList.remove("ButtonRed")
}

function DeletePos(e) {
    let parent = e.parentNode.parentNode.parentNode;
    let posIDNode = e.parentNode.parentNode.parentNode.querySelector(".posNumber");
    let posID = posIDNode.innerText;
    hiddenPostPosID.value = posID;
    parent.classList.add("deletePosAnim");
    setTimeout(function () { parent.parentNode.removeChild(parent); }, 1000);
    RemovePos.click();
}

function PostUpdateRate(e) {
    let RateDrop = document.querySelector(".UpdateRateDrop");
    let Rate = RateDrop.options[RateDrop.selectedIndex].value;
    hiddenPostRate.value = Rate;
    UpdateRate.click();
    e.setAttribute("disabled", "disabled")
    e.classList.remove("ButtonBlue")
}


function UserProfileRedirection(e) {
    let id = e.parentNode.parentNode.parentNode.querySelector(".UserBox_UserID").innerText;
    let hiddenField = document.querySelector("#ClickedUserID");
    hiddenField.value = id;
    e.click;
}


function GetProjectID_Button(e) {
    let posID = e.parentNode.parentNode.querySelector(".projBox_ProjectPosID").innerText;
    let reqID = e.parentNode.parentNode.querySelector(".projBox_RequestID").innerText;
    hiddenPostPosID.value = posID;
    hiddenPostReqID.value = reqID;
}

function DeclineInvitation_Button(e) {
    let posID = e.parentNode.parentNode.querySelector(".projBox_ProjectPosID").innerText;
    let reqID = e.parentNode.parentNode.querySelector(".projBox_RequestID").innerText;
    hiddenPostPosID.value = posID;
    hiddenPostReqID.value = reqID;
    let declineButton = document.querySelector(".DeclineInvitation");
    declineButton.click();
}

function AcceptInvitation_Button(e) {
    let posID = e.parentNode.parentNode.querySelector(".projBox_ProjectPosID").innerText;
    let reqID = e.parentNode.parentNode.querySelector(".projBox_RequestID").innerText;
    hiddenPostPosID.value = posID;
    hiddenPostReqID.value = reqID;
    let acceptButton = document.querySelector(".AcceptInvitation");
    acceptButton.click();
}

function GetProjectID2_Button(e) {
    let userID = e.parentNode.parentNode.querySelector(".UserBox_UserID").innerText;
    let posID = e.parentNode.parentNode.querySelector(".projBox_ProjectPosID").innerText;
    let reqID = e.parentNode.parentNode.querySelector(".projBox_RequestID").innerText;
    let hiddenField = document.querySelector("#ClickedUserID");
    hiddenField.value = userID;
    hiddenPostPosID.value = posID;
    hiddenPostReqID.value = reqID;
}

function SetNameGroup(e, stringname) {
    e.name = stringname;
    e.removeAttribute("onclick");
}

function RequstPos2(e) {
    let elementChecked = document.querySelector('input[name="positions"]:checked').value;
    hiddenPostPosID.value = elementChecked;
}

function ChangeSize() {
    let size = document.querySelector(".positions_list").scrollHeight;
    document.querySelector(".blurdrop").style.height = size + "px";
}

function popupMenuMechanism(element, displaytype, optionalelement, OptionelementbgNeutralize, elementbgNeutralize, optionalelement2) {
    switch (getComputedStyle(element, "display").display) {
        case "block":
            element.style.display = "none";
            if (optionalelement) {
                optionalelement.style.display = "none";
            }
            if (optionalelement2) {
                optionalelement2.style.display = "none";
            }
            if (OptionelementbgNeutralize != "") {
                elementbgNeutralize.style.backgroundColor = "transparent";
            }
            break;
        case "flex":
            element.style.display = "none";
            if (optionalelement) {
                optionalelement.style.display = "none";
            }
            if (optionalelement2) {
                optionalelement2.style.display = "none";
            }
            if (OptionelementbgNeutralize != "") {
                elementbgNeutralize.style.backgroundColor = "transparent";
            }
            break;
        case "none":
            element.style.display = displaytype;
            if (optionalelement) {
                optionalelement.style.display = displaytype;
            }
            if (optionalelement2) {
                optionalelement2.style.display = displaytype;
            }
            if (OptionelementbgNeutralize) {
                elementbgNeutralize.style.backgroundColor = OptionelementbgNeutralize;
            }
            break;
    }
}

function popupMenuMechanism2(element, displaytype, optionalelement,defaultbackgroundcolor, OptionelementbgNeutralize, elementbgNeutralize, optionalelement2) {
    switch (getComputedStyle(element, "display").display) {
        case "block":
            element.style.display = "none";
            if (optionalelement) {
                optionalelement.style.display = "none";
            }
            if (optionalelement2) {
                optionalelement2.style.display = "none";
            }
            if (OptionelementbgNeutralize != "") {
                elementbgNeutralize.style.backgroundColor = defaultbackgroundcolor;
            }
            break;
        case "flex":
            element.style.display = "none";
            if (optionalelement) {
                optionalelement.style.display = "none";
            }
            if (optionalelement2) {
                optionalelement2.style.display = "none";
            }
            if (OptionelementbgNeutralize != "") {
                elementbgNeutralize.style.backgroundColor = defaultbackgroundcolor;
            }
            break;
        case "none":
            element.style.display = displaytype;
            if (optionalelement) {
                optionalelement.style.display = displaytype;
            }
            if (optionalelement2) {
                optionalelement2.style.display = displaytype;
            }
            if (OptionelementbgNeutralize) {
                elementbgNeutralize.style.backgroundColor = OptionelementbgNeutralize;
            }
            break;
    }
}

function clickButton(button) {
    button.click();
}


function OpeningpopupMenuMechanism(popUp_element, displaytype) {
    popUp_element.style.display = displaytype;
}

function ClosingpopupMenuMechanism(popUp_element) {
    popUp_element.style.display = "none";
}

function addevent() {
    let ProjectHeaders = document.querySelector(".ProjectHeaders"),
        positionpanel = document.querySelector(".profileProjectPositions_Container"),
        Bu_openInvPanel = document.querySelector(".openInvPanel"),
        inviteUserDropContainer = document.querySelector(".inviteUserDropContainer");
   popupMenuMechanism(positionpanel, "flex", ProjectHeaders, "rgb(45,45,45)", inviteUserDropContainer);
}

function viewPhoto(e) {
    let photoUrl = e.style.backgroundImage.slice(4, -1).replace(/["']/g, ""),
        image = document.querySelector("#ViewPhoto"),
        imagecontainer = document.querySelector(".viewPhoto_container"),
        overlay = document.querySelector(".overlay"),
        hiddenGeneral = document.querySelector("#GeneralPost"),
        popUpRemoveCreation = document.querySelector(".popUpRemoveCreation"),
        creID = e.querySelector(".CreationID").innerText;
    hiddenGeneral.value = creID;
    image.src = photoUrl;
    OpeningpopupMenuMechanism(imagecontainer, "block");
    OpeningpopupMenuMechanism(overlay, "block");
    OpeningpopupMenuMechanism(popUpRemoveCreation, "block");
}

function hideImg() {
    let image = document.querySelector("#ViewPhoto"),
        imagecontainer = document.querySelector(".viewPhoto_container"),
        overlay = document.querySelector(".overlay"),
        removeCreationB = document.querySelector(".popUpRemoveCreation");
    ClosingpopupMenuMechanism(imagecontainer);
    ClosingpopupMenuMechanism(overlay);
    ClosingpopupMenuMechanism(removeCreationB);
}

function editContacts(e) {
    let contactadder = document.querySelector(".addContact_popup");
    if (getComputedStyle(contactadder, "display").display == "none") {
        OpeningpopupMenuMechanism(contactadder, "flex");
        e.backgroundColor = "rgba(242, 147, 235,0.3)";
    }
    else {
        ClosingpopupMenuMechanism(contactadder);
        e.backgroundColor = "rgb(29, 237, 167)";
    }
}