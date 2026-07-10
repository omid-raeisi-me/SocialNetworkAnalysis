using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class ClosenessCentrality : IClosenessCentrality
    {

        private Dictionary<int, int> ClosenessBFS(SocialGraph graph, int startNodeId)
        {
            Dictionary<int, int> distances = new();
            Queue<int> nodesQueue = new();

            nodesQueue.Enqueue(startNodeId);
            distances[startNodeId] = 0;
            while (nodesQueue.Count > 0) 
            {
                int currentNodeId = nodesQueue.Dequeue();
                int currentDistance = distances[currentNodeId];

                var neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (!distances.ContainsKey(neighbor))
                    { 
                        distances[neighbor] = currentDistance + 1;
                        nodesQueue.Enqueue(neighbor);
                    }
                }
            }
        
            return distances;
        }



        public ClosenessCentralityResult Execute(SocialGraph graph)
        {
            ClosenessCentralityResult result = new();
            var allNodes = graph.GetUsers();
            double maxScore = -1.0;

            if (allNodes.Count() <= 1)
            { 
                result.maxScore = 0;
                return result;
            }

            foreach (var startNodeId in allNodes)
            {
                Dictionary<int, int> distances = ClosenessBFS(graph, startNodeId);
                int sumOfDistances = 0;
                bool isConnectedToAll = true;

                foreach (var nodeId in allNodes)
                {
                    if (distances.ContainsKey(nodeId))
                    {
                        sumOfDistances += distances[nodeId];
                    }
                    else
                    {
                        isConnectedToAll = false;
                        break;
                    }
                }
                double closenessScore = 0.0;
                if (isConnectedToAll && sumOfDistances > 0)
                { 
                    closenessScore = (double)(allNodes.Count() - 1) / sumOfDistances;
                }

                result.closenessScore.Add(startNodeId, closenessScore);

                if (closenessScore > maxScore)
                {
                    maxScore = closenessScore;
                    result.CentralityNodes.Clear();
                    result.CentralityNodes.Add(startNodeId);
                }
                else if (closenessScore == maxScore)
                {
                    result.CentralityNodes.Add(startNodeId);
                }
            }
            
            result.maxScore = maxScore;
            return result;
        }
    }
}



