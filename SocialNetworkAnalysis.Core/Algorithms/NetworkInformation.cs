using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class NetworkInformation : INetworkInformation
    {
        private readonly IAverageDegree _averageDegree;
        private readonly IConnectedComponents _connectedComponents;
        private readonly IDensity _density;
        private readonly IDiameter _diameter;

        public NetworkInformation(
            IAverageDegree averageDegree,
            IConnectedComponents connectedComponents,
            IDensity density,
            IDiameter diameter)
        {
            _averageDegree = averageDegree;
            _connectedComponents = connectedComponents;
            _density = density;
            _diameter = diameter;
        }

        public NetworkInformationResult Execute(SocialGraph graph)
        {
            NetworkInformationResult result = new();
            var allNodes = graph.GetUsers();
            result.TotalUserCount = allNodes.Count();
            if (result.TotalUserCount == 0)
            {
                return result;
            }

            int totalDegree = 0;
            Dictionary<int, int> nodesWhithMostDegree = new();
            int maxDegree = -1;

            foreach (int node in allNodes)
            {
                var friends = graph.GetFriends(node);
                int currentNodeDegree = friends?.Count() ?? 0;
                totalDegree += currentNodeDegree;

                if (currentNodeDegree > maxDegree)
                {
                    nodesWhithMostDegree.Clear();
                    nodesWhithMostDegree.Add(node, currentNodeDegree);
                    maxDegree = currentNodeDegree;
                }
                else if (currentNodeDegree == maxDegree)
                {
                    nodesWhithMostDegree.Add(node, currentNodeDegree);
                    maxDegree = currentNodeDegree;

                }
            }

            result.TotalFriendshipCount = totalDegree / 2;
            result.UsersWithMostFriends = nodesWhithMostDegree;

            var averageRelationPerUser = _averageDegree.Execute(graph);
            result.AverageRelationPerUser = averageRelationPerUser?.AverageDegree ?? 0;

            var componentsResult = _connectedComponents.Execute(graph);
            var components = componentsResult.Components;

            int largestComponentSize = 0;
            foreach (var component in components)
            {
                if (component.Count > largestComponentSize)
                {
                    largestComponentSize = component.Count;
                }
            }

            result.LargestFriendshipGroupSize = largestComponentSize;

            DiameterResult diameterResult = _diameter.Execute(graph);
            result.diameter = diameterResult.diameter;

            DensityResult densityResult = _density.Execute(graph);
            result.density = densityResult.density;

            return result;
        }
    }
}