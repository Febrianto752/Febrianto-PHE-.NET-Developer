using Client.Services.interfaces;
using Models.ViewModels;
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

        public async Task<ResponseVM?> GetVendorByGuid(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.GET,
                Url = Config.BaseApiUrl + "/api/vendors/" + guid
            });
        }

        public async Task<ResponseVM?> ApproveByAdmin(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.PUT,
                Url = Config.BaseApiUrl + "/api/vendors/" + guid + "/approved-by-admin"
            });
        }

        public async Task<ResponseVM?> ApproveByManager(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.PUT,
                Url = Config.BaseApiUrl + "/api/vendors/" + guid + "/approved-by-manager"
            });
        }

        public async Task<ResponseVM?> DeleteVendor(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = Config.BaseApiUrl + "/api/vendors/" + guid
            });
        }

    }
}
