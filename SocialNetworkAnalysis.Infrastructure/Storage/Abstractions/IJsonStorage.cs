using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Storage.Abstractions
{
    public interface IJsonStorage<T>
    {
        Task<T> ReadAsync();
        Task WriteAsync(T data);
    }
}
