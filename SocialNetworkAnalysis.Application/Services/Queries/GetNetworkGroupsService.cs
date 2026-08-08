using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Application.Services.Queries
{
    public class GetNetworkGroupsService : IGetNetworkGroupsService
    {
        private readonly IGraphRuntime _runtime;
        private readonly IConnectedComponents _connectedComponents;

        public GetNetworkGroupsService(IGraphRuntime runtime, IConnectedComponents connectedComponents)
        {
            _runtime = runtime;
            _connectedComponents = connectedComponents;
        }

        public NetworkGroupsDto Execute()
        {
            return _runtime.ExecuteRead(graph =>
            {
                NetworkGroupsDto appResult = new();

                var coreResult = _connectedComponents.Execute(graph);

                appResult.TotalGroups = coreResult.ComponentsCount;

                int groupCounter = 1;

                foreach (var componentIds in coreResult.Components)
                {
                    var group = new NetworkGroup
                    {
                        GroupId = groupCounter++
                    };

                    foreach (var id in componentIds)
                    {
                        group.Members.Add(new User
                        {
                            Id = id,
                            Name = graph.GetUserName(id)
                        });
                    }

                    appResult.Groups.Add(group);
                }

                return appResult;
            });
        }
    }
}
