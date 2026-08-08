using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;

namespace SocialNetworkAnalysis.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAddFriendshipService, AddFriendshipService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IGetBetweennessCentralityService, GetBetweennessCentralityService>();
            services.AddScoped<IGetClosenessCentralityService, GetClosenessCentralityService>();
            services.AddScoped<IGetCommonFriendsService, GetCommonFriendsService>();
            services.AddScoped<IGetCommunityDetectionService, GetCommunityDetectionService>();
            services.AddScoped<IGetDistancesFromAllUsersService, GetDistancesFromAllUsersService>();
            services.AddScoped<IGetFriendSuggestionsService, GetFriendSuggestionsService>();
            services.AddScoped<IGetNetworkGroupsService, GetNetworkGroupsService>();
            services.AddScoped<IGetShortestPathService, GetShortestPathService>();
            services.AddScoped<IGetNetworkInformationService, GetNetworkInformationService>();
            services.AddScoped<IGetUserFriendsService, GetUserFriendsService>();
            services.AddScoped<IGetWholeGraphService, GetWholeGraphService>();
            services.AddScoped<IRemoveFriendshipService, RemoveFriendshipService>();
            services.AddScoped<IRemoveUserService, RemoveUserService>();
            services.AddScoped<IUpdateUserNameService, UpdateUserNameService>();
            services.AddScoped<ISaveGraphService, SaveGraphService>();

            return services;
        }
    }
}
