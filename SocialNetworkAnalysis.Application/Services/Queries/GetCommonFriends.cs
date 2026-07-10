using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetCommonFriends : IGetCommonFriends
    {
        private readonly IGraphRuntime _runtime;
        private readonly ICommonNeighbors _commonNeighborsAlgorithm;

        public GetCommonFriends(IGraphRuntime runtime, ICommonNeighbors commonNeighborsAlgorithm)
        {
            _runtime = runtime;
            _commonNeighborsAlgorithm = commonNeighborsAlgorithm;
        }

        public List<User> Execute(int userAId, int userBId)
        {
            return _runtime.ExecuteRead(graph =>
            {
                List<User> commonFriendsList = new();

                var coreResult = _commonNeighborsAlgorithm.Execute(graph, userAId, userBId);

                if (coreResult?.SharedNeighbors != null)
                {
                    foreach (var id in coreResult.SharedNeighbors)
                    {
                        commonFriendsList.Add(new User
                        {
                            Id = id,
                            Name = graph.GetUserName(id) 
                        });
                    }
                }

                return commonFriendsList;
            });
        }
    }
}
