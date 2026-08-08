using SocialNetworkAnalysis.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class WholeGraphDto
    {
        public List<User> Users { get; set; } = new();
        public List<Friendship> Friendships { get; set; } = new();
    }
}
