using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions
{
    public interface IGetDistancesFromAllUsersService
    {
        List<UserDistanceDto> Execute(int startUserId);
    }
}
