using SocialNetworkAnalysis.Application.Contracts.Runtime;
using SocialNetworkAnalysis.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetWholeGraphService : IGetWholeGraphService
    {
        private readonly IGraphRuntime _runtime;

        public GetWholeGraphService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public WholeGraphResponse Execute()
        {
            return _runtime.ExecuteRead(graph =>
            {
                var response = new WholeGraphResponse();

                foreach (var userId in graph.GetUsers())
                {
                    var userName = graph.GetUserName(userId);

                    response.Users.Add(new User
                    {
                        Id = userId,
                        Name = userName
                    });
                }

                foreach (var userId in graph.GetUsers())
                {
                    foreach (var friendId in graph.GetFriends(userId))
                    {
                        if (userId < friendId)
                        {
                            response.Friendships.Add(new Friendship(userId, friendId));
                        }
                    }
                }

                return response;
            });
        }
    }
}
