using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class EdgeAction
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private IAddFriendshipService _addFriendshipService { get; set; }

        [Inject]
        private IRemoveFriendshipService _removeFriendshipService { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        private string _fromId = "";
        private string _toId = "";

        private async Task Add()
        {
            try
            {
                _addFriendshipService.Execute(new Application.Models.Friendship(int.Parse(_fromId), int.Parse(_toId)));
                await Refresh();           
            }
            catch { }
        }

        private async Task Remove()
        {
            try
            {
                _removeFriendshipService.Execute(new Application.Models.Friendship(int.Parse(_fromId), int.Parse(_toId)));
                await Refresh();
            }
            catch { }
        }

        private async Task Refresh()
        {
            var graph = _getWholeGraphService.Execute();
            await _js.InvokeVoidAsync("graph.loadGraph", graph);

            _fromId = "";
            _toId = "";
        }
    }
}
