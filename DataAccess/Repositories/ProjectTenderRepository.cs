using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Models;

namespace DataAccess.Repositories
{
    public class ProjectTenderRepository : GeneralRepository<ProjectTender>, IProjectTenderRepository
    {
        public ProjectTenderRepository(ApplicationDbContext context) : base(context) { }

        public bool RemoveRange(IEnumerable<ProjectTender> projectTenders)
        {
            try
            {
                _context.Set<ProjectTender>().RemoveRange(projectTenders);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
