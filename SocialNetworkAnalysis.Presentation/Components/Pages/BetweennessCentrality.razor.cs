using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class BetweennessCentrality
    {
        [Inject]
        private IGetBetweennessCentralityService _getBetweennessCentralityService { get; set; }

        private BetweennessCentralityDto _betweennessCentralityDto = new();

        protected override void OnInitialized()
        {
            _betweennessCentralityDto = _getBetweennessCentralityService.Execute();
        }
    }
}
