using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Results
{
    public class NetworkInformationResult
    {
        public int TotalUserCount { get; set; }
        public int TotalFriendshipCount { get; set; }
        public double AverageRelationPerUser { get; set; }
        public List<int> LargestFriendshipGroup { get; set; } = new();
        public Dictionary<int, int> UsersWithMostFriends { get; set; } = new();
        public double density { get; set; }
        public int diameter { get; set; }
    }
}
