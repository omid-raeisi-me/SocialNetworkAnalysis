using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Result
{
    public class DegreeCentralityResult
    {
        public Dictionary<int, int> degreeOfNodes { get; set; } = new();
        public List<int> centralityNodes { get; set; } = new();
    }
}
