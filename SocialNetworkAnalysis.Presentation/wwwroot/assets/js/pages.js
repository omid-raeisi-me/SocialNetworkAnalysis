document.addEventListener("click", function (event) {

    const button = event.target.closest(".collapse-btn");

    if (!button)
        return;

    const panel = document.querySelector(".algorithm-panel");

    if (!panel)
        return;

    const icon = button.querySelector("i");

    panel.classList.toggle("collapsed");

    if (panel.classList.contains("collapsed")) {

        icon.classList.remove("bi-chevron-up");
        icon.classList.add("bi-chevron-down");

    } else {

        icon.classList.remove("bi-chevron-down");
        icon.classList.add("bi-chevron-up");
    }
});