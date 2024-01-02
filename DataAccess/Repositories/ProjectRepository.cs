using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Models;

namespace DataAccess.Repositories
{
    public class ProjectRepository : GeneralRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}
