using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    

    public class AveragePathLength : IAveragePathLength
    {
        private readonly IBFS _bfs;

        public AveragePathLength(IBFS Bfs)
        { 
            _bfs = Bfs;
        }
        public AverageDegreeResult Execute(SocialGraph graph)
        {
            AverageDegreeResult result = new();

            var allNodes = graph.GetAllNodes();
            if (allNodes.Count() <= 1)
            {
                result.AverageDegree = 0;
                return result;
            }

            double sumOfAllPaths = 0;
            int reachablePairsCount = 0;


            foreach (var startNode in allNodes)
            {
                var bfsResult = _bfs.Execute(graph, startNode);
                if (bfsResult?.Distances == null)
                {
                    continue;
                }
                foreach (var x in bfsResult.Distances)
                {
                    int targetNode = x.Key;
                    int distance = x.Value;

                    if (targetNode == startNode)
                    {
                        continue;
                    }

                    sumOfAllPaths += distance;
                    reachablePairsCount++;
                }
            }
            if (reachablePairsCount > 0)
            {
                result.AverageDegree = sumOfAllPaths / reachablePairsCount;
            }
            else
            {
                result.AverageDegree = 0;
            }
            return result;
        }
    }
}

