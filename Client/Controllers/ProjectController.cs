using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Models.ViewModels.Project;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<Project>? list = new();

            ResponseVM? response = await _projectService.GetAllProjectsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Project>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectVM createProjectVM)
        {
            if (ModelState.IsValid)
            {
                ResponseVM? response = await _projectService.CreateProjectAsync(createProjectVM);

                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Berhasil membuat proyek";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return View(createProjectVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid guid)
        {
            ProjectDetailsVM? project = new();

            ResponseVM? response = await _projectService.GetProjectByGuidAsync(guid);

            if (response != null && response.IsSuccess)
            {
                project = JsonConvert.DeserializeObject<ProjectDetailsVM>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            ResponseVM? response = await _projectService.GetProjectByGuidAsync(guid);

            if (response != null && response.IsSuccess)
            {
                ProjectDetailsVM? model = JsonConvert.DeserializeObject<ProjectDetailsVM>(Convert.ToString(response.Data));

                var editProjectVM = new EditProjectVM()
                {
                    Guid = model.ProjectGuid,
                    Name = model.ProjectName,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };

                return View(editProjectVM);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectVM editProjectVM)
        {
            if (ModelState.IsValid)
            {
                ResponseVM? response = await _projectService.EditProjectAsync(editProjectVM.Guid, editProjectVM);

                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Berhasil mengubah project";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return View(editProjectVM);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            ResponseVM? response = await _projectService.DeleteProjectAsync(guid);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Berhasil menghapus project";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = response?.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
