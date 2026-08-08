using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Application.Abstractions.Commands
{
    public interface IAddUserService
    {
        void Execute(string name);
    }
}
