using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class NetworkInformation
    {
        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private IGetNetworkInformationService _getNetworkInformationService { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        private NetworkInformationDto _networkInformationDto = new NetworkInformationDto();

        protected override void OnInitialized()
        {
            _networkInformationDto = _getNetworkInformationService.Execute();
        }
    }
}
