using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Runtime.Contracts.Persistence
{
    public interface ISettingsRepository
    {
        Task<int> GetLastIdAsync();
        Task SetLastIdAsync(int lastId);
    }
}
