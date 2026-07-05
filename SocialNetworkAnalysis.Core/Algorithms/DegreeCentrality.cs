using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class DegreeCentrality : IDegreeCentrality
    {
        public DegreeCentralityResult Execute(SocialGraph graph)
        {
            Dictionary<int, int> DegreeOfNodes = new();
            List<int> CentralityNodes = new();
            int maxDegree = -1;
            var allNodes = graph.GetUsers();

            foreach (var node in allNodes)
            {
                var listOfFriends = graph.GetFriends(node);
                int friendsCount = listOfFriends.Count();
                DegreeOfNodes.Add(node, friendsCount);
                if (friendsCount > maxDegree)
                {
                    maxDegree = friendsCount;
                    CentralityNodes.Clear();
                    CentralityNodes.Add(node);
                }
                else if (friendsCount == maxDegree)
                {
                    CentralityNodes.Add(node);
                }
            }
            DegreeCentralityResult result = new()
            {
                centralityNodes = CentralityNodes,
                degreeOfNodes = DegreeOfNodes
            };

            return result ;
        }
    }
}