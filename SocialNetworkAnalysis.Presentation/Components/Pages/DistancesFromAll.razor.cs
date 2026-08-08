using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class DistancesFromAll
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IGetDistancesFromAllUsersService _getDistancesFromAllUsersService { get; set; }

        private List<UserDistanceDto>  _userDistanceDtos = new();

        protected override void OnInitialized()
        {
            _userDistanceDtos = _getDistancesFromAllUsersService.Execute(Id);
        }
    }
}
