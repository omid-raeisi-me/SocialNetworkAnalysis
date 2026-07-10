using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class Density : IDensity
    {
        public DensityResult Execute(SocialGraph graph)
        {
            DensityResult result = new();
            var allNodes = graph.GetUsers();
            if (allNodes.Count() <= 1)
            { 
                result.density = 0;
                return result;
            }

            int nodeCount = allNodes.Count();
            result.density = 2 * (double)graph.GetEdgeCount() / (nodeCount * (nodeCount - 1));

            return result;
        }
    }
}
