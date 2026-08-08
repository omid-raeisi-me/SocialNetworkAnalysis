using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class AddUserService : IAddUserService
    {
        private readonly IGraphRuntime _runtime;

        public AddUserService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Execute(string name)
        {
            var graph = _runtime.Graph;
            var id = _runtime.GenerateId();
            graph.AddUser(id, name);
        }
    }
}
