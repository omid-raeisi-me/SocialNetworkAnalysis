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
        private int _nextId;
        private bool _graphChanged;
        private bool _nextIdChanged;

        private ReaderWriterLockSlim _lock;
        private IGraphRepository _graphRepository;
        private ISettingsRepository _settingsRepository;

        public GraphRuntime(IGraphRepository graphRepository, ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _graphRepository = graphRepository;
            _lock = new ReaderWriterLockSlim();
            _graphChanged = false;
            _nextIdChanged = false;

            Initialize();
        }

        private void Initialize()
        {
            _graph = _graphRepository.GetGraphAsync().Result ?? new SocialGraph();
            _nextId = _settingsRepository.GetLastIdAsync().Result;
        }

        public void ExecuteWrite(Action<SocialGraph> action)
        {
            _lock.EnterWriteLock();

            try
            {
                _graphChanged = true;
                action(_graph);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T ExecuteRead<T>(Func<SocialGraph, T> action)
        {
            _lock.EnterReadLock();

            try
            {
                return action(_graph);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public async Task<T> ExecuteSnapshotAsync<T>(Func<SocialGraph, T> query)
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

            return await Task.Run(() => query(snapshot));
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

            if(_nextIdChanged)
            {
                int lastId = _nextId - 1;
                await _settingsRepository.SetLastIdAsync(lastId);
            }
        }

        public int GenerateId()
        {
            _nextIdChanged = true;
            return Interlocked.Increment(ref _nextId);
        }
    }
}
