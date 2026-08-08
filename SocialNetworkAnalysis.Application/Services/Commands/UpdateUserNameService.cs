using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class UpdateUserNameService : IUpdateUserNameService
    {
        private readonly IGraphRuntime _runtime;

        public UpdateUserNameService(IGraphRuntime runtime)
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
