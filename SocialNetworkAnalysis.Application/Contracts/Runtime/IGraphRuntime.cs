using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Application.Contracts.Runtime
{
    public interface IGraphRuntime
    {
        Task InitializeAsync();
        void ExecuteWrite(Action<SocialGraph> action);
        T ExecuteRead<T>(Func<SocialGraph, T> action);
        Task<T> ExecuteSnapshotAsync<T>(Func<SocialGraph, T> query);
        int GenerateId();
        Task SaveAsync();
    }
}
