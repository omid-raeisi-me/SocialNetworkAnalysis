using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetCommunityDetectionService : IGetCommunityDetectionService
    {
        private readonly IGraphRuntime _runtime;
        private readonly ICommunityDetection _communityDetectionAlgorithm;

        public GetCommunityDetectionService(IGraphRuntime runtime, ICommunityDetection communityDetectionAlgorithm)
        {
            _runtime = runtime;
            _communityDetectionAlgorithm = communityDetectionAlgorithm;
        }

        public CommunityDetectionDto Execute()
        {
            var graph = _runtime.Graph;
            CommunityDetectionDto response = new();

            var coreResult = _communityDetectionAlgorithm.Execute(graph);

            if (coreResult == null) return response;

            if (coreResult.LocalCommunities != null)
            {
                foreach (var coreGroup in coreResult.LocalCommunities)
                {
                    var group = new List<User>();
                    foreach (var userId in coreGroup)
                    {
                        group.Add(new User()
                        {
                            Id = userId,
                            Name = graph.GetUserName(userId)
                        });
                    }
                    response.LocalCommunities.Add(group);
                }
                response.LocalCommunitiesCount = coreResult.LocalCommunitiesCount;
            }

            if (coreResult.GlobalCommunities != null)
            {
                foreach (var coreGroup in coreResult.GlobalCommunities)
                {
                    var group = new List<User>();
                    foreach (var userId in coreGroup)
                    {
                        group.Add(new User()
                        {
                            Id = userId,
                            Name = graph.GetUserName(userId)
                        });
                    }
                    response.GlobalCommunities.Add(group);
                }
                response.GlobalCommunitiesCount = coreResult.GlobalCommunitiesCount;
            }

            return response;
        }
    }
}
