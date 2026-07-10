using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class AddFriendship : IAddFriendship
    {
        private readonly IGraphRuntime _runtime;

        public AddFriendship(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(FriendShip friendship)
        {
            _runtime.ExecuteWrite(graph =>
            {
                graph.AddFriendship(friendship.FromId, friendship.ToId);
            });
        }
    }
}
