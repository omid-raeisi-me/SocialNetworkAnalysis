using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs.Response
{

    public class NetworkGroup
    {
        public int GroupId { get; set; } 
        public List<User> Members { get; set; } = new();
    }
    public class NetworkGroupsResponse
    {
        public int TotalGroups { get; set; }
        public List<NetworkGroup> Groups { get; set; } = new();
    }
}
