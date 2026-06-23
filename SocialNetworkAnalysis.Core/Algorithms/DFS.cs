using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    internal class DFS : IDFS
    {
        public DFSResult Execute(SocialGraph graph, int startNodeId)
        {
            HashSet<int> visitedSet = new();
            List<int> resultList = new();
            Stack<int> nodesStack = new();

            visitedSet.Add(startNodeId);
            nodesStack.Push(startNodeId);

            while (nodesStack.Count > 0)
            {
                int currentNodeId = nodesStack.Pop();
                resultList.Add(currentNodeId);

                IEnumerable<int> neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (!visitedSet.Contains(neighbor))
                    {
                        visitedSet.Add(neighbor); 
                        nodesStack.Push(neighbor);
                    }
                }
            }

            DFSResult result = new();
            result.VisitedNodes = resultList;
            return result;
        }
    }
}
