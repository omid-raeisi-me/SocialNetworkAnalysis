using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetFriendSuggestionsService : IGetFriendSuggestionsService
    {
        private readonly IGraphRuntime _runtime;
        private readonly ILinkPrediction _linkPrediction; 

        public GetFriendSuggestionsService(IGraphRuntime runtime, ILinkPrediction linkPrediction)
        {
            _runtime = runtime;
            _linkPrediction = linkPrediction;
        }

        public FriendSuggestionDto Execute(int userId, int topK = 5)
        {
            var graph = _runtime.Graph;
            FriendSuggestionDto appResult = new();

                var coreResult = _linkPrediction.Execute(graph, userId, topK);

                if (coreResult == null) return appResult;

                if (coreResult.commonNeighborsRecommendations != null)
                {
                    foreach (var item in coreResult.commonNeighborsRecommendations)
                    {
                        appResult.CommonNeighbors.Add(new SuggestedUser
                        {
                            User = new User { Id = item.targetNodeId, Name = graph.GetUserName(item.targetNodeId) },
                            Score = item.score
                        });
                    }
                }

                if (coreResult.jaccardRecommendations != null)
                {
                    foreach (var item in coreResult.jaccardRecommendations)
                    {
                        appResult.Jaccard.Add(new SuggestedUser
                        {
                            User = new User { Id = item.targetNodeId, Name = graph.GetUserName(item.targetNodeId) },
                            Score = item.score
                        });
                    }
                }

                if (coreResult.adamicAdarRecommendations != null)
                {
                    foreach (var item in coreResult.adamicAdarRecommendations)
                    {
                        appResult.AdamicAdar.Add(new SuggestedUser
                        {
                            User = new User { Id = item.targetNodeId, Name = graph.GetUserName(item.targetNodeId) },
                            Score = item.score
                        });
                    }
                }

                return appResult;
        }
    }
}
