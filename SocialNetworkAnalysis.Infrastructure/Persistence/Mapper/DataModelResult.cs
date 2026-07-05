using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Mapper
{
    public class DataModelResult
    {
        public List<User> Users { get; set; }
        public List<FriendShip> FriendShips { get; set; }
    }
}
