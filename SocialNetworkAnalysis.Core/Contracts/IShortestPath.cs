using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Contracts
{
    public interface IShortestPath
    {
        ShortestPathResult Execute(SocialGraph graph, int startNodeId, int targetNodeId);
        public Dictionary<int, int> ShortestPathBFS(SocialGraph graph, int startNodeId, int targetNodeId);
    }
}
