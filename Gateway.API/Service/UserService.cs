using APIGateWay.API.Common;
using Gateway.API.Common;
using Newtonsoft.Json;
using GRO.SharedLibrary.ViewModel.Common;
using GRO.SharedLibrary.ViewModel.IAM;

namespace Gateway.API.Service
{
    public interface IUserService
    {
        UserViewModel? GetUserByEmail(string emailId);
        UserViewModel? Authenticate(LoginInfo loginInfo);
    }
    public class UserService : IUserService
    {
        private readonly HTTPClients httpClient;
        protected ApiSettings? apiSettings;
        public virtual string HostUrl { get; set; }
        public virtual string ApiBaseUrl { get; set; }

        public UserService(HTTPClients httpClient, IConfiguration configurationManager) 
        {
            HostUrl = configurationManager.GetSection("ApiSettings:ApplicationAPI:HostUrl").Value;
            ApiBaseUrl = apiSettings?.IAMAPI + HostUrl  + IAMApiEndPoints.User;
            this.httpClient = httpClient;
        }
        public UserViewModel? Authenticate(LoginInfo loginInfo)
        {
            var data = httpClient.PostAsJsonAsync(loginInfo, ApiBaseUrl + "Authentication/Login").Result;
            return JsonConvert.DeserializeObject<UserViewModel>(JsonConvert.SerializeObject(data));
        }
        public UserViewModel? GetUserByEmail(string emailId)
        {
            var data = httpClient.GetAsync(ApiBaseUrl);
            return JsonConvert.DeserializeObject<UserViewModel>(JsonConvert.SerializeObject(data));
        }
    }
}
