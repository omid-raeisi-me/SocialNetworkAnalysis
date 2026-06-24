using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Result
{
    public class CommonNeighborsResult
    {
        public List<int> SharedNeighbors { get; set; } = new();

        public int count { get; set; }
    }
}
