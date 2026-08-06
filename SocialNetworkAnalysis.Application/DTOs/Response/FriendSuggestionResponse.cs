using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs.Response
{
    public class SuggestedUser
    {
        public User User { get; set; }
        public double Score { get; set; }
    }

    public class FriendSuggestionResponse
    {
        public List<SuggestedUser> CommonNeighbors { get; set; } = new();
        public List<SuggestedUser> Jaccard { get; set; } = new();
        public List<SuggestedUser> AdamicAdar { get; set; } = new();
    }
}
