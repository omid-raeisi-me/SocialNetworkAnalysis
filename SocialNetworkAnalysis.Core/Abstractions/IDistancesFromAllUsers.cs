using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Abstractions
{
    public interface IDistancesFromAllUsers
    {
        DistancesFromAllUsersResult Execute(SocialGraph graph, int startUser);
    }
}