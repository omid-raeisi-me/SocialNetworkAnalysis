using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Results
{
    public class CommunityDetectionResult
    {
        public List<List<int>> LocalCommunities { get; set; } = new();
        public int LocalCommunitiesCount { get; set; }
        public List<List<int>> GlobalCommunities { get; set; } = new();
        public int GlobalCommunitiesCount { get; set; }
    }
}
