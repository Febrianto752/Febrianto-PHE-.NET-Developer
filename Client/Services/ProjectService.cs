using Client.Services.interfaces;
using Models.ViewModels;
using Models.ViewModels.Project;
using Utilities;
using Utilities.Enums;

namespace Client.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IBaseService _baseService;
        public ProjectService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseVM?> CreateProjectAsync(CreateProjectVM createProjectVM)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.POST,
                Data = createProjectVM,
                Url = Config.BaseApiUrl + "/api/projects",
            });
        }

        public async Task<ResponseVM?> DeleteProjectAsync(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = Config.BaseApiUrl + "/api/projects/" + guid
            });
        }

        public async Task<ResponseVM?> EditProjectAsync(Guid guid, EditProjectVM editProjectVM)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.PUT,
                Data = editProjectVM,
                Url = Config.BaseApiUrl + "/api/projects/" + guid
            });
        }

        public async Task<ResponseVM?> GetAllProjectsAsync()
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.GET,
                Url = Config.BaseApiUrl + "/api/projects"
            });
        }

        public async Task<ResponseVM?> GetProjectByGuidAsync(Guid guid)
        {
            return await _baseService.SendAsync(new RequestVM()
            {
                ApiType = ApiTypeEnum.GET,
                Url = Config.BaseApiUrl + "/api/projects/" + guid
            });
        }
    }
}
