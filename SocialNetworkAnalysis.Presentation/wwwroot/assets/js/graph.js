window.graph = {};

window.graph.cy = null;

window.graph.initialize = function () {

    window.graph.cy = cytoscape({

        container: document.getElementById("cy"),

        elements: [],

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

    if (algorithmPanel && collapseButton) {

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
    }
};

window.graph.loadGraph = function (graph) {

    const cy = window.graph.cy;

    cy.elements().remove();

    const elements = [];

    graph.users.forEach(user => {

        elements.push({
            data: {
                id: user.id.toString(),
                label: user.id + ". " + user.name
            }
        });

    });

    graph.friendships.forEach(friendship => {

        elements.push({
            data: {
                source: friendship.fromId.toString(),
                target: friendship.toId.toString()
            }
        });

    });

    cy.add(elements);

    cy.layout({
        name: "cose"
    }).run();
};