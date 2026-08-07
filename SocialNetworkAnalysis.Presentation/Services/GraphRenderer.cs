using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions;

namespace SocialNetworkAnalysis.Presentation.Services
{
    public sealed class GraphRenderer
    {
        private IJSRuntime _js;
        private IGetWholeGraphService _getWholeGraphService;
        private IAddUserService _addUserService;
        private ISaveGraphService _saveGraphService;

        public GraphRenderer(IJSRuntime js, IGetWholeGraphService getWholeGraphService,
            IAddUserService addUserService,
            ISaveGraphService saveGraphService)
        {
            _js = js;
            _getWholeGraphService = getWholeGraphService;
            _addUserService = addUserService;
            _saveGraphService = saveGraphService;
        }

        public async Task InitializeAsync()
        {
            _addUserService.Execute(new Application.DTOs.Models.User()
            {
                Id = 1,
                Name = "Test"
            });

            await _saveGraphService.ExecuteAsync();

            var graph = _getWholeGraphService.Execute();
            await _js.InvokeVoidAsync("graph.initialize", graph);
        }

        public async Task AddNodeAsync(string id, string label)
        {
            await _js.InvokeVoidAsync("graph.addNode", id, label);
        }

        public async Task AddEdgeAsync(string source, string target)
        {
            await _js.InvokeVoidAsync("graph.addEdge", source, target);
        }

        public async Task ClearAsync()
        {
            await _js.InvokeVoidAsync("graph.clear");
        }

        public async Task FitAsync()
        {
            await _js.InvokeVoidAsync("graph.fit");
        }
    }
}
