using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions.Commands
{
    public interface IImportGraphService
    {
        Task ExecuteAsync(Stream stream);
    }
}
