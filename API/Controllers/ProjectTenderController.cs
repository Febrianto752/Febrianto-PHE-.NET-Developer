using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Models.ViewModels.ProjectTender;
using Utilities.Enums;

namespace API.Controllers
{
    [Route("api/project-tenders")]
    [ApiController]
    public class ProjectTenderController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTenderRepository _projectTenderRepository;
        private readonly ApplicationDbContext _db;
        public ProjectTenderController(IProjectRepository projectRepository, ApplicationDbContext db, IProjectTenderRepository projectTenderRepository)
        {
            _projectRepository = projectRepository;
            _db = db;
            _projectTenderRepository = projectTenderRepository;
        }

        [HttpGet]
        public IActionResult GetProjectTenders()
        {
            var projectTenders = _projectTenderRepository.GetAll();

            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = projectTenders
            });
        }

        [HttpPost]
        public IActionResult Create(CreateProjectTenderVM createProjectTenderVM)
        {
            var projectIsExist = _projectRepository.GetByGuid(p => p.Guid == createProjectTenderVM.ProjectGuid, tracked: false);

            if (projectIsExist is null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data project tidak ditemukan"
                });
            }
            var newProjectTender = new ProjectTender()
            {
                Guid = Guid.NewGuid(),
                ProjectGuid = createProjectTenderVM.ProjectGuid,
                VendorGuid = createProjectTenderVM.VendorGuid,
                Status = TenderStatusEnum.Submitted,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var project = _projectTenderRepository.Create(newProjectTender);

            if (project == null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data gagal ditambahkan"
                });
            }

            return Created("", new ResponseVM
            {
                IsSuccess = true,
                Message = "Data created",
                Data = newProjectTender
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var projectTender = _projectTenderRepository.GetByGuid(pt => pt.Guid == guid, tracked: false);

            if (projectTender == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data tidak ditemukan"
                });
            }

            _projectTenderRepository.Delete(projectTender);

            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Berhasil menghapus data"
            });

        }



    }
}
