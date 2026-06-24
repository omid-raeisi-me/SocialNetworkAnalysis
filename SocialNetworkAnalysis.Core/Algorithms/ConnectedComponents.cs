using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Contracts;
using SocialNetworkAnalysis.Core.Models;
using SocialNetworkAnalysis.Core.Result;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class ConnectedComponents : IConnectedComponents
    {
        public ConnectedComponentsResult Execute(SocialGraph graph)
        {
            BFS bfsExecuter = new BFS();

            List<List<int>> components = new();
            HashSet<int> visitedNodes = new();

            var allNodes = graph.GetAllNodes();

            foreach (int nodeId in allNodes)
            {
                if (visitedNodes.Contains(nodeId))
                {
                    continue;
                }

                BFSResult bfsResult = bfsExecuter.Execute(graph, nodeId);
                List<int> component = bfsResult.VisitedNodes;

                components.Add(component);

                visitedNodes.UnionWith(component);
            }

            ConnectedComponentsResult connectedComponents = new()
            {
                Components = components,
                ComponentsCount = components.Count
            };

            return connectedComponents;
        }
    }
}