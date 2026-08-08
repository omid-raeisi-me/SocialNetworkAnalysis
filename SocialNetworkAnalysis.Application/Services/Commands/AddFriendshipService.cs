using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class AddFriendshipService : IAddFriendshipService
    {
        private readonly IGraphRuntime _runtime;

        public AddFriendshipService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(Friendship friendship)
        {
            var graph = _runtime.Graph;
            graph.AddFriendship(friendship.FromId, friendship.ToId);

        }
    }
}
