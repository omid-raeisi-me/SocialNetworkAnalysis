using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Results;

namespace SocialNetworkAnalysis.Core.Abstractions
{
    public interface ICommonNeighbors
    {
        CommonNeighborsResult Execute(SocialGraph graph,int nodeA,int nodeB);
    }
}
