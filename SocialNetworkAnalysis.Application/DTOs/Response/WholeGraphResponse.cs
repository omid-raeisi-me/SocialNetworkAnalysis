using SocialNetworkAnalysis.Application.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs.Response
{
    public class WholeGraphResponse
    {
        public List<User> Users { get; set; } = new();
        public List<Friendship> Friendships { get; set; } = new();
    }
}
