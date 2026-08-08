using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Abstractions.Queries
{
    public interface IGetShortestPathService
    {
        List<User> Execute(int startUserId, int targetUserId);
    }
}
