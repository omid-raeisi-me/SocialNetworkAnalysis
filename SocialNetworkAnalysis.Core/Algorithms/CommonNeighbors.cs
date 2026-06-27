using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class CommonNeighbors : ICommonNeighbors
    {
        public CommonNeighborsResult Execute(SocialGraph graph, int nodeA, int nodeB)
        {
            CommonNeighborsResult result = new();
            
            if (nodeA == nodeB)
            {
                result.count = 0;
                return result;
            }

            var allNodes = graph.GetAllNodes();
            if (!allNodes.Contains(nodeA) || !allNodes.Contains(nodeB))
            {
                result.count = 0;
                return result;
            }

            List<int> sharedFriends = new();
            var FriendsOfNodeA = graph.GetFriends(nodeA);
            var FriendsOfNodeB = graph.GetFriends(nodeB);

            if (FriendsOfNodeA == null || FriendsOfNodeB == null || !FriendsOfNodeA.Any() || !FriendsOfNodeB.Any())
            {
                result.count = 0;
                return result;
            }

            HashSet<int> setB = new(FriendsOfNodeB);

            foreach (var friendA in FriendsOfNodeA)
            {
                if (setB.Contains(friendA))
                {
                    sharedFriends.Add(friendA);
                }
        
            }

            result.SharedNeighbors = sharedFriends;
            result.count = sharedFriends.Count;

            return result;
        }
    }
}
