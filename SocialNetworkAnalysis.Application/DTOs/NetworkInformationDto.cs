using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class NetworkInformationDto
    {
        public int TotalUserCount { get; set; }
        public int TotalFriendshipCount { get; set; }
        public double AverageRelationPerUser { get; set; }
        public List<User> LargestFriendshipGroup { get; set; } = new();
        public double Density { get; set; }
        public int Diameter { get; set; }
        public Dictionary<User, int> Influencers { get; set; } = new();
    }
}
