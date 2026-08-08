using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class SuggestedUser
    {
        public User User { get; set; }
        public double Score { get; set; }
    }

    public class FriendSuggestionDto
    {
        public List<SuggestedUser> CommonNeighbors { get; set; } = new();
        public List<SuggestedUser> Jaccard { get; set; } = new();
        public List<SuggestedUser> AdamicAdar { get; set; } = new();
    }
}
