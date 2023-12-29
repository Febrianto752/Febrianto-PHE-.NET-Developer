using Models;

namespace DataAccess.Repositories.interfaces
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        public Account? GetByEmail(string email);
    }

}
