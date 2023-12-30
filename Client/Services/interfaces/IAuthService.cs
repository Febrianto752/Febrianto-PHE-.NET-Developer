using Models.ViewModels;
using Models.ViewModels.Auth;

namespace Client.Services.interfaces
{
    public interface IAuthService
    {
        Task<ResponseVM?> LoginAsync(LoginVM loginRequestDto);
    }
}
