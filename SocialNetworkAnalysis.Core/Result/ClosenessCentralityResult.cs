using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Result
{
    public class ClosenessCentralityResult
    {
        public Dictionary<int, double> closenessScore { get; set; } = new();

        public List<int> CentralityNodes { get; set; } = new();

        public double maxScore { get; set; }
    }
}
