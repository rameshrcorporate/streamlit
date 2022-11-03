using Gateway.API.Common;

namespace Gateway.API.Service
{
    public class BaseService
    {
        protected ApiSettings? apiSettings;
        protected IServiceProvider serviceProvider;
        public virtual string? ApiBaseUrl { get; set; }
        public BaseService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            if (serviceProvider !=null)
            {
                apiSettings = serviceProvider.GetService<ApiSettings>();
            }
        }
    }
}
