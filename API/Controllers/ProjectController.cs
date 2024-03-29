﻿using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Models.ViewModels.Project;
using Models.ViewModels.ProjectTender;
using System.Net;
using Utilities.Handlers;

namespace API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTenderRepository _projectTenderRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ApplicationDbContext _db;
        public ProjectController(IProjectRepository projectRepository, ApplicationDbContext db, IProjectTenderRepository projectTenderRepository, IAccountRepository accountRepository, IVendorRepository vendorRepository)
        {
            _projectRepository = projectRepository;
            _projectTenderRepository = projectTenderRepository;
            _accountRepository = accountRepository;
            _vendorRepository = vendorRepository;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = _projectRepository.GetAll().ToList();
            if (projects == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            var projectTenders = _projectTenderRepository.GetAll();

            projects = projects.Select(p =>
            {
                var tendersByProject = projectTenders.Where(pt => pt.ProjectGuid == p.Guid).ToList();

                p.ProjectTenders = tendersByProject;
                return p;

            }).ToList();


            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = projects
            });
        }

        [HttpGet("{guid}")]
        public IActionResult Details(Guid guid)
        {
            var project = _projectRepository.GetByGuid(v => v.Guid == guid);
            var projectDetails = new ProjectDetailsVM();

            if (project == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            projectDetails.ProjectGuid = project.Guid;
            projectDetails.ProjectName = project.Name;
            projectDetails.Description = project.Description;
            projectDetails.Description = project.Description;
            projectDetails.StartDate = project.StartDate;
            projectDetails.EndDate = project.EndDate;


            var tendersByProject = _projectTenderRepository.GetAll().Where(pt => pt.ProjectGuid == project.Guid).ToList();

            if (tendersByProject != null)
            {
                var vendors = _vendorRepository.GetAll();
                var accounts = _accountRepository.GetAll();

                projectDetails.VendorParticipants = tendersByProject.Select(tp =>
                {

                    var vendor = vendors.FirstOrDefault(v => v.Guid == tp.VendorGuid);
                    var vendorAccount = accounts.FirstOrDefault(a => a.Guid == vendor.AccountGuid);

                    var vendorParticiapnt = new VendorParticipantVM()
                    {
                        ProjectTenderGuid = tp.Guid,
                        VendorName = vendorAccount.Name,
                        BusinessField = vendor.BusinessField,
                        TypeCompany = vendor.TypeCompany,

                    };

                    return vendorParticiapnt;


                }).ToList();
            }


            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = projectDetails
            });
        }


        [HttpPost]
        public IActionResult Create(CreateProjectVM createProjectVM)
        {

            if (createProjectVM.StartDate > createProjectVM.EndDate)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Perkiraan tanggal selesai project tidak boleh kurang dari tanggal mulai project"
                });

            }
            var projectIsExist = _projectRepository.GetAll().FirstOrDefault(p => p.Name == createProjectVM.Name);

            if (projectIsExist is not null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Nama project sudah ada"
                });
            }

            var newProject = new Project()
            {
                Guid = Guid.NewGuid(),
                Name = createProjectVM.Name,
                Description = createProjectVM.Description,
                StartDate = createProjectVM.StartDate,
                EndDate = createProjectVM.EndDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var project = _projectRepository.Create(newProject);

            if (project == null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Check your field"
                });
            }

            return Created("", new ResponseVM
            {
                IsSuccess = true,
                Message = "Data created",
                Data = newProject
            });
        }

        [HttpPut("{guid}")]
        public IActionResult Edit(Guid guid, EditProjectVM editProjectVM)
        {
            var project = _projectRepository.GetByGuid(p => p.Guid == guid);

            if (project == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            try
            {
                project.Name = editProjectVM.Name;
                project.Description = editProjectVM.Description;
                project.StartDate = editProjectVM.StartDate;
                project.EndDate = editProjectVM.EndDate;
                project.ModifiedDate = DateTime.Now;

                _projectRepository.SaveChanges();

                return Ok(new ResponseVM
                {
                    IsSuccess = true,
                    Message = "Berhasil mengubah data"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseHandler<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Terjadi kesalahan di server"
                });
            }

        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var project = _projectRepository.GetByGuid(p => p.Guid == guid, tracked: false);

            if (project == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data tidak ditemukan"
                });
            }

            try
            {
                var projectTenders = _projectTenderRepository.GetAll().Where(pt => pt.ProjectGuid == project.Guid);

                if (projectTenders.Count() > 0)
                {
                    _projectTenderRepository.RemoveRange(projectTenders);
                }

                _projectRepository.Delete(project);

                return Ok(new ResponseVM
                {
                    IsSuccess = true,
                    Message = "Berhasil menghapus data"
                });
            }
            catch
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Terjadi kesalahan, pastikan format isian kolom sudah benar"
                });
            }

        }
    }
}
