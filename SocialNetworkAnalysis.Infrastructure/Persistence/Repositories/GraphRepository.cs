using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Infrastructure.Persistence.Repositories
{
    public class GraphRepository : IGraphRepository
    {
        private GraphContext _graphContext;
        private IGraphMapper _mapper;

        public GraphRepository(GraphContext graphContext, IGraphMapper mapper)
        {
            _graphContext = graphContext;
            _mapper = mapper;
        }

        public async Task<SocialGraph> GetGraphAsync()
        {
            var users = await _graphContext.GetUsersAsync();
            var friendships = await  _graphContext.GetFriendshipsAsync();

            var graph = _mapper.ConvertToDomianModel(users, friendships);

            return graph;
        }

        public async Task SetGraphAsync(SocialGraph graph)
        {
            var dataModel = _mapper.ConvertToDataModel(graph);

            await _graphContext.SetUserAsync(dataModel.Users);
            await _graphContext.SetFriendshipAsync(dataModel.Friendships);
        }
    }
}
