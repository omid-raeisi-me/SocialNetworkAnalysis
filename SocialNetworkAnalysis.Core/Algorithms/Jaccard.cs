using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class Jaccard : IJaccard
    {
        public JaccardResult Execute(SocialGraph graph, int nodeA, int nodeB)
        {
            JaccardResult result = new();

            if (nodeA == nodeB)
            {
                result.Score = 0;
                return result;
            }

            var allNodes = graph.GetAllNodes();
            if (!allNodes.Contains(nodeA) || !allNodes.Contains(nodeB))
            {
                result.Score = 0;
                return result;
            }

            int commonFriendsCount = 0
            var FriendsOfNodeA = graph.GetFriends(nodeA);
            var FriendsOfNodeB = graph.GetFriends(nodeB);

            if (FriendsOfNodeA == null || FriendsOfNodeB == null || !FriendsOfNodeA.Any() || !FriendsOfNodeB.Any())
            {
                result.Score = 0;
                return result;
            }

            HashSet<int> setB = new(FriendsOfNodeB);

            foreach (int friend in FriendsOfNodeA)
            {
                if (setB.Contains(friend))
                {
                    commonFriendsCount++;
                }
            }

            int unionFriendsCount = (FriendsOfNodeA.Count()) + (FriendsOfNodeB.Count()) - commonFriendsCount;
            double finalScore = (double)commonFriendsCount / (double)unionFriendsCount;

            result.Score = finalScore;
            return result;
        }
    }
}
