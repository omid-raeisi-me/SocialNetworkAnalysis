using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class DistancesFromAllUsers : IDistancesFromAllUsers
    {
        private readonly IShortestPath _shortestPath;

        public DistancesFromAllUsers(IShortestPath shortestPath )
        {
            _shortestPath = shortestPath;
        }

        public DistancesFromAllUsersResult Execute(SocialGraph graph, int startNode)
        {
            DistancesFromAllUsersResult result = new();
            var allNodes = graph.GetAllNodes();
            Dictionary<int, double> distances = new();

            foreach (var node in allNodes)
            {
                if (node == startNode)
                {
                    distances.Add(startNode, 0);
                    continue;
                }

                ShortestPathResult shortestPathresult = _shortestPath.Execute(graph, startNode, node);

                if (shortestPathresult.IsPathExist)
                {
                    double distance = shortestPathresult.Path.Count() - 1;
                    distances.Add(node, distance);
                }
                else
                {
                    distances.Add(node, double.PositiveInfinity);
                }
            }

            result.distances = distances.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        
            return result;
        }
    }
}

