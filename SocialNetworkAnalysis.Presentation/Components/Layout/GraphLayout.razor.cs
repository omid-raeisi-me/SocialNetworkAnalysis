using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions;
using SocialNetworkAnalysis.Presentation.Models;

namespace SocialNetworkAnalysis.Presentation.Components.Layout
{
    public partial class GraphLayout
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private IGetWholeGraphService _wholeGraphService { get; set; }

        private DotNetObjectReference<GraphLayout>? _reference;
        private UserInfo? _selectedNode;
        private double _panelX;
        private double _panelY;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var graph = _wholeGraphService.Execute();
                await _js.InvokeVoidAsync("graph.initialize");
                await _js.InvokeVoidAsync("graph.loadGraph", graph);

                _reference = DotNetObjectReference.Create(this);
                await _js.InvokeVoidAsync("graph.registerNodeClick",_reference);
            }
        }

        [JSInvokable]
        public async Task OnNodeClicked(int id, double x, double y)
        {
            _selectedNode = await LoadUser(id);
            _panelX = x + 20;
            _panelY = y - 20;
            await InvokeAsync(StateHasChanged);
        }

        private void ClosePanel()
        {
            _selectedNode = null;
        }

        private Task<UserInfo> LoadUser(int id)
        {
            return Task.FromResult(new UserInfo()
            {
                Id = id,
                Name = $"User {id}",
                Degree = 12
            });
        }
    }
}
