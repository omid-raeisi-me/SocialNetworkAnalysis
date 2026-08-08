using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class BetweennessCentralityDto
    {
        public Dictionary<string, double> CentralityScores { get; set; } = new();
        public List<string> MostInfluentialNodes { get; set; } = new();
        public double MaxScore { get; set; }
    }
}
