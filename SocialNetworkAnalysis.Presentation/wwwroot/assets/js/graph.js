const cy = cytoscape({

    container: document.getElementById("cy"),

    elements: [

        { data: { id: "1", label: "sara", type: "person" } },
        { data: { id: "11", label: "taha", type: "person" } },
        { data: { id: "2", label: "Bob", type: "person" } },
        { data: { id: "3", label: "John", type: "person" } },
        { data: { id: "4", label: "Emma", type: "person" } },
        { data: { id: "5", label: "David", type: "person" } },
        { data: { id: "6", label: "Lucas", type: "person" } },
        { data: { id: "7", label: "Olivia", type: "person" } },
        { data: { id: "8", label: "Sophia", type: "person" } },
        { data: { id: "9", label: "Liam", type: "person" } },
        { data: { id: "10", label: "Noah", type: "person" } },

        { data: { source: "1", target: "2" } },
        { data: { source: "1", target: "3" } },
        { data: { source: "1", target: "4" } },
        { data: { source: "2", target: "5" } },
        { data: { source: "3", target: "6" } },
        { data: { source: "4", target: "7" } },
        { data: { source: "5", target: "8" } },
        { data: { source: "6", target: "9" } },
        { data: { source: "7", target: "10" } },
        { data: { source: "8", target: "9" } },
        { data: { source: "2", target: "7" } },
        { data: { source: "3", target: "8" } },
        { data: { source: "5", target: "10" } }

    ],

    style: [

    {
        selector: "node",

        style: {

            label: "data(label)",

            width: 50,
            height: 50,

            "background-color": "#5B8CFF",

            "border-width": 2,

            "border-color": "#BFD3FF",

            color: "#FFFFFF",

            "font-size": 10,

            "font-weight": "600",

            "text-valign": "center",

            "text-halign": "center",

            "overlay-opacity": 0,

            "text-outline-width": 0,

            "transition-property": "background-color, border-color",

            "transition-duration": "150ms"

        }

    },

    {
        selector: "edge",

        style: {

            width: 2.2,

            "line-color": "#55657E",

            opacity: .8,

            "curve-style": "bezier",

            "target-arrow-shape": "none"

        }

    },

    {
        selector: "node:selected",

        style: {

            "background-color": "#8c3aff",

            "border-color": "#2DD4BF",

            "border-width": 5

        }

    },

    {
        selector: "edge:selected",

        style: {

            "line-color": "#2DD4BF",

            width: 3.5

        }

    },

    {
        selector: ".highlight",

        style: {

            "background-color": "#F59E0B",

            "border-color": "#FCD34D",

            "border-width": 5

        }

    }

],

    layout: {

        name: "cose",

        animate: true,

        animationDuration: 700,

        fit: true,

        padding: 60

    },

    wheelSensitivity: 0.18

});

const algorithmPanel = document.querySelector(".algorithm-panel");

const collapseButton = document.querySelector(".collapse-btn");

const collapseIcon = collapseButton.querySelector("i");

collapseButton.addEventListener("click", () => {

    algorithmPanel.classList.toggle("collapsed");

    if (algorithmPanel.classList.contains("collapsed")) {

        collapseIcon.classList.remove("bi-chevron-down");
        collapseIcon.classList.add("bi-chevron-up");

    }
    else {

        collapseIcon.classList.remove("bi-chevron-up");
        collapseIcon.classList.add("bi-chevron-down");

    }

});
