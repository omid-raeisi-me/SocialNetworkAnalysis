using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAnalysis.Application.Abstractions;
using SocialNetworkAnalysis.Application.Services.Commands;
using SocialNetworkAnalysis.Application.Services.Queries;
using SocialNetworkAnalysis.Infrastructure.Runtime;

namespace SocialNetworkAnalysis.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, string usersJsonPath,
            string friendshipsJsonPath, string settingsJsonPath)
        {
            services.AddSingleton<IJsonStorage<List<User>>, JsonStorage<List<User>>>(p =>
                                                            new JsonStorage<List<User>>(usersJsonPath));
            services.AddSingleton<IJsonStorage<List<Friendship>>, JsonStorage<List<Friendship>>>(p =>
                                                            new JsonStorage<List<Friendship>>(friendshipsJsonPath));
            services.AddSingleton<IJsonStorage<Settings>, JsonStorage<Settings>>(p =>
                                                            new JsonStorage<Settings>(settingsJsonPath));

            services.AddSingleton<IGraphRepository, GraphRepository>();
            services.AddSingleton<ISettingsRepository, SettingsRepository>();
            services.AddSingleton<IGraphMapper, GraphMapper>();
            services.AddSingleton<GraphContext, GraphContext>();

            services.AddSingleton<IGraphRuntime, GraphRuntime>();

            return services;
        }
    }
}
