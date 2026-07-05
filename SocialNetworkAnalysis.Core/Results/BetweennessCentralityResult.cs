using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Results
{
    public class BetweennessCentralityResult
    {
        public Dictionary<int, double> centralityScores { get; set; } = new();

        public List<int> mostInfluentialNodes { get; set; } = new();

        public double maxScore { get; set; }
    }
}
