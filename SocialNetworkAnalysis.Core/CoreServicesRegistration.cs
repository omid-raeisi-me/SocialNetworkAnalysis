using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAnalysis.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAdamicAdar, AdamicAdar>();
            services.AddScoped<IAverageDegree, AverageDegree>();
            services.AddScoped<IAveragePathLength, AveragePathLength>();
            services.AddScoped<IBetweennessCentrality, BetweennessCentrality>();
            services.AddScoped<IBFS, BFS>();
            services.AddScoped<IClosenessCentrality, ClosenessCentrality>();
            services.AddScoped<ICommonNeighbors, CommonNeighbors>();
            services.AddScoped<ICommunityDetection, CommunityDetection>();
            services.AddScoped<IConnectedComponents, ConnectedComponents>();
            services.AddScoped<IDegreeCentrality, DegreeCentrality>();
            services.AddScoped<IDensity, Density>();
            services.AddScoped<IDFS, DFS>();
            services.AddScoped<IDiameter, Diameter>();
            services.AddScoped<IDistancesFromAllUsers, DistancesFromAllUsers>();
            services.AddScoped<IJaccard, Jaccard>();
            services.AddScoped<ILinkPrediction, LinkPrediction>();
            services.AddScoped<INetworkInformation, NetworkInformation>();
            services.AddScoped<IPathExist, PathExist>();
            services.AddScoped<IShortestPath, ShortestPath>();
            services.AddScoped<IUserFriendsList, UserFriendsList>();

            return services;
        }
    }
}
