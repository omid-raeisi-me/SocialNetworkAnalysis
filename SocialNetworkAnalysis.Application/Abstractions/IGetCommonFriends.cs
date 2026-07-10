using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions
{
    public interface IGetCommonFriends
    {
        List<User> Execute(int userAId, int userBId);
    }
}
