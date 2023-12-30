using Utilities.Enums;

namespace Models.ViewModels
{
    public class RequestVM
    {
        public ApiTypeEnum ApiType { get; set; } = ApiTypeEnum.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public ContentTypeEnum ContentType { get; set; } = ContentTypeEnum.Json;
    }
}
