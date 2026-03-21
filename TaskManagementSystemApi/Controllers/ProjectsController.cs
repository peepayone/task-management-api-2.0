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

        /// <summary>
        /// 依照專案 ID 取得該專案底下的所有任務
        /// GET: /api/projects/{id}/tasks
        /// </summary>
        /// <param name="id">專案 ID</param>
        /// <returns>任務清單</returns>
        [HttpGet("{id}/tasks")]
        public IActionResult GetTasksByProjectId(int id)
        {
            var project = _projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            var tasks = _projectService.GetTasksByProjectId(id);

            return Ok(tasks);
        }

        /// <summary>
        /// 更新專案
        /// PUT: /api/projects/{id}
        /// </summary>
        /// <param name="id">專案 ID</param>
        /// <param name="updateProjectDto">更新專案 DTO</param>
        /// <returns>更新成功回傳 204，找不到專案回傳 404</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = _projectService.UpdateProject(id, updateProjectDto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// 刪除專案
        /// DELETE: /api/projects/{id}
        /// </summary>
        /// <param name="id">專案 ID</param>
        /// <returns>刪除成功回傳 204，失敗回傳 404</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var deleted = _projectService.DeleteProject(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}