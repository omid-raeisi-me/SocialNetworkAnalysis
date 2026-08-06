# Social Network Analysis (SNA) 🚀
An advanced implementation of Social Network Analysis algorithms using optimized data structures. This project focuses on graph theory applications, specifically tailored for sparse social networks.

## 📌 Overview
This project provides a comprehensive suite of algorithms to analyze social structures, identify influential nodes, and measure connectivity within a network. Unlike standard implementations, this project utilizes a **`Dictionary<User, HashSet<User>>`** (Adjacency List) to ensure optimal performance in both time and space complexity.

## 🛠 Technical Stack & Data Structure
- **Language:** C#, Html, Css, Javascript
- **Core Structure:** Adjacency List using `HashSet`.
- **Why `HashSet`?** 
  - **Edge Lookup:** $O(1)$ average time complexity.
  - **Memory Efficiency:** $O(V + E)$ instead of $O(V^2)$, making it ideal for sparse social graphs.
  - **Uniqueness:** Automatically handles duplicate edges.

## 📊 Algorithm Complexity Analysis
The algorithms used in the project are presented below.

| Algorithm | Purpose |
| :--- | :---: |
| **BFS** | Level-order traversal & shortest paths in unweighted graphs |
| **DFS** | Depth-first traversal & connectivity analysis |
| **Adamic Adar** | Link prediction based on shared neighbors weight |
| **Average Degree** | Measure of network density |
| **Average Path Length** | Mean shortest distance between all pairs |
| **Betweenness Centrality** | Identifying nodes that act as bridges |
| **Closeness Centrality** | Measuring how fast information spreads from a node |
| **Common Neighbors** | Counting shared connections between two nodes |
| **Community Detection** | Grouping nodes into clusters (e.g., Louvain/Modularity) |
| **Connected Components** | Finding isolated sub-networks |
| **Degree Centrality** | Measuring node popularity/influence |
| **Diameter** | Finding the longest shortest path in the network |
| **Distances (All-Pairs)** | Computing distance matrix for all users |
| **Jaccard Similarity** | Measuring similarity via neighbor overlap ratio |
| **Link Prediction** | Forecasting future connections |
| **Network Info** | Calculating global metrics (Density, Clustering, etc.) |

## 📂 Project Documentation
For a detailed mathematical breakdown and implementation logic of each algorithm, please refer to the official project document:

👉 **[Download Project Documentation (PDF)](https://raw.githubusercontent.com/omid-raeisi-me/SocialNetworkAnalysis/refs/heads/master/README.pdf)**


