using SocialNetworkAnalysis.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions
{
    public interface IGetWholeGraphService
    {
        WholeGraphResponse Execute();
    }
}
