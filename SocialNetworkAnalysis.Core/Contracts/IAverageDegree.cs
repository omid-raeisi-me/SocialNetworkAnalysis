using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Contracts
{
    public interface IAverageDegree
    {
        AverageDegreeResult Execute(SocialGraph graph);
    }
}
