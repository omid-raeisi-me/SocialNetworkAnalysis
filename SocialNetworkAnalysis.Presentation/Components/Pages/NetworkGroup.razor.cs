using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class NetworkGroup
    {
        [Inject]
        private IGetNetworkGroupsService _getNetworkGroupsService { get; set; }

        private NetworkGroupsDto _networkGroupsDto = new();

        protected override void OnInitialized()
        {
            _networkGroupsDto = _getNetworkGroupsService.Execute();
        }
    }
}
