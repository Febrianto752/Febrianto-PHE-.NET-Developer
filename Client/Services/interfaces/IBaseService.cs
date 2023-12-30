using Models.ViewModels;

namespace Client.Services.interfaces
{
    public interface IBaseService
    {
        Task<ResponseVM?> SendAsync(RequestVM requestVM, bool withBearer = true);
    }
}
