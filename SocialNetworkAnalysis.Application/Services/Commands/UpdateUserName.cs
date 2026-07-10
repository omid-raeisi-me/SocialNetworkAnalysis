using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class UpdateUserName : IUpdateUserName
    {
        private readonly IGraphRuntime _runtime;

        public UpdateUserName(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(User user)
        {
            _runtime.ExecuteWrite(graph =>
            {
                graph.UpdateUserName(user.Id, user.Name);
            });
        }
    }
}
