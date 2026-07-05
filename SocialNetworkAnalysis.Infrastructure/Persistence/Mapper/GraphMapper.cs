using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Mapper
{
    public class GraphMapper
    {
        public SocialGraph ConvertToDomianModel(List<User> users, List<FriendShip> friendShips)
        {
            SocialGraph graph = new SocialGraph();

            foreach (var user in users)
            {
                graph.AddUser(user.Id, user.Name);
            }

            foreach (var friendShip in friendShips)
            {
                graph.AddFriendship(friendShip.FromId, friendShip.ToId);
            }

            return graph;
        }

        public DataModelResult ConvertToDataModel(SocialGraph graph)
        {
            var idDictionary = graph.GetUsers().ToList();

            var users = new List<User>(idDictionary.Count);
            var friendShips = new List<FriendShip>(graph.GetEdgeCount());

            foreach (var id in idDictionary)
            {
                users.Add(new User()
                {
                    Id = id,
                    Name = graph.GetUserName(id)
                });
            }

            foreach (var pair in graph.GetUsersHaveFriends())
            {
                foreach (int destination in graph.GetFriends(pair))
                {
                    if (pair < destination)
                    {
                        friendShips.Add(new FriendShip(pair, destination));
                    }
                }
            }

            return new DataModelResult()
            {
                Users = users,
                FriendShips = friendShips
            };
        }
    }
}
