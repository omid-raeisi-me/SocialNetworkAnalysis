using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.DTOs
{

    public class NetworkGroup
    {
        public int GroupId { get; set; } 
        public List<User> Members { get; set; } = new();
    }
    public class NetworkGroupsDto
    {
        public int TotalGroups { get; set; }
        public List<NetworkGroup> Groups { get; set; } = new();
    }
}
