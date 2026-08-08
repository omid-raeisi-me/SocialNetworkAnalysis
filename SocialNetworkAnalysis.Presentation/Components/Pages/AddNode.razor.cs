using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class AddNode
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private IAddUserService _addUserService { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        private string _name;

        public async Task Add()
        {
            _addUserService.Execute(_name);

            var graph = _getWholeGraphService.Execute();
            await _js.InvokeVoidAsync("graph.loadGraph", graph);

            _name = "";
        }
    }
}
