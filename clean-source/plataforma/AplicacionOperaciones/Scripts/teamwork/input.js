
handleClickInput = (e) => {
    const novalid = (e.attributes.novalid !== undefined) ? "novalid" : "";

    document.getElementById(e.dataset.target).style.transform = "translate3d(0, -30px, 0)";
    document.getElementById(e.dataset.target).style.transition = "all .15s ease-out";

    if (novalid === "") {
        document.getElementById(e.dataset.target).style.color = "#428bca";
        e.style.color = "#428bca";
        e.style.borderBottom = "1px solid #428bca";
    }
};

handleBlurInput = (e) => {
    const required = (e.attributes.required !== undefined) ? "required" : "";

    if (e.value === "") {
        document.getElementById(e.dataset.target).style.transform = "translate3d(0, 0, 0)";
    }

    if (required === "") {
        document.getElementById(e.dataset.target).style.color = "rgb(100, 100, 100)";
        e.style.color = "rgb(100, 100, 100)";
        e.style.borderBottom = "1px solid rgb(200, 200, 200)";
    }

    if (required !== "") {
        if (e.value !== "") {
            document.getElementById(e.dataset.target).style.color = "rgb(100, 100, 100)";
            e.style.color = "rgb(100, 100, 100)";
            e.style.borderBottom = "1px solid rgb(200, 200, 200)";
        }
        else {
            document.getElementById(e.dataset.target).style.color = "rgb(255, 0, 0)";
            e.style.color = "rgb(255, 0, 0)";
            e.style.borderBottom = "1px solid rgb(255, 0, 0)";
            e.setAttribute("novalid", "")
        }
    }

}
