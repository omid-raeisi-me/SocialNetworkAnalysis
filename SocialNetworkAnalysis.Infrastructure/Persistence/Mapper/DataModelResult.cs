using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Mapper
{
    public class DataModelResult
    {
        public List<User> Users { get; set; }
        public List<Friendship> Friendships { get; set; }
    }
}
