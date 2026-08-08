using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions.Commands
{
    public interface IRemoveUserService
    {
        void Execute(int userId);
    }
}
