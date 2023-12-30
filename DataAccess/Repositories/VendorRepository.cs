using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Models;

namespace DataAccess.Repositories
{
    public class VendorRepository : GeneralRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
