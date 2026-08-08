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
            return _runtime.ExecuteSnapshotAsync(graph =>
            {
                CommunityDetectionDto response = new();

                var coreResult = _communityDetectionAlgorithm.Execute(graph);

                if (coreResult == null) return response;

                if (coreResult.LocalCommunities != null)
                {
                    foreach (var coreGroup in coreResult.LocalCommunities)
                    {
                        var nameGroup = new List<string>();
                        foreach (var userId in coreGroup)
                        {
                            nameGroup.Add(graph.GetUserName(userId));
                        }
                        response.LocalCommunities.Add(nameGroup);
                    }
                    response.LocalCommunitiesCount = coreResult.LocalCommunitiesCount;
                }

                if (coreResult.GlobalCommunities != null)
                {
                    foreach (var coreGroup in coreResult.GlobalCommunities)
                    {
                        var nameGroup = new List<string>();
                        foreach (var userId in coreGroup)
                        {
                            nameGroup.Add(graph.GetUserName(userId));
                        }
                        response.GlobalCommunities.Add(nameGroup);
                    }
                    response.GlobalCommunitiesCount = coreResult.GlobalCommunitiesCount;
                }

                return response;
            }).GetAwaiter().GetResult();
        }
    }
}
