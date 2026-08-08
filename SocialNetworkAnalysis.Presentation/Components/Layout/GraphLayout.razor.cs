using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.Models;
using SocialNetworkAnalysis.Presentation.Models;

namespace SocialNetworkAnalysis.Presentation.Components.Layout
{
    public partial class GraphLayout
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        [Inject]
        private IGetUserFriendsService _getUserFriendsService { get; set; }

        [Inject]
        private ISaveGraphService _saveGraphService { get; set; }

        [Inject]
        private IRemoveUserService _removeUserService { get; set; }

        [Inject]
        private IGetNetworkInformationService _getNetworkInformationService { get; set; }

        private DotNetObjectReference<GraphLayout>? _reference;
        private UserInfo? _selectedNode;
        private double _panelX;
        private double _panelY;
        private bool _showNodeMenu;
        private bool _showEdgeMenu;
        private int _selectedNodeId;
        private int _source;
        private int _target;
        private double _menuX;
        private double _menuY;

        private string _searchQuery = string.Empty;
        private List<User> _searchResults = new();
        private bool _showSearchResults;

        private int _edgeCount;
        private int _nodeCount;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var graph = _getWholeGraphService.Execute();
                await _js.InvokeVoidAsync("graph.initialize");
                await _js.InvokeVoidAsync("graph.loadGraph", graph);

                _reference = DotNetObjectReference.Create(this);
                await _js.InvokeVoidAsync("graph.registerNodeClick", _reference);
            }
        }

        protected override void OnInitialized()
        {
            var info = _getNetworkInformationService.Execute();
            _nodeCount = info.TotalUserCount;
            _edgeCount = info.TotalFriendshipCount;
        }

        [JSInvokable]
        public async Task OnNodeClicked(int id, double x, double y)
        {
            ClosePanel();

            _selectedNode = await LoadUser(id);
            _panelX = x;
            _panelY = y;
            await InvokeAsync(StateHasChanged);
        }

        private void ClosePanel()
        {
            _selectedNode = null;
            _showEdgeMenu = false;
            _showNodeMenu = false;
        }

        private async Task<UserInfo> LoadUser(int id)
        {
            var name = await _js.InvokeAsync<string>("graph.getNodeName", id);
            var friends = _getUserFriendsService.Execute(id);

            return new UserInfo()
            {
                Id = id,
                Name = name,
                Degree = friends.Count(),
                Friends = friends
            };
        }

        [JSInvokable]
        public Task OpenNodeMenu(int nodeId, double x, double y)
        {
            ClosePanel();

            _selectedNodeId = nodeId;
            _menuX = x;
            _menuY = y;
            _showNodeMenu = true;
            _showEdgeMenu = false;

            StateHasChanged();

            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task OpenEdgeMenu(int source, int target, double x, double y)
        {
            ClosePanel();

            _source = source;
            _target = target;
            _menuX = x;
            _menuY = y;
            _showEdgeMenu = true;
            _showNodeMenu = false;

            StateHasChanged();

            return Task.CompletedTask;
        }

        private async Task OnSearchInput(ChangeEventArgs e)
        {
            _searchQuery = e.Value?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                _searchResults.Clear();
                _showSearchResults = false;
                return;
            }

            _searchResults =
                await _js.InvokeAsync<List<User>>(
                    "graph.searchNodes",
                    _searchQuery);

            _showSearchResults = true;
        }

        private async Task SelectSearchResult(int id)
        {
            _showSearchResults = false;

            await _js.InvokeVoidAsync("graph.selectSearchNode", id);
        }

        private void ClearSearch()
        {
            _searchQuery = string.Empty;
            _searchResults.Clear();
            _showSearchResults = false;
        }

        private async Task DeleteUser()
        {
            _removeUserService.Execute(_selectedNodeId);
            var graph = _getWholeGraphService.Execute();
            await _js.InvokeVoidAsync("graph.loadGraph", graph);

            ClosePanel();
        }

        private async Task Save()
        {
            await _saveGraphService.ExecuteAsync();
        }
    }
}
