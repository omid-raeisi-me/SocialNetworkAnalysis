using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Models
{
    public class SocialGraph
    {
        private Dictionary<int, string> _users = new();
        private Dictionary<int, HashSet<int>> _adjacency = new();

        public void AddUser(int userId, string name)
        {
            if (_users.ContainsKey(userId))
                return;

            _users[userId] = name;
            _adjacency[userId] = new HashSet<int>();
        }

        public void RemoveUser(int userId)
        {
            if (!_users.ContainsKey(userId))
                return;

            foreach (var friendId in _adjacency[userId])
            {
                _adjacency[friendId].Remove(userId);
            }

            _adjacency.Remove(userId);
            _users.Remove(userId);
        }

        public void AddFriendship(int user1Id, int user2Id)
        {
            if (user1Id == user2Id)
                return;

            if (!_users.ContainsKey(user1Id))
                throw new KeyNotFoundException();

            if (!_users.ContainsKey(user2Id))
                throw new KeyNotFoundException();

            _adjacency[user1Id].Add(user2Id);
            _adjacency[user2Id].Add(user1Id);
        }

        public void RemoveFriendship(int user1Id, int user2Id)
        {
            if (!_users.ContainsKey(user1Id))
                return;

            if (!_users.ContainsKey(user2Id))
                return;

            _adjacency[user1Id].Remove(user2Id);
            _adjacency[user2Id].Remove(user1Id);
        }

        public IEnumerable<int> GetFriends(int userId)
        {
            return _adjacency[userId];
        }

        public IEnumerable<int> GetAllNodes()
        {
            return _users.Keys;
        }
    }
}
