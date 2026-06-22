using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Infrastructure.FileManager.Contracts;

namespace SocialNetworkAnalysis.Infrastructure.FileManager.Services
{
    public class JsonStorage<T> : IJsonStorage<T>
    {
        private string _filePath;

        public JsonStorage(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<T> ReadAsync()
        {
            if (!File.Exists(_filePath))
                return default;

            await using var stream = new FileStream(
                _filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public async Task WriteAsync(T data)
        {
            await using var stream = new FileStream(
                _filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await JsonSerializer.SerializeAsync<T>(
                stream,
                data,
                new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
