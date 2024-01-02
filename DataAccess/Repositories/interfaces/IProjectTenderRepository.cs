using Models;

namespace DataAccess.Repositories.interfaces
{
    public interface IProjectTenderRepository : IGeneralRepository<ProjectTender>
    {
        bool RemoveRange(IEnumerable<ProjectTender> projectsTenders);
    }
}
