using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class BetweennessCentrality : IBetweennessCentrality
    {
        private Dictionary<int, double> BrandesBFS(SocialGraph graph, int startedNodeId, IEnumerable<int> allNodes) 
        {
            Stack<int> stack = new();
            Queue<int> queue = new();
            Dictionary<int, List<int>> parents = new();
            Dictionary<int, int> pathCounts = new();
            Dictionary<int, int> distances = new();

            foreach (int nodeId in allNodes)
            {
                parents[nodeId] = new List<int>();
                pathCounts[nodeId] = 0;
                distances[nodeId] = -1;
            }

            distances[startedNodeId] = 0;
            pathCounts[startedNodeId] = 1;
            queue.Enqueue(startedNodeId);

            while (queue.Count > 0)
            { 
                int currentNodeId = queue.Dequeue();
                stack.Push(currentNodeId);

                var neighbors = graph.GetFriends(currentNodeId);
                foreach (int neighbor in neighbors)
                {
                    if (distances[neighbor] == -1)
                    {
                        distances[neighbor] = distances[currentNodeId] + 1;
                        queue.Enqueue(neighbor);
                    }
                    if (distances[neighbor] == distances[currentNodeId] + 1)
                    {
                        pathCounts[neighbor] += pathCounts[currentNodeId];
                        parents[neighbor].Add(currentNodeId);
                        
                    }
                }
            }

            Dictionary<int, double> dependencies = new();
            foreach (int nodeId in allNodes)
            { 
                dependencies[nodeId] = 0.0;
            }

            while (stack.Count > 0)
            { 
                int node = stack.Pop();
                foreach (int parent in parents[node])
                {
                    double fraction = (double)(pathCounts[parent]) / pathCounts[node];
                    dependencies[parent] += fraction * (1.0 + dependencies[node]);
                }
            
            }

            return dependencies;
        }

        
        
        public BetweennessCentralityResult Execute(SocialGraph graph)
        {
            BetweennessCentralityResult result = new();
            var allNodes = graph.GetAllNodes();
            int n = allNodes.Count();

            if (n <= 2) 
            {
                result.maxScore = 0;
                return result;
            }

            Dictionary<int, double> nodeScores = new();
            foreach (int nodeId in allNodes)
            {
                nodeScores[nodeId] = 0.0;
            }

            foreach (int startNodeId in allNodes)
            {
                Dictionary<int, double> dependencie = BrandesBFS(graph, startNodeId, allNodes);

                foreach (int nodeId in allNodes)
                {

                    if (nodeId != startNodeId)
                    {
                        nodeScores[nodeId] += dependencie[nodeId];
                    }
                }

            }

            double maxScore = -1.0;

            foreach (int nodeId in allNodes)
            {
                double finalScore = nodeScores[nodeId] / 2.0;
                result.centralityScores.Add(nodeId, finalScore);

                if (finalScore > maxScore)
                {
                    maxScore = finalScore;
                    result.mostInfluentialNodes.Clear();
                    result.mostInfluentialNodes.Add(nodeId);
                }
                else if (finalScore == maxScore)
                {
                    result.mostInfluentialNodes.Add(nodeId);
                }
            
            }

            result.maxScore = maxScore;
            return result;
        }
    }
}

