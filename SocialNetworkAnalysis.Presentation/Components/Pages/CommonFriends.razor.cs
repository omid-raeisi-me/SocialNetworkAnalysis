using Microsoft.AspNetCore.Components;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class CommonFriends
    {
        [Inject]
        private IGetCommonFriendsService _getCommonFriendsService { get; set; }

        private string _node1Id;
        private string _node2Id;
        private List<User> _commonFriends;
        private int _haveCommonFriends = -1;

        public void Run()
        {
            try
            {
                _commonFriends = _getCommonFriendsService.Execute(int.Parse(_node1Id), int.Parse(_node2Id));

                if( _commonFriends?.Count > 0)
                {
                    _haveCommonFriends = 1;
                }
                else
                {
                    _haveCommonFriends = 0;
                }
            }
            catch { }
        }
    }
}
