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
        SocialGraph Graph { get; }
        Task InitializeAsync();
        int GenerateId();
        Task SaveAsync();
    }
}
