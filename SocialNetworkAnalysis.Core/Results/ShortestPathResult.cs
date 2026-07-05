using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Results
{
    public class ShortestPathResult
    {
        public bool IsPathExist { get; set; }
        public List<int> Path { get; set; } = new();
    }
}
