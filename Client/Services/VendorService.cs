using Client.Services.interfaces;
using Models.ViewModels;
using Models.ViewModels.Vendor;
using Utilities;
using Utilities.Enums;

namespace Client.Services
{
    public class VendorService : IVendorService
    {
        private readonly IBaseService _baseService;
        public VendorService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseVM?> GetAllVendorsAsync()
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.GET,
                Url = Config.BaseApiUrl + "/api/vendors"
            });
        }

        public async Task<ResponseVM?> CreateVendorAsync(CreateVendorVM createVendorVM)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.POST,
                Data = createVendorVM,
                Url = Config.BaseApiUrl + "/api/vendors",
                ContentType = ContentTypeEnum.MultipartFormData
            });
        }
    }
}
