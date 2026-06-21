using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    internal class ShortestPath : IShortestPath
    {
        public ShortestPathResult Execute(SocialGraph graph,int startNodeId, int targetNodeId)
        {

            BFS bfsExecuter = new();
            BFSResult bfsResult = bfsExecuter.Execute(graph,startNodeId);
            if (!(bfsResult.VisitedNodes.Contains(targetNodeId)))
            {
                ShortestPathResult resultWithoutPath = new()
                {
                    IsPathexist = false
                };
                return resultWithoutPath;
            }
            
            List<int> path = new();
            int currentNodeId = targetNodeId;
            while (currentNodeId != startNodeId)
            {
                path.Add(currentNodeId);
                currentNodeId = bfsResult.ParentMap[currentNodeId];
            }
            path.Add(startNodeId);

            ShortestPathResult resultWithPath = new()
            {
                IsPathexist = true,
                Path = path
            };
            return resultWithPath;


        }
    }
}
