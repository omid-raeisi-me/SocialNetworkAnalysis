using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class AddUserService : IAddUserService
    {
        private readonly IGraphRuntime _runtime;

        public AddUserService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(User user)
        {
            _runtime.ExecuteWrite(graph =>
            {
                graph.AddUser(user.Id, user.Name);
            });
        }
    }
}
