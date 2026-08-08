using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class ClosenessCentralityDto
    {
        public Dictionary<string, double> ClosenessScores { get; set; } = new();
        public List<string> CentralityNodes { get; set; } = new();
        public double MaxScore { get; set; }
    }
}
