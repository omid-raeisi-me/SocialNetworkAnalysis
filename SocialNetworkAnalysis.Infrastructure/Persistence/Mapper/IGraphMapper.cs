using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Mapper
{
    public interface IGraphMapper
    {
        SocialGraph ConvertToDomianModel(List<User> users, List<FriendShip> friendShips);
        DataModelResult ConvertToDataModel(SocialGraph graph);
    }
}
