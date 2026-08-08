using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class ClosenessCentrality
    {
        [Inject]
        private IGetClosenessCentralityService _getClosenessCentralityService { get; set; }

        private ClosenessCentralityDto _closenessCentralityDto = new();

        protected override void OnInitialized()
        {
            _closenessCentralityDto = _getClosenessCentralityService.Execute();
        }
    }
}
