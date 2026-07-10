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

        public GetUserFriends(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public List<User> Execute(int userId)
        {
            return _runtime.ExecuteRead(graph =>
            {
                List<User> friendsList = new();

                foreach (var friendId in graph.GetFriends(userId))
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
