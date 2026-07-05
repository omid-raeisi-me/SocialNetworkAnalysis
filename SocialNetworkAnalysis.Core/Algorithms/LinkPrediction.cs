using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class LinkPrediction : ILinkPrediction
    {
        private readonly ICommonNeighbors _commonNeighbors;
        private readonly IJaccard _jaccard;
        private readonly IAdamicAdar _adamicAdar;

        public LinkPrediction(
            ICommonNeighbors CommonNeighbors ,
            IJaccard Jaccard,
            IAdamicAdar AdamicAdar)
        {
            _commonNeighbors = CommonNeighbors;
            _jaccard = Jaccard;
            _adamicAdar = AdamicAdar;
        }

        public LinkPredictionResult Execute(SocialGraph graph, int userId, int topK)
        {
            LinkPredictionResult result = new();
            var allNodes = graph.GetUsers();
            if (!allNodes.Contains(userId))
            {   
                return result;    
            }

            var userFriends = graph.GetFriends(userId);
            HashSet<int> userFriendsSet = new(userFriends);

            List<RecommendationItem> commonNeighborsPredictList = new();
            List<RecommendationItem> jaccardPredictList= new();
            List<RecommendationItem> adamicAdarPredictList = new();

            foreach (var node in allNodes)
            {
                if (node == userId || userFriendsSet.Contains(node))
                {
                    continue;
                }

                var commonNeighborsResult = _commonNeighbors.Execute(graph, userId, node);
                if (commonNeighborsResult.count > 0)
                {
                    RecommendationItem r = new()
                    {
                        targetNodeId = node,
                        score = commonNeighborsResult.count
                    };
                    commonNeighborsPredictList.Add(r);
                }

                var jaccardResult = _jaccard.Execute(graph, userId, node);
                if (jaccardResult.Score > 0)
                {
                    RecommendationItem r = new()
                    {
                        targetNodeId = node,
                        score = jaccardResult.Score
                    };
                    jaccardPredictList.Add(r);
                }

                var adamicAdarResult = _adamicAdar.Execute(graph, userId, node);
                if (adamicAdarResult.Score > 0)
                {
                    RecommendationItem r = new()
                    {
                        targetNodeId = node,
                        score = adamicAdarResult.Score
                    };
                    adamicAdarPredictList.Add(r);
                }
            }

            result.commonNeighborsRecommendations = commonNeighborsPredictList.OrderByDescending(x => x.score).Take(topK).ToList();
            result.jaccardRecommendations = jaccardPredictList.OrderByDescending(x => x.score).Take(topK).ToList();
            result.adamicAdarRecommendations = adamicAdarPredictList.OrderByDescending(x => x.score).Take(topK).ToList();

            return result;
        }
    }
}
