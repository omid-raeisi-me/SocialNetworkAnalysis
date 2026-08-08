window.graph = {};

window.graph.cy = null;

window.graph.initialize = function () {

    console.log("CYTOSCAPE INITIALIZED");
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
                label: user.name
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

window.graph.highlightNode = function (nodeId, backgroundColor, borderColor) {

    const cy = window.graph.cy;

    const node = cy.getElementById(nodeId.toString());

    if (node.length === 0)
        return;


    node.style({

        "background-color": backgroundColor,

        "border-color": borderColor,

        "border-width": 5

    });
};

window.graph.highlightEdge = function (source, target, color) {

    const cy = window.graph.cy;


    const edge = cy.edges().filter(function (ele) {

        return (
            ele.data("source") === source.toString() &&
            ele.data("target") === target.toString()
        )
            ||
            (
                ele.data("source") === target.toString() &&
                ele.data("target") === source.toString()
            );

    });


    if (edge.length === 0)
        return;


    edge.style({

        "line-color": color,

        "width": 5

    });
};

window.graph.registerNodeClick = function (dotNetReference) {

    const cy = window.graph.cy;

    cy.on("tap", "node", function (event) {

        const node = event.target;

        const position = node.renderedPosition();

        const width = node.renderedOuterWidth();
        const height = node.renderedOuterHeight();

        const panelX = position.x - (width / 2);
        const panelY = position.y - 20;

        if (event.originalEvent.ctrlKey) {

            dotNetReference.invokeMethodAsync(
                "OpenNodeMenu",
                parseInt(node.id()),
                panelX + 90,
                panelY
            );

            return;
        }

        if (event.originalEvent.shiftKey) {

            dotNetReference.invokeMethodAsync(
                "OnNodeClicked",
                parseInt(node.id()),
                panelX,
                panelY
            );

            return;
        }

    });

};

window.graph.getNodeName = function (id) {

    const cy = window.graph.cy;

    const node = cy.getElementById(id.toString());

    if (node.length === 0) {
        return null;
    }

    return node.data("label");
};

window.graph.searchNodes = function (query) {

    const cy = window.graph.cy;

    if (!cy || !query) {
        return [];
    }

    query = query.trim().toLowerCase();

    if (!query) {
        return [];
    }

    const results = [];

    cy.nodes().forEach(node => {

        const id = node.id();
        const name = node.data("label") || "";

        const idMatch =
            id.toLowerCase() === query;

        const nameMatch =
            name.toLowerCase().includes(query);

        if (idMatch || nameMatch) {

            results.push({
                id: parseInt(id),
                name: name
            });

        }

    });

    results.sort((a, b) => {

        const aExact =
            a.id.toString().toLowerCase() === query;

        const bExact =
            b.id.toString().toLowerCase() === query;

        if (aExact && !bExact)
            return -1;

        if (!aExact && bExact)
            return 1;

        const aNameExact =
            a.name.toLowerCase() === query;

        const bNameExact =
            b.name.toLowerCase() === query;

        if (aNameExact && !bNameExact)
            return -1;

        if (!aNameExact && bNameExact)
            return 1;

        return a.name.localeCompare(b.name);

    });

    return results.slice(0, 10);
};


window.graph.selectSearchNode = function (id) {

    const cy = window.graph.cy;

    if (!cy)
        return false;

    const node = cy.getElementById(id.toString());

    if (!node || node.length === 0)
        return false;

    cy.elements().unselect();

    node.select();

    cy.animate({

        center: {
            eles: node
        },

        zoom: Math.max(cy.zoom(), 1.2)

    }, {

        duration: 400

    });

    return true;
};