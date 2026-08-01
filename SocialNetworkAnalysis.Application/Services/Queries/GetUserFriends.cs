using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetUserFriends : IGetUserFriends
    {
        private readonly IGraphRuntime _runtime;
        private readonly IUserFriendsList _userFriendsList; 

        public GetUserFriends(IGraphRuntime runtime, IUserFriendsList userFriendsList)
        {
            _runtime = runtime;
            _userFriendsList = userFriendsList;
        }

        public List<User> Execute(int userId)
        {
            return _runtime.ExecuteRead(graph =>
            {
                var algorithmResult = _userFriendsList.Execute(graph, userId);

                List<User> friendsList = new();

                if (algorithmResult?.listOfFriends == null)
                {
                    return friendsList;
                }

                foreach (var friendId in algorithmResult.listOfFriends)
                {
                    friendsList.Add(new User
                    {
                        Id = friendId,
                        Name = graph.GetUserName(friendId)
                    });
                }

                return friendsList;
            });
        }
    }
}
