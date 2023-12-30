using Models.ViewModels;

namespace Client.Services.interfaces
{
    public interface IVendorService
    {
        Task<ResponseVM?> GetAllVendorsAsync();
        Task<ResponseVM?> GetVendorByGuid(Guid guid);
        Task<ResponseVM?> ApproveByAdmin(Guid guid);
        Task<ResponseVM?> ApproveByManager(Guid guid);
        Task<ResponseVM?> DeleteVendor(Guid guid);
    }
}
