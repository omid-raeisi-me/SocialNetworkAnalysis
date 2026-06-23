using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Result
{
    public class ConnectedComponentsResult
    {
        public List<List<int>> Components { get; set; } = new();
        public int ComponentsCount { get; set; }
    }
}
