using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IGraphRuntime _runtime;

        public RemoveUserService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(int userId)
        {
            _runtime.ExecuteWrite(graph =>
            {
                graph.RemoveUser(userId);
            });
        }
    }
}
