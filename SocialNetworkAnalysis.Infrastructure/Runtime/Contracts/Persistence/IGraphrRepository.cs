using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Infrastructure.Runtime.Contracts.Persistence
{
    public interface IGraphRepository
    {
        Task<SocialGraph> GetGraphAsync();
        Task SetGraphAsync(SocialGraph graph);
    }
}
