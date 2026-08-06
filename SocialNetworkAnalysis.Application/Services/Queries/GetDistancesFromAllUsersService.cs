using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetDistancesFromAllUsersService : IGetDistancesFromAllUsersService
    {
        private readonly IGraphRuntime _runtime;
        private readonly IDistancesFromAllUsers _distancesAlgorithm;

        public GetDistancesFromAllUsersService(IGraphRuntime runtime, IDistancesFromAllUsers distancesAlgorithm)
        {
            _runtime = runtime;
            _distancesAlgorithm = distancesAlgorithm;
        }

        public List<UserDistanceDto> Execute(int startUserId)
        {
            return _runtime.ExecuteRead(graph =>
            {
                var responseList = new List<UserDistanceDto>();

                var coreResult = _distancesAlgorithm.Execute(graph, startUserId);

                if (coreResult?.distances == null) return responseList;

                foreach (var pair in coreResult.distances)
                {
                    string userName = graph.GetUserName(pair.Key);
                    double finalDistance;

                    if (double.IsPositiveInfinity(pair.Value))
                    {
                        finalDistance = double.NegativeInfinity;
                    }
                    else
                    {
                        finalDistance = pair.Value;
                    }

                    responseList.Add(new UserDistanceDto
                    {
                        Name = userName,
                        Distance = finalDistance
                    });
                }

                return responseList;
            });
        }
    }
}
