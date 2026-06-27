using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Contracts
{
    public interface IPathExist
    {
        PathExistResult Execute(SocialGraph graph, int userA, int userB);
    }
}
