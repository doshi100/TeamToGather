
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




if (scrollContainer != null) {
    createScroller();
    scrollContent.addEventListener('paste', detectPaste);
    scrollContent.addEventListener('keydown', ChangeScrollerHeight);
    scrollContent.addEventListener('scroll', moveScroller);
    SummaryForm.addEventListener('keydown', CheckCharLimit);
    SummaryForm.addEventListener('paste', detectPaste_Limit);
    var path = window.location.pathname;
    var page = path.split("/").pop();
    if (page == "ProjectCreation.aspx" || page == "Profile.aspx") {
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

function GetProjectID2_Button(e) {
    let userID = e.parentNode.parentNode.querySelector(".UserBox_UserID").innerText;
    let posID = e.parentNode.parentNode.querySelector(".projBox_ProjectPosID").innerText;
    let reqID = e.parentNode.parentNode.querySelector(".projBox_RequestID").innerText;
    let hiddenField = document.querySelector("#ClickedUserID");
    hiddenField.value = userID;
    hiddenPostPosID.value = posID;
    hiddenPostReqID.value = reqID;
}