using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Contracts.Runtime;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Infrastructure.Runtime
{
    public class GraphRuntime : IGraphRuntime
    {
        private SocialGraph _graph;
        private int _lastId;

        private ReaderWriterLockSlim _lock;
        private IGraphRepository _graphRepository;
        private ISettingsRepository _settingsRepository;

        public SocialGraph Graph { get { return _graph; } }

        public GraphRuntime(IGraphRepository graphRepository, ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _graphRepository = graphRepository;
            _lock = new ReaderWriterLockSlim();
        }

        public async Task InitializeAsync()
        {
            _graph = await _graphRepository.GetGraphAsync() ?? new SocialGraph();
            _lastId = await _settingsRepository.GetLastIdAsync();
        }

        public async Task SaveAsync()
        {
            await _graphRepository.SetGraphAsync(_graph);
            await _settingsRepository.SetLastIdAsync(_lastId);
        }

        public int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}
