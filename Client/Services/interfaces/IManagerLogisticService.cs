using Models.ViewModels;

namespace Client.Services.interfaces
{
    public interface IManagerLogisticService
    {
        Task<ResponseVM?> GetAllManagerLogisticsAsync();
    }
}
