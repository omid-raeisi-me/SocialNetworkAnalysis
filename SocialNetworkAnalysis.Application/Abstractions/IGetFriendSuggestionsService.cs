using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Abstractions
{
    public interface IGetFriendSuggestionsService
    {
        FriendSuggestionResponse Execute(int userId, int topK = 5);
    }
}
