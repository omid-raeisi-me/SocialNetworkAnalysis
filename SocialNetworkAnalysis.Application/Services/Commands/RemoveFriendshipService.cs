using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class RemoveFriendshipService : IRemoveFriendshipService
    {
        private readonly IGraphRuntime _runtime;

        public RemoveFriendshipService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(Friendship friendship)
        {
            var graph = _runtime.Graph;
            graph.RemoveFriendship(friendship.FromId, friendship.ToId);
        }
    }
}
