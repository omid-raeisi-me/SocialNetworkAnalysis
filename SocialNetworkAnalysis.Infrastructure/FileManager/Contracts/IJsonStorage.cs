using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.FileManager.Contracts
{
    public interface IJsonStorage<T>
    {
        Task<T> ReadAsync(CancellationToken cancellationToken);
        Task WriteAsync(T data, CancellationToken cancellationToken);
    }
}
