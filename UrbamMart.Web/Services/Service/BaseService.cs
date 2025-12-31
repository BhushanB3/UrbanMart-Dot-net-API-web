using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using UrbamMart.Web.Models;
using UrbamMart.Web.Services.IService;
using static UrbamMart.Web.Utilities.StaticDetails;

namespace UrbamMart.Web.Services.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T?> SendAsync<T>(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("UrbanMart");
                HttpRequestMessage Message = new();
                Message.Headers.Add("Accept", "application/json"); // tells server we want json response

                //token handling

                Message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                    Message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        Message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        Message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        Message.Method = HttpMethod.Delete;
                        break;
                    default:
                        Message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage? response = null;

                response = await client.SendAsync(Message);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.Unauthorized:
                    default:
                        var apiContent = await response.Content.ReadAsStringAsync();
                        var apiResult = JsonConvert.DeserializeObject<T>(apiContent);
                        return apiResult;
                }
            }
            catch(Exception ex){
                throw new Exception("Error Occurred", ex);
            }
        }
    }
}
