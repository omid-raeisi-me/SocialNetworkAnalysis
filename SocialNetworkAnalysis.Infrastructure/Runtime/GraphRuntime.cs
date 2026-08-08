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
        private bool _graphChanged;
        private bool _settingsChanged;

        private ReaderWriterLockSlim _lock;
        private IGraphRepository _graphRepository;
        private ISettingsRepository _settingsRepository;

        public SocialGraph Graph { get { return _graph; } }

        public GraphRuntime(IGraphRepository graphRepository, ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _graphRepository = graphRepository;
            _lock = new ReaderWriterLockSlim();
            _graphChanged = false;
            _settingsChanged = false;
        }

        public async Task InitializeAsync()
        {
            _graph = await _graphRepository.GetGraphAsync()?? new SocialGraph();
            _lastId = await _settingsRepository.GetLastIdAsync();
        }

        public async Task SaveAsync()
        {
            if (_graphChanged)
            {

                SocialGraph snapshot;

                _lock.EnterReadLock();

                try
                {
                    snapshot = _graph.DeepClone();
                }
                finally
                {
                    _lock.ExitReadLock();
                }

                _graphChanged = false;
                await _graphRepository.SetGraphAsync(snapshot);
            }

            if(_settingsChanged)
            {
                await _settingsRepository.SetLastIdAsync(_lastId);
            }
        }

        public int GenerateId()
        {
            _settingsChanged = true;
            return Interlocked.Increment(ref _lastId);
        }
    }
}
