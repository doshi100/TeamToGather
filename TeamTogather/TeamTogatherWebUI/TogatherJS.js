let smallNav = document.querySelector(".loginsmlMenu");
let ProfilePhoto = document.querySelector(".profilePhoto");
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

