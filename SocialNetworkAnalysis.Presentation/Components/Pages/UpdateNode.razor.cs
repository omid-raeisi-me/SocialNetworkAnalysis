using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class UpdateNode
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private IUpdateUserNameService _updateUserNameService { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        private string _name;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _name = await _js.InvokeAsync<string>("graph.getNodeName", Id);
            }
        }

        public async Task Update()
        {
            _updateUserNameService.Execute(new User()
            {
                Id = Id,
                Name = _name
            });

            var graph = _getWholeGraphService.Execute();
            await _js.InvokeVoidAsync("graph.loadGraph", graph);
        }
    }
}
