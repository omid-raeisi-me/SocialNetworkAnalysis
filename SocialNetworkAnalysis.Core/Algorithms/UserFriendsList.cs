using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class UserFriendsList : IUserFriendsList
    {
        public UserFriendsListResult Execute(SocialGraph graph, int user)
        {   
            List<int> friends = graph.GetFriends(user).ToList();

            UserFriendsListResult result = new()
            {
                listOfFriends = friends
            }; 

            return result;
        }
    }
}
