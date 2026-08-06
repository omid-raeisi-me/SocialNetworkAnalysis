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

            var friendsA = graph.GetFriends(nodeA) as HashSet<int>;
            var friendsB = graph.GetFriends(nodeB) as HashSet<int>;

            if (friendsA == null || friendsB == null || friendsA.Count == 0 || friendsB.Count == 0)
            {
                result.Score = 0;
                return result;
            }

            HashSet<int> smallerSet;
            HashSet<int> largerSet;

            if (friendsA.Count <= friendsB.Count)
            {
                smallerSet = friendsA;
                largerSet = friendsB;
            }
            else
            {
                smallerSet = friendsB;
                largerSet = friendsA;
            }

            double totalScore = 0;

            foreach (int friend in smallerSet)
            {
                if (largerSet.Contains(friend))
                {
                    var friendFriends = graph.GetFriends(friend) as HashSet<int>;
                    int degree = friendFriends?.Count ?? 0;

                    if (degree > 1)
                    {
                        totalScore += 1.0 / Math.Log(degree);
                    }
                }
            }

            result.Score = totalScore;
            return result;
        }
    }
}
