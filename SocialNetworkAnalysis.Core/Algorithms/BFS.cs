using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    internal class BFS : IBFS
    {
        public BFSResult Execute(SocialGraph graph, int startNodeId)
        {
            HashSet<int> visitedSet = new();
            List<int> resultList = new();
            Queue<int> nodesQueue = new();
            Dictionary<int, int> parentMapDictionary = new();
            nodesQueue.Enqueue(startNodeId);

            while (nodesQueue.Count > 0)
            { 
                int currentNodeId = nodesQueue.Dequeue();
                if (visitedSet.Contains(currentNodeId))
                {
                    continue;
                }
                visitedSet.Add(currentNodeId);
                resultList.Add(currentNodeId);

                IEnumerable<int> neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (!(visitedSet.Contains(neighbor)))
                    { 
                        nodesQueue.Enqueue(neighbor);
                        if (!parentMapDictionary.ContainsKey(neighbor))
                        {
                            parentMapDictionary.Add(neighbor, currentNodeId);
                        }
                    }
                }
            }
            BFSResult result = new() {
                VisitedNodes = resultList,
                ParentMap = parentMapDictionary
            };
            
            return result;
        }
    }
}
