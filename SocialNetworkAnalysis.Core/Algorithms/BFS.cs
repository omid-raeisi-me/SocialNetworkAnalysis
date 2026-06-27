using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class BFS : IBFS
    {
        public BFSResult Execute(SocialGraph graph, int startNodeId)
        {
            HashSet<int> visitedSet = new();
            List<int> resultList = new();
            Queue<int> nodesQueue = new();
            Dictionary<int, int> distancesDictionary = new(); 

            nodesQueue.Enqueue(startNodeId);
            visitedSet.Add(startNodeId);
            distancesDictionary[startNodeId] = 0;

            while (nodesQueue.Count > 0) 
            {
                int currentNodeId = nodesQueue.Dequeue();
                resultList.Add(currentNodeId);
                int currentDistances = distancesDictionary[currentNodeId];
                IEnumerable<int> neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (!visitedSet.Contains(neighbor))
                    {
                        visitedSet.Add(neighbor);
                        nodesQueue.Enqueue(neighbor);
                        distancesDictionary[neighbor] = currentDistances + 1;
                    }
                }
            }

            BFSResult result = new()
            {
                VisitedNodes = resultList,
                Distances = distancesDictionary
            };
            
            return result;
        }
    }
}
