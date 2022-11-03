using Gateway.API.Common;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
namespace APIGateWay.API.Common
{
    public interface IHTTPClients
    {

    }
    public class HTTPClients
    {
        #region Constructor 
        public HTTPClients()
        {
        }
        #endregion
        #region Get Async 
        /// <summary> 
        /// Get Async 
        /// </summary> 
        /// <param name="url"></param> 
        /// <returns></returns> 
        public async Task<SuccessData> GetAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(new Uri(url)).Result;
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted)
                            throw x.Exception;
                        return JsonConvert.DeserializeObject<SuccessData>(x.Result);
                    });
                }
            }
            catch (Exception)
            {
            }
            return new SuccessData();
        }
        #endregion
        #region Post As Json Async 
        /// <summary> 
        /// Post As Json Async 
        /// </summary> 
        /// <typeparam name="TModel"></typeparam> 
        /// <param name="model"></param> 
        /// <param name="url"></param>
        /// <returns></returns> 
        public async Task<SuccessData> PostAsJsonAsync<TModel>(TModel model, string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, model, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted)
                            throw x.Exception;
                        return JsonConvert.DeserializeObject<SuccessData>(x.Result);

                    });
                }
            }
            catch (Exception)
            {
            }
            return new SuccessData();
        }
        #endregion
        #region Delete Async 
        /// <summary>
        //// Delete Async 
        /// </summary> 
        /// <param name="url"></param> 
        /// <returns></returns> 
        public async Task<SuccessData> DeleteAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync(url).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted)
                            throw x.Exception;
                        return JsonConvert.DeserializeObject<SuccessData>(x.Result);
                    });
                }
            }
            catch (Exception) { }
            return new SuccessData();
        }
        #endregion
    }
}