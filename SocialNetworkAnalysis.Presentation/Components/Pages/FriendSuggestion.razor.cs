using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class FriendSuggestion
    {
        [Inject]
        private IGetFriendSuggestionsService _getFriendSuggestionsService { get; set; }

        private FriendSuggestionDto _friendSuggestionDto = new();
        private string _topNumber = "";
        private string _nodeId = "";

        private void Run()
        {
            try
            {
                if (_topNumber.Trim() == "")
                    _friendSuggestionDto = _getFriendSuggestionsService.Execute(int.Parse(_nodeId));
                else
                    _friendSuggestionDto = _getFriendSuggestionsService.Execute(int.Parse(_nodeId), int.Parse(_topNumber));
            }
            catch { }
        }
    }
}
