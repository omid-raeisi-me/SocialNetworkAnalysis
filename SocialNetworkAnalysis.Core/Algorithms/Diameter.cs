using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class Diameter : IDiameter
    {

        private readonly IBFS _bfs;

        public Diameter(IBFS Bfs) 
        {
            _bfs = Bfs;
        }
        public DiameterResult Execute(SocialGraph graph)
        {
            DiameterResult result = new();
            var allNodes = graph.GetUsers();
            if (allNodes.Count() <= 1)
            {
                result.diameter = 0;
                return result;
            }

            int maxShortestPath = 0;

            foreach (var startNode in allNodes)
            {
                var bfsResult = _bfs.Execute(graph, startNode);
                if (bfsResult?.Distances == null )
                {
                    continue;
                }

                foreach (int distance in bfsResult.Distances.Values)
                { 
                    if (distance > maxShortestPath)
                    {
                        maxShortestPath = distance;
                    }
                }
            }

            result.diameter = maxShortestPath;
            return result;
        }    
    }
}
