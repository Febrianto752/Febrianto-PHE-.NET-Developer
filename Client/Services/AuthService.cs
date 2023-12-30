using Client.Services.interfaces;
using Models.ViewModels;
using Models.ViewModels.Auth;
using Utilities;
using Utilities.Enums;

namespace Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseVM?> LoginAsync(LoginVM loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.POST,
                Data = loginRequestDto,
                Url = Config.BaseApiUrl + "/api/auth/login"
            }, withBearer: false);
        }
    }
}
