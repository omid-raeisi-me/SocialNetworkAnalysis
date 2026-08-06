using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetBetweennessCentralityService : IGetBetweennessCentralityService
    {
        private readonly IGraphRuntime _runtime;
        private readonly IBetweennessCentrality _betweennessCentralityAlgorithm;

        public GetBetweennessCentralityService(IGraphRuntime runtime, IBetweennessCentrality betweennessCentralityAlgorithm)
        {
            _runtime = runtime;
            _betweennessCentralityAlgorithm = betweennessCentralityAlgorithm;
        }

        public BetweennessCentralityResponse Execute()
        {
            return _runtime.ExecuteSnapshotAsync(graph =>
            {
                var response = new BetweennessCentralityResponse();

                var coreResult = _betweennessCentralityAlgorithm.Execute(graph);

                if (coreResult == null) return response;

                if (coreResult.mostInfluentialNodes != null)
                {
                    foreach (var nodeId in coreResult.mostInfluentialNodes)
                    {
                        response.MostInfluentialNodes.Add(graph.GetUserName(nodeId));
                    }
                }

                if (coreResult.centralityScores != null)
                {
                    var sortedScores = coreResult.centralityScores
                        .OrderByDescending(x => x.Value)
                        .ToList();

                    foreach (var pair in sortedScores)
                    {
                        string userName = graph.GetUserName(pair.Key);
                        double roundedScore = Math.Round(pair.Value, 4);
                        response.CentralityScores.Add(userName, roundedScore);
                    }
                }

                response.MaxScore = Math.Round(coreResult.maxScore, 4);

                return response;
            }).GetAwaiter().GetResult();
        }
    }
}
