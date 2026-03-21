using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemApi.DTOs.Project;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Controllers
{
    /// <summary>
    /// 專案 API 控制器
    /// </summary>
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        /// <summary>
        /// 建構式注入專案服務
        /// </summary>
        /// <param name="projectService">專案服務</param>
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 取得所有專案
        /// GET: /api/projects
        /// </summary>
        /// <returns>專案清單</returns>
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProjects();

            return Ok(projects);
        }

        /// <summary>
        /// 依照專案 ID 取得單一專案
        /// GET: /api/projects/{id}
        /// </summary>
        /// <param name="id">專案 ID</param>
        /// <returns>單一專案資料</returns>
        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        /// <summary>
        /// 建立新專案
        /// POST: /api/projects
        /// </summary>
        /// <param name="createProjectDto">建立專案 DTO</param>
        /// <returns>建立完成後的專案資料</returns>
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            // 如果 request body 不合法，直接回傳 400
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProject = _projectService.CreateProject(createProjectDto);

            return CreatedAtAction(
                nameof(GetProjectById),
                new { id = createdProject.ProjectId },
                createdProject
            );
        }
    }
}