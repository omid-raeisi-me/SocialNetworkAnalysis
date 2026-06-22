using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Infrastructure.FileManager.Contracts;
using SocialNetworkAnalysis.Infrastructure.Persistence.Models;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Context
{
    public class GraphContext
    {
        private IJsonStorage<List<User>> _userStorage;
        private IJsonStorage<List<FriendShip>> _friendShipStorage;
        private IJsonStorage<DbSettings> _dbSettingsStorage;

        private List<User> _users;
        private List<FriendShip> _friendShips;
        private DbSettings _dbSettings;

        public GraphContext(IJsonStorage<List<User>> userStorage,
            IJsonStorage<List<FriendShip>> friendShipStorage, IJsonStorage<DbSettings> dbSettingsStorage)
        {
            _userStorage = userStorage;
            _friendShipStorage = friendShipStorage;
            _dbSettingsStorage = dbSettingsStorage;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            if (_users == null)
                _users = await _userStorage.ReadAsync();

            return _users;
        }

        public async Task AddUserAsync(User user)
        {
            if (_users == null)
                _users = await _userStorage.ReadAsync();

            if (_dbSettings == null)
                _dbSettings = await _dbSettingsStorage.ReadAsync();

            _users.Add(new User()
            {
                Id = _dbSettings.LastId,
                Name = user.Name
            });

            _dbSettings.LastId++;
        }

        public async Task RemoveUserAsync(User user)
        {
            if (_users == null)
                _users = await _userStorage.ReadAsync();

            if (_friendShips == null)
                _friendShips = await _friendShipStorage.ReadAsync();

            var userGraph = _users.FirstOrDefault(u => u.Id == user.Id);

            if (userGraph != null)
            {
                var friendShipsSelected = _friendShips.Where(f =>
                    f.FromId == userGraph.Id || f.ToId == userGraph.Id);

                foreach (var friendShip in friendShipsSelected)
                    _friendShips.Remove(friendShip);

                _users.Remove(user);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (_users == null)
                _users = await _userStorage.ReadAsync();

            var userGraph = _users.FirstOrDefault(u => u.Id == user.Id);

            if (user != null)
            {
                user.Name = user.Name;
            }
        }

        public async Task<List<FriendShip>> GetFriendShipsAsync()
        {
            if (_friendShips == null)
                _friendShips = await _friendShipStorage.ReadAsync();

            return _friendShips;
        }

        public async Task AddFriendShipAsync(FriendShip friendShip)
        {
            if (_friendShips == null)
                _friendShips = await _friendShipStorage.ReadAsync();

            var isExist = _friendShips.Any(f =>
                                    f.FromId == friendShip.FromId && f.ToId == friendShip.ToId);
            if (!isExist)
                _friendShips.Add(friendShip);
        }

        public void RemoveFriendShipAsync(FriendShip friendShip)
        {
            var friendShipGraph = _friendShips.FirstOrDefault(f =>
                                f.FromId == friendShip.FromId && f.ToId == friendShip.ToId);

            if (friendShipGraph != null)
                _friendShips.Remove(friendShipGraph);
        }

        public async Task SaveChangesAsync()
        {
            if (_users != null)
                await _userStorage.WriteAsync(_users);

            if (_friendShips != null)
                await _friendShipStorage.WriteAsync(_friendShips);

            if (_dbSettings != null)
                await _dbSettingsStorage.WriteAsync(_dbSettings);
        }
    }
}
