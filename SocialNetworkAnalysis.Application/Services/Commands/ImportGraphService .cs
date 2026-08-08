using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Commands
{
    public class ImportGraphService : IImportGraphService
    {
        private IGraphRuntime _graphRuntime;

        public ImportGraphService(IGraphRuntime graphRuntime)
        {
            _graphRuntime = graphRuntime;
        }

        public async Task ExecuteAsync(Stream stream)
        {
            var userIds = new Dictionary<string, int>();

            using var reader = new StreamReader(stream);

            string? line;

            while ((line = await reader.ReadLineAsync()) is not null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2)
                    continue;

                var fromName = parts[0];
                var toName = parts[1];

                var fromId = GetOrCreateUser(
                    userIds,fromName
                    );

                var toId = GetOrCreateUser(
                    userIds,toName);

                _graphRuntime.Graph.AddFriendship(fromId, toId);
            }
        }

        private int GetOrCreateUser(Dictionary<string, int> userIds, string name)
        {
            var graph = _graphRuntime.Graph;

            if (userIds.TryGetValue(name, out var id))
                return id;

            id = _graphRuntime.GenerateId();

            userIds[name] = id;

            graph.AddUser(id, name);

            return id;
        }
    }
}
