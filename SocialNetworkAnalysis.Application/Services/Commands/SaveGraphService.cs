using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Commands;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class SaveGraphService : ISaveGraphService
    {
        private readonly IGraphRuntime _runtime;

        public SaveGraphService(IGraphRuntime runtime)
        {
            _runtime = runtime;
        }

        public async Task ExecuteAsync()
        {
            await _runtime.SaveAsync();
        }
    }
}
