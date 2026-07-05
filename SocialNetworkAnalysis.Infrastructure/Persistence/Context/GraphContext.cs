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
        private IJsonStorage<List<FriendShip>> _friendShipStorage;
        private IJsonStorage<Settings> _settingsStorage;

        public GraphContext(IJsonStorage<List<User>> userStorage,
            IJsonStorage<List<FriendShip>> friendShipStorage, IJsonStorage<Settings> dbSettingsStorage)
        {
            _userStorage = userStorage;
            _friendShipStorage = friendShipStorage;
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

        public async Task<List<FriendShip>> GetFriendShipsAsync()
        {
            var friendShips = await _friendShipStorage.ReadAsync();
            return friendShips;
        }

        public async Task SetFriendShipAsync(List<FriendShip> friendShips)
        {
            await _friendShipStorage.WriteAsync(friendShips);
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
