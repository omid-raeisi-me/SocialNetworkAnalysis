using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.DTOs
{
    public class BetweennessCentralityDto
    {
        public Dictionary<User, double> CentralityScores { get; set; } = new();
        public List<User> MostInfluentialNodes { get; set; } = new();
        public double MaxScore { get; set; }
    }
}
