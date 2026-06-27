using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Result
{
    public class LinkPredictionResult
    {
        public List<RecommendationItem> commonNeighborsRecommendations { get; set; } = new();
        public List<RecommendationItem> jaccardRecommendations { get; set; } = new();
        public List<RecommendationItem> adamicAdarRecommendations { get; set; } = new();
    }

    public class RecommendationItem
    {
        public int targetNodeId { get; set; }
        public double score { get; set; }
    }
}
