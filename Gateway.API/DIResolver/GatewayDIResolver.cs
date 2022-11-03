using APIGateWay.API.Common;
using Gateway.API.Common;
using Gateway.API.Middleware;
using Gateway.API.Service;

namespace Gateway.API.DIResolver
{
    public static class GatewayDIResolver
    {
        public static void ConfigureDIResolver(this IServiceCollection services)
        {
            #region Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<HTTPClients>();
            services.AddScoped<ApiSettings>();
            services.AddScoped<HostSettings>();
            #endregion

        }
    }
}
