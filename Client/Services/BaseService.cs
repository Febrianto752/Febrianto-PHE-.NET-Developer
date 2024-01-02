using Client.Services.interfaces;
using Models.ViewModels;
using Newtonsoft.Json;
using System.Text;
using Utilities.Enums;

namespace Client.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        public async Task<ResponseVM?> SendAsync(RequestVM requestVM, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("SMSAPI");
                HttpRequestMessage message = new();
                if (requestVM.ContentType == ContentTypeEnum.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }
                //token
                if (withBearer)
                {
                    var token = _tokenService.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestVM.Url);

                if (requestVM.ContentType == ContentTypeEnum.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();

                    foreach (var prop in requestVM.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(requestVM.Data);
                        if (value is FormFile)
                        {
                            var file = (FormFile)value;
                            if (file != null)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else
                        {
                            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else
                {
                    if (requestVM.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestVM.Data), Encoding.UTF8, "application/json");
                    }
                }


                HttpResponseMessage? apiResponse = null;

                switch (requestVM.ApiType)
                {
                    case ApiTypeEnum.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypeEnum.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiTypeEnum.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);


                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseVM = JsonConvert.DeserializeObject<ResponseVM>(apiContent);
                return apiResponseVM;

            }
            catch (Exception ex)
            {
                var responseVM = new ResponseVM
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return responseVM;
            }
        }
    }
}
