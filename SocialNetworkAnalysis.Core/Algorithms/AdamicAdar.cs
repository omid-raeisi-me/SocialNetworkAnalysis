using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class AdamicAdar : IAdamicAdar
    {
        public AdamicAdarResult Execute(SocialGraph graph, int nodeA, int nodeB)
        {
            AdamicAdarResult result = new();

            if (nodeA == nodeB)
            {
                result.Score = 0;
                return result;
            }

            var allNodes = graph.GetUsers();
            if (!allNodes.Contains(nodeA) || !allNodes.Contains(nodeB))
            {
                result.Score = 0;
                return result;
            }

            List<int> commonFriends = new();
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
                    commonFriends.Add(friend);
                }
            }

            double totalScore = 0;

            foreach (int friend in commonFriends)
            {
                int degree = graph.GetFriends(friend).Count();
                totalScore += 1.0 / Math.Log(degree);
            }

            result.Score = totalScore;

            return result;
        }
    }
}
