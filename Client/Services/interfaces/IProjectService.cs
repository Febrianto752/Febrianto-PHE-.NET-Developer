using Models.ViewModels;
using Models.ViewModels.Project;

namespace Client.Services.interfaces
{
    public interface IProjectService
    {
        Task<ResponseVM?> GetAllProjectsAsync();
        Task<ResponseVM?> GetProjectByGuidAsync(Guid guid);
        Task<ResponseVM?> CreateProjectAsync(CreateProjectVM createProjectVM);
        Task<ResponseVM?> EditProjectAsync(Guid guid, EditProjectVM editProjectVM);
        Task<ResponseVM?> DeleteProjectAsync(Guid guid);
    }
}
