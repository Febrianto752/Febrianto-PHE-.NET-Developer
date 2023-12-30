using Models.ViewModels;
using Models.ViewModels.Vendor;

namespace Client.Services.interfaces
{
    public interface IVendorService
    {
        Task<ResponseVM?> GetAllVendorsAsync();
        Task<ResponseVM?> CreateVendorAsync(CreateVendorVM createVendorVM);
    }
}
