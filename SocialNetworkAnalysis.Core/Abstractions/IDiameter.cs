using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Abstractions
{
    public interface IDiameter
    {
        DiameterResult Execute(SocialGraph graph);
    }
}
