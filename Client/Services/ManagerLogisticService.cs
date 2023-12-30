using Client.Services.interfaces;
using Models.ViewModels;
using Utilities;
using Utilities.Enums;

namespace Client.Services
{
    public class ManagerLogisticService : IManagerLogisticService
    {
        private readonly IBaseService _baseService;
        public ManagerLogisticService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseVM?> GetAllManagerLogisticsAsync()
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.GET,
                Url = Config.BaseApiUrl + "/api/manager-logistics"
            });
        }
    }
}
