using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    internal class ShortestPath : IShortestPath
    {
        public Dictionary<int,int> ShortestPathBFS(SocialGraph graph, int startNodeId, int targetNodeId)
        {
            HashSet<int> visitedSet = new();
            Queue<int> nodesQueue = new();
            Dictionary<int, int> parentMapDictionary = new();

            nodesQueue.Enqueue(startNodeId);
            visitedSet.Add(startNodeId); 

            bool targetFound = false;

            while (nodesQueue.Count > 0 && !targetFound)
            {
                int currentNodeId = nodesQueue.Dequeue();

                IEnumerable<int> neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (!visitedSet.Contains(neighbor))
                    {
                        visitedSet.Add(neighbor); 
                        nodesQueue.Enqueue(neighbor);

                        if (!parentMapDictionary.ContainsKey(neighbor))
                        {
                            parentMapDictionary.Add(neighbor, currentNodeId);
                        }

                        if (neighbor == targetNodeId)
                        {
                            targetFound = true;
                            break;
                        }
                    }
                }
            }

            return parentMapDictionary;
        }

        public ShortestPathResult Execute(SocialGraph graph, int startNodeId, int targetNodeId)
        {
            if (startNodeId == targetNodeId)
            {
                List<int> p = new();
                p.Add(startNodeId);
                return new ShortestPathResult
                {
                    IsPathExist = true,
                    Path = p
                };
            }
      
            Dictionary<int,int> ParentDictionary = ShortestPathBFS(graph, startNodeId, targetNodeId);
            if (!ParentDictionary.ContainsKey(targetNodeId))
            {
                return new ShortestPathResult 
                { 
                    IsPathExist = false 
                };
            }

            List<int> path = new();
            int currentNodeId = targetNodeId;
            while (currentNodeId != startNodeId)
            {
                path.Add(currentNodeId);
                currentNodeId = ParentDictionary[currentNodeId];
            }
            path.Add(startNodeId);

            path.Reverse();

            return new ShortestPathResult
            {
                IsPathExist = true,
                Path = path
            };
        }
    }
}