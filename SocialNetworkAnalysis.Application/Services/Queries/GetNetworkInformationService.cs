using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetNetworkInformationService : IGetNetworkInformationService
    {
        private readonly IGraphRuntime _runtime;
        private readonly INetworkInformation _networkInformation;

        public GetNetworkInformationService(IGraphRuntime runtime, INetworkInformation networkInformation)
        {
            _runtime = runtime;
            _networkInformation = networkInformation;
        }

        public NetworkInformationResponse Execute()
        {
            return _runtime.ExecuteSnapshotAsync(graph =>
            {
                var coreResult = _networkInformation.Execute(graph);
                if (coreResult == null) return new NetworkInformationResponse();

                var response = new NetworkInformationResponse
                {
                    TotalUserCount = coreResult.TotalUserCount,
                    TotalFriendshipCount = coreResult.TotalFriendshipCount,
                    AverageRelationPerUser = Math.Round(coreResult.AverageRelationPerUser, 2),
                    Density = Math.Round(coreResult.density, 4),
                    Diameter = coreResult.diameter
                };

                if (coreResult.UsersWithMostFriends != null)
                {
                    foreach (var pair in coreResult.UsersWithMostFriends)
                    {
                        response.Influencers.Add(graph.GetUserName(pair.Key), pair.Value);
                    }
                }

                if (coreResult.LargestFriendshipGroup != null)
                {
                    foreach (var id in coreResult.LargestFriendshipGroup)
                    {
                        response.LargestFriendshipGroup.Add(graph.GetUserName(id));
                    }
                }

                return response;
            }).GetAwaiter().GetResult();
        }
    }
}