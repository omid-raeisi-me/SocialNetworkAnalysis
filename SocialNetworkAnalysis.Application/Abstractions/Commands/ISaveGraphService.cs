using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions.Commands
{
    public interface ISaveGraphService
    {
        Task ExecuteAsync();
    }
}
