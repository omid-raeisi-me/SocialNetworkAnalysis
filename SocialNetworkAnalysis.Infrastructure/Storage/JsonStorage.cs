using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Infrastructure.Storage
{
    using System.Text.Json;

    public class JsonStorage<T> : IJsonStorage<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public JsonStorage(string filePath)
        {
            _filePath = filePath;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<T> ReadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return CreateDefaultInstance();
            }

            try
            {
                await using var stream = new FileStream(
                    _filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4096,
                    useAsync: true);

                if (stream.Length == 0)
                {
                    return CreateDefaultInstance();
                }

                var result = await JsonSerializer.DeserializeAsync<T>(stream, _options);

                return result ?? CreateDefaultInstance();
            }
            catch (JsonException)
            {
                return CreateDefaultInstance();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task WriteAsync(T data)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await using var stream = new FileStream(
                _filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await JsonSerializer.SerializeAsync(stream, data, _options);
        }

        private T CreateDefaultInstance()
        {
            return typeof(T).IsValueType ? default! : Activator.CreateInstance<T>();
        }
    }

}
