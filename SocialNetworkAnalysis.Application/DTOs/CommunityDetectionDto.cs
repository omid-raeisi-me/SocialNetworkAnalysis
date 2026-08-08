using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class CommunityDetectionDto
    {
        public List<List<User>> LocalCommunities { get; set; } = new();
        public int LocalCommunitiesCount { get; set; }
        public List<List<User>> GlobalCommunities { get; set; } = new();
        public int GlobalCommunitiesCount { get; set; }
    }
}
