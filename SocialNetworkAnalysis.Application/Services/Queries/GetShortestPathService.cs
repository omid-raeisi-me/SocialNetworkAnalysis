using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetShortestPathService : IGetShortestPathService
    {
        private readonly IGraphRuntime _runtime;
        private readonly IShortestPath _shortestPathAlgorithm;

        public GetShortestPathService(IGraphRuntime runtime, IShortestPath shortestPathAlgorithm)
        {
            _runtime = runtime;
            _shortestPathAlgorithm = shortestPathAlgorithm;
        }

        public List<User> Execute(int startUserId, int targetUserId)
        {
            var graph = _runtime.Graph;
            List<User> finalPath = new();

                var result = _shortestPathAlgorithm.Execute(graph, startUserId, targetUserId);

                if (result.IsPathExist)
                {
                    foreach (var id in result.Path)
                    {
                        finalPath.Add(new User
                        {
                            Id = id,
                            Name = graph.GetUserName(id) 
                        });
                    }
                }

                return finalPath;
        }
    }
}
