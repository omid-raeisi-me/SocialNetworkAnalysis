using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class NetworkInformation : INetworkInformation
    {
        private readonly IConnectedComponents _connectedComponents;
        private readonly IDiameter _diameter;

        public NetworkInformation(
            IConnectedComponents connectedComponents,
            IDiameter diameter)
        {
            _connectedComponents = connectedComponents;
            _diameter = diameter;
        }

        public NetworkInformationResult Execute(SocialGraph graph)
        {
            NetworkInformationResult result = new();
            var allNodes = graph.GetUsers().ToList();
            result.TotalUserCount = allNodes.Count;
            if (result.TotalUserCount == 0)
            {
                return result;
            }

            //int totalDegree = 0;
            Dictionary<int, int> nodesWithMostDegree = new();
            int maxDegree = -1;

            foreach (int node in allNodes)
            {
                var friends = graph.GetFriends(node);
                int currentNodeDegree = friends?.Count() ?? 0;
                //totalDegree += currentNodeDegree;

                if (currentNodeDegree > maxDegree)
                {
                    nodesWithMostDegree.Clear();
                    nodesWithMostDegree.Add(node, currentNodeDegree);
                    maxDegree = currentNodeDegree;
                }
                else if (currentNodeDegree == maxDegree)
                {
                    nodesWithMostDegree.Add(node, currentNodeDegree);
                }
            }

            result.TotalFriendshipCount = graph.GetEdgeCount();
            result.UsersWithMostFriends = nodesWithMostDegree;

            result.AverageRelationPerUser = (double)(2 * graph.GetEdgeCount()) / result.TotalUserCount;
            if (result.TotalUserCount > 1)
            {
                result.density = (double)(2 * graph.GetEdgeCount()) / (result.TotalUserCount * (result.TotalUserCount - 1));
            }

            var componentsResult = _connectedComponents.Execute(graph);

            result.LargestFriendshipGroup = componentsResult.Components
                .OrderByDescending(c => c.Count)
                .FirstOrDefault() ?? new List<int>();

            DiameterResult diameterResult = _diameter.Execute(graph);
            result.diameter = diameterResult.diameter;

            return result;
        }
    }
}