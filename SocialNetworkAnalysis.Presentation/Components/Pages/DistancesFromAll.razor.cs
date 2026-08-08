using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class DistancesFromAll
    {
        [Inject]
        private IGetDistancesFromAllUsersService _getDistancesFromAllUsersService { get; set; }

        private string _nodeId = "";
        private List<UserDistanceDto>  _userDistanceDtos = new();

        protected void Run()
        {
            try
            {
                _userDistanceDtos = _getDistancesFromAllUsersService.Execute(int.Parse(_nodeId));
            }
            catch { }
        }
    }
}
