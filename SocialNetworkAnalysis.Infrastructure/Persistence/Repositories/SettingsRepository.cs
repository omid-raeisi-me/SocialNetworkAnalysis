using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Repositories
{
    internal class SettingsRepository : ISettingsRepository
    {
        private GraphContext _graphContext;

        public SettingsRepository(GraphContext graphContext)
        {
            _graphContext = graphContext;
        }

        public async Task<int> GetLastIdAsync()
        {
            var settings = await _graphContext.GetSettingsAsync();
            return settings.LastId;
        }

        public async Task SetLastIdAsync(int lastId)
        {
            var settings = await _graphContext.GetSettingsAsync();
            settings.LastId = lastId;
            await _graphContext.SetSettingsAsync(settings);
        }
    }
}
