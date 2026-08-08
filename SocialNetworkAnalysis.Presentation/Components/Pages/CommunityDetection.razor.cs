using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class CommunityDetection
    {
        [Inject]
        private IGetCommunityDetectionService _getCommunityDetectionService { get; set; }

        private  CommunityDetectionDto _communityDetectionDto = new();

        protected override void OnInitialized()
        {
            _communityDetectionDto = _getCommunityDetectionService.Execute();
        }
    }
}
