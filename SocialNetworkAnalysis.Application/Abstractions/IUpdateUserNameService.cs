using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions
{
    public interface IUpdateUserNameService
    {
        void Execute(User user);
    }
}
