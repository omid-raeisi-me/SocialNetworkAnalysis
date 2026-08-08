using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class FriendSuggestion
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IGetFriendSuggestionsService _getFriendSuggestionsService { get; set; }

        private FriendSuggestionDto _friendSuggestionDto = new();
        private string _topNumber = "";

        private void Run()
        {
            try
            {
                if (_topNumber.Trim() == "")
                    _friendSuggestionDto = _getFriendSuggestionsService.Execute(Id);
                else
                    _friendSuggestionDto = _getFriendSuggestionsService.Execute(Id, int.Parse(_topNumber));
            }
            catch { }
        }
    }
}
