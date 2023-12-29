using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Models;

namespace DataAccess.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Account? GetByEmail(string email)
        {
            return _context.Set<Account>().FirstOrDefault(e => e.Email == email);
        }
    }
}
