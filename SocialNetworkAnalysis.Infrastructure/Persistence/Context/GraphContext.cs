using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Context
{
    public class GraphContext
    {
        private IJsonStorage<List<User>> _userStorage;
        private IJsonStorage<List<Friendship>> _friendshipStorage;
        private IJsonStorage<Settings> _settingsStorage;

        public GraphContext(IJsonStorage<List<User>> userStorage,
            IJsonStorage<List<Friendship>> friendshipStorage, IJsonStorage<Settings> dbSettingsStorage)
        {
            _userStorage = userStorage;
            _friendshipStorage = friendshipStorage;
            _settingsStorage = dbSettingsStorage;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _userStorage.ReadAsync();
            return users;
        }

        public async Task SetUserAsync(List<User> users)
        {
            await _userStorage.WriteAsync(users);
        }

        public async Task<List<Friendship>> GetFriendshipsAsync()
        {
            var friendships = await _friendshipStorage.ReadAsync();
            return friendships;
        }

        public async Task SetFriendshipAsync(List<Friendship> friendships)
        {
            await _friendshipStorage.WriteAsync(friendships);
        }

        public async Task<Settings> GetSettingsAsync()
        {
            var settings = await _settingsStorage.ReadAsync();
            return settings;
        }

        public async Task SetSettingsAsync(Settings settings)
        {
            await _settingsStorage.WriteAsync(settings);
        }
    }
}
