using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class ConnectedComponents : IConnectedComponents
    {
        private readonly IBFS _bfs;

        public ConnectedComponents(IBFS bfs)
        {
            _bfs = bfs;
        }

        public ConnectedComponentsResult Execute(SocialGraph graph)
        {

            List<List<int>> components = new();
            HashSet<int> visitedNodes = new();

            var allNodes = graph.GetUsers();

            foreach (int nodeId in allNodes)
            {
                if (visitedNodes.Contains(nodeId))
                {
                    continue;
                }

                BFSResult bfsResult = _bfs.Execute(graph, nodeId);
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