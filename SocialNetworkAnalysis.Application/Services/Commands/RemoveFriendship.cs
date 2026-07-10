using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class RemoveFriendship : IRemoveFriendship
    {
        private readonly IGraphRuntime _runtime;

        public RemoveFriendship(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(FriendShip friendship)
        {
            _runtime.ExecuteWrite(graph =>
            {
                graph.RemoveFriendship(friendship.FromId, friendship.ToId);
            });
        }
    }
}
