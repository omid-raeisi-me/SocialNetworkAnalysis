using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class AverageDegree : IAverageDegree
    {
        public AverageDegreeResult Execute(SocialGraph graph)
        {
            AverageDegreeResult result = new();
            var allNodes = graph.GetAllNodes();
            if (!allNodes.Any())
            {
                result.AverageDegree = 0;
                return result;
            }

            int totalDegree = 0;
            foreach (var node in allNodes)
            {
                var friends = graph.GetFriends(node);
                totalDegree += friends?.Count() ?? 0;
            }

            int nodeCount = allNodes.Count();
            result.AverageDegree = (double)totalDegree / nodeCount;

            return result;
        }
    }
}
