using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class DistancesFromAllUsers : IDistancesFromAllUsers
    {
        private readonly IBFS _bfs;

        public DistancesFromAllUsers(IBFS bfs)
        {
            _bfs = bfs;
        }

        public DistancesFromAllUsersResult Execute(SocialGraph graph, int startNode)
        {
            DistancesFromAllUsersResult result = new();
            var allNodes = graph.GetUsers();
            Dictionary<int, double> distances = new();

            var bfsResult = _bfs.Execute(graph, startNode);
            var bfsDistances = bfsResult?.Distances ?? new Dictionary<int, int>();

            foreach (var node in allNodes)
            {
                if (node == startNode)
                {
                    distances.Add(startNode, 0);
                    continue;
                }

                if (bfsDistances.ContainsKey(node))
                {
                    distances.Add(node, bfsDistances[node]);
                }
                else
                {
                    distances.Add(node, double.PositiveInfinity);
                }
            }

            result.distances = distances
                .OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            return result;
        }
    }
}

