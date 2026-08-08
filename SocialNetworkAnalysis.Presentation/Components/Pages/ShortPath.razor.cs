using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.DTOs;
using SocialNetworkAnalysis.Application.Models;
using SocialNetworkAnalysis.Core.Abstractions;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class ShortPath
    {
        [Inject]
        private IGetShortestPathService _shortPathService { get; set; }

        private string _startNodeId;
        private string _endNodeId;
        private List<User> _shortPath;
        private int _shortPathFound = -1;

        public void Run()
        {
            try
            {
                _shortPath = _shortPathService.Execute(int.Parse(_startNodeId), int.Parse(_endNodeId));

                if(_shortPath?.Any() == true)
                {
                    _shortPathFound = 1;
                }
                else
                {
                    _shortPathFound = 0;
                }
            }
            catch{ }
        }
    }
}
