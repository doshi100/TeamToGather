
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



if (scrollContainer != null) {
    createScroller();
    scrollContent.addEventListener('paste', detectPaste);
    window.addEventListener('keydown', ChangeScrollerHeight);
    scrollContent.addEventListener('scroll', moveScroller);
    SummaryForm.addEventListener('keydown', CheckCharLimit);
    SummaryForm.addEventListener('paste', detectPaste_Limit);
    bold.addEventListener('mousedown', () => format4('b',"font-family: tekoSemiBold;"));
    highlightB.addEventListener('mousedown', () => format4('strong', "background-color: rgb(108, 241, 186); font-weight: 400"));
    underLine.addEventListener('mousedown', () => format4('u', ""));
    italic.addEventListener('mousedown', () => format4('i',""));
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

function format(evt, command) {
    document.execCommand(command, false, null)
}


function format2(command) {
    let text = document.getSelection();
    let ParentElement = text.baseNode.parentElement;
    if (ParentElement.tagName !== "SPAN") {
        let text2 = document.getSelection().getRangeAt(0);
        let node = document.createElement('span');
        node.classList.add(command);
        text2.surroundContents(node);
    }
    else {
        if (!ParentElement.classList.contains(command)) {
            ParentElement.classList.add(command);
        }
        else {
            ParentElement.classList.remove(command);
        }
    }

}

function BoldFormat() {
    //let text = document.getSelection();
    //let ParentElement = text.baseNode.parentElement;
    //if (!parentElement.tagName == "SPAN") {
    //    let text2 = document.getSelection().getRangeAt(0);
    //    let node = document.createElement('span');
    //    node.classList.add('bold');
    //    text2.surroundContents(node);
    //}
    //else {
    //    if (!ParentElement.classList.contains("bold")) {
    //        let text2 = document.getSelection().getRangeAt(0);
    //        let node = document.createElement('span');
    //        node.classList.add('bold');
    //        text2.surroundContents(node);
    //    }
    //    else {
    //        ParentElement.classList.remove('bold');
    //    }
    //}
    format2("bold")
}

function UnderLinedFormat() {
    //let text = document.getSelection();
    //let ParentElement = text.baseNode.parentElement;
    //if (!ParentElement.classList.contains("MakeUnderLine")) {
    //    let text2 = document.getSelection().getRangeAt(0);
    //    let node = document.createElement('span');
    //    node.classList.add('MakeUnderLine');
    //    text2.surroundContents(node);
    //}
    //else {
    //    ParentElement.classList.remove('MakeUnderLine');
    //}
    format2("MakeUnderLine")
}

function HighlightFormat() {
    //let text = document.getSelection();
    //let ParentElement = text.baseNode.parentElement;
    //if (!ParentElement.classList.contains("highlight")) {
    //    let text2 = document.getSelection().getRangeAt(0);
    //    let node = document.createElement('span');
    //    node.classList.add('highlight');
    //    text2.surroundContents(node);
    //}
    //else {
    //    ParentElement.classList.remove('highlight');
    //}
    format2("highlight")
}

if (Placeholder != null) {
    window.addEventListener("load", function () {
        Placeholder.innerHTML = "";
        Placeholder.focus();
    })
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

function CheckCharLimit(evt) {
    let key = evt.key;
    if ((SummaryForm.textContent.length >= 114 || (key === "Enter")) && (key !== "Backspace" && key !== "ArrowDown" && key !== "ArrowUp" && key !== "ArrowLeft" && key !== "ArrowRight" )) {
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

function detectPaste(evt) {
    evt.preventDefault()
    var text = evt.clipboardData.getData('text/plain')
    document.execCommand('insertText', true, text)
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

