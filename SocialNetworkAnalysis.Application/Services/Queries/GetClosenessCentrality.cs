using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetClosenessCentrality : IGetClosenessCentrality
    {
        private readonly IGraphRuntime _runtime;
        private readonly IClosenessCentrality _closenessCentralityAlgorithm;

        public GetClosenessCentrality(IGraphRuntime runtime, IClosenessCentrality closenessCentralityAlgorithm)
        {
            _runtime = runtime;
            _closenessCentralityAlgorithm = closenessCentralityAlgorithm;
        }

        public ClosenessCentralityResponse Execute()
        {
            return _runtime.ExecuteRead(graph =>
            {
                var response = new ClosenessCentralityResponse();

                var coreResult = _closenessCentralityAlgorithm.Execute(graph);

                if (coreResult == null) return response;

                if (coreResult.CentralityNodes != null)
                {
                    foreach (var nodeId in coreResult.CentralityNodes)
                    {
                        response.CentralityNodes.Add(graph.GetUserName(nodeId));
                    }
                }

                if (coreResult.closenessScore != null)
                {
                    var sortedScores = coreResult.closenessScore
                        .OrderByDescending(x => x.Value)
                        .ToList();

                    foreach (var pair in sortedScores)
                    {
                        string userName = graph.GetUserName(pair.Key);
                        double roundedScore = Math.Round(pair.Value, 4); 
                        response.ClosenessScores.Add(userName, roundedScore);
                    }
                }

                response.MaxScore = Math.Round(coreResult.maxScore, 4);

                return response;
            });
        }
    }
}
