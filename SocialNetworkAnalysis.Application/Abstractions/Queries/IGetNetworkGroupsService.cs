using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions.Queries
{
    public interface IGetNetworkGroupsService
    {
        NetworkGroupsDto Execute();
    }
}
